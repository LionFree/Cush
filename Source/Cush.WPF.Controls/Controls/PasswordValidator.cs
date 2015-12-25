using System.ComponentModel.DataAnnotations;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Cush.Common.Crypto;

namespace Cush.WPF.Controls
{
    public class PasswordValidator : FrameworkElement
    {
        static PasswordValidator()
        {
            Crypto = new CryptoHasher();
            SsHelper = new SecureStringHelper();
        }


        private static readonly SecureStringHelper SsHelper;
        private static readonly CryptoHasher Crypto;
        private static PasswordBox _confirmBox;
        private static PasswordBox _originalBox;
        private static bool _updating;


        // unique per application; MUST BE SECRET
        // Don't store this in memory for sensitive stuff - 
        // anyone can attach a debugger and get yer key if it's stored in a variable like this.
        //private static readonly byte[] Key;

        public static readonly DependencyProperty ValidateAgainstProperty =
            DependencyProperty.RegisterAttached("ValidateAgainst",
                typeof (PasswordBox),
                typeof (PasswordValidator),
                new FrameworkPropertyMetadata(null, ValidateAgainstChanged));

        public static readonly DependencyProperty EncryptedPasswordProperty =
            DependencyProperty.RegisterAttached("EncryptedPassword",
                typeof (SecureString),
                typeof (PasswordValidator),
                new PropertyMetadata(default(SecureString), OnEncryptedPasswordChanged));


        public static void SetValidateAgainst(UIElement element, PasswordBox value)
        {
            element.SetValue(ValidateAgainstProperty, value);
        }

        public static PasswordBox GetValidateAgainst(UIElement element)
        {
            return (PasswordBox) element.GetValue(ValidateAgainstProperty);
        }

        public static SecureString GetEncryptedPassword(DependencyObject dp)
        {
            return (SecureString) dp.GetValue(EncryptedPasswordProperty);
        }

        public static void SetEncryptedPassword(DependencyObject dp, SecureString value)
        {
            dp.SetValue(EncryptedPasswordProperty, value);
        }
        
        /// <summary>
        ///     Updates the bindings and the PasswordChanged event handlers on two PasswordBoxes.
        /// </summary>
        private static void ValidateAgainstChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // d is the confirm box
            _confirmBox = d as PasswordBox;
            if (_confirmBox == null) return;

            // e.NewValue is the box we're attaching to (the "original" password box)
            _originalBox = e.NewValue as PasswordBox;
            if (e != null && _originalBox == null)
                throw new ValidationException("ValidateAgainst must be a PasswordBox. \"" + e.NewValue + "\"");

            // Update the bindings to remove the need to specify Validation details in the XAML.
            RebindConfirmBox();
            BindOriginalBox();

            // Update the PasswordChange events to handle validating the passwords.
            _originalBox.PasswordChanged += ValidatePasswords;
            _confirmBox.PasswordChanged += ValidatePasswords;
        }

        /// <summary>
        ///     Creates a validatable binding on the Original box, so that
        ///     when the validation fires on the confirmation box, it'll also
        ///     show on the original box.
        ///     (Removes the need to bind ANYTHING on the original box in XAML.)
        /// </summary>
        private static void BindOriginalBox()
        {
            if (_originalBox == null) return;

            var newBinding = new Binding
            {
                Source = _confirmBox,
                ValidatesOnDataErrors = true,
                ValidatesOnExceptions = true,
                UpdateSourceTrigger = UpdateSourceTrigger.Explicit
            };

            _originalBox.SetBinding(ValidateAgainstProperty, newBinding);
        }

        /// <summary>
        ///     Rebinds the confirm box, to be sure that the validation fires.
        ///     (Removes the need to specify "ValidatesOnErrors=True, ValidatesOnExceptions=True" in the XAML binding.)
        /// </summary>
        private static void RebindConfirmBox()
        {
            if (_confirmBox == null) return;
            // Get the binding for the _confirmBox
            var newConfirmBinding = new Binding
            {
                ValidatesOnDataErrors = true,
                ValidatesOnExceptions = true
            };
            var confirmBinding = BindingOperations.GetBinding(_confirmBox, ValidateAgainstProperty);
            if (confirmBinding == null) return;

            if (confirmBinding.Source != null && confirmBinding.ElementName == null)
                newConfirmBinding.Source = confirmBinding.Source;
            else if (confirmBinding.ElementName != null && confirmBinding.Source == null)
                newConfirmBinding.ElementName = confirmBinding.ElementName;

            _confirmBox.SetBinding(ValidateAgainstProperty, newConfirmBinding);
        }

        /// <summary>
        ///     Validates the passwords in the original and confirmation box.
        /// </summary>
        private static void ValidatePasswords(object sender, RoutedEventArgs e)
        {
            var originalBoxBindingExpression = BindingOperations.GetBindingExpression(_originalBox,
                ValidateAgainstProperty);
            var originalBoxBindingExpressionBase = BindingOperations.GetBindingExpressionBase(_originalBox,
                ValidateAgainstProperty);
            if (originalBoxBindingExpression == null || originalBoxBindingExpressionBase == null) return;

            // Get the binding for the _confirmBox
            var confirmBoxBindingExpression = BindingOperations.GetBindingExpression(_confirmBox,
                ValidateAgainstProperty);
            var confirmBoxBindingExpressionBase = BindingOperations.GetBindingExpressionBase(_confirmBox,
                ValidateAgainstProperty);
            if (confirmBoxBindingExpression == null || confirmBoxBindingExpressionBase == null) return;


            if (!SsHelper.AreEqual(_originalBox.SecurePassword, _confirmBox.SecurePassword))
            {
                var originalError = new ValidationError(new ExceptionValidationRule(), originalBoxBindingExpression)
                {
                    ErrorContent = Cush.WPF.Controls.Strings.ERROR_PasswordsDoNotMatch
                };
                var confirmError = new ValidationError(new ExceptionValidationRule(), confirmBoxBindingExpression)
                {
                    ErrorContent = Controls.Strings.ERROR_PasswordsDoNotMatch
                };

                Validation.MarkInvalid(originalBoxBindingExpression, originalError);
                Validation.MarkInvalid(confirmBoxBindingExpressionBase, confirmError);
            }
            else
            {
                Validation.ClearInvalid(originalBoxBindingExpression);
                Validation.ClearInvalid(confirmBoxBindingExpressionBase);
            }
        }

        /// <summary>
        ///     Wires the passwordbox to the designated SecureString property (e.g., on your viewmodel).
        /// </summary>
        private static void OnEncryptedPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var box = d as PasswordBox;

            // only handle this event when the property is attached to a PasswordBox
            // and when the BoundPassword attached property has been set
            if (box == null || GetEncryptedPassword(d) == null)
            {
                return;
            }

            // avoid recursive updating by ignoring the box's changed event
            box.PasswordChanged -= HandlePasswordChanged;

            // Update the passwordbox with the existing value.
            if (!_updating)
            {
                box.Password = SsHelper.SecureStringToString((SecureString) e.NewValue);
            }

            box.PasswordChanged += HandlePasswordChanged;
        }

        /// <summary>
        ///     Pushes the new password into the EncryptedPassword property.
        /// </summary>
        private static void HandlePasswordChanged(object sender, RoutedEventArgs e)
        {
            var box = sender as PasswordBox;
            if (box == null) return;

            _updating = true;
            SetEncryptedPassword(box, box.SecurePassword);
            _updating = false;
        }

        public static string Hash(SecureString password, string salt)
        {
            return Crypto.Hash(password, salt);
        }

        public static string CreateNewSalt()
        {
            return Crypto.CreateNewSalt();
        }

        public static bool AreEqual(SecureString ss1, SecureString ss2)
        {
            return SsHelper.AreEqual(ss1, ss2);
        }
    }
}