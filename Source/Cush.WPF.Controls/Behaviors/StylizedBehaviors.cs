using System.Windows;
using System.Windows.Interactivity;

namespace Cush.WPF.Controls.Behaviors
{
    public class StylizedBehaviors
    {
        private static readonly DependencyProperty OriginalBehaviorProperty = DependencyProperty.RegisterAttached(@"OriginalBehaviorInternal", typeof(Behavior), typeof(StylizedBehaviors), new UIPropertyMetadata(null));

        public static readonly DependencyProperty BehaviorsProperty = DependencyProperty.RegisterAttached(
            @"Behaviors",
            typeof(StylizedBehaviorCollection),
            typeof(StylizedBehaviors),
            new FrameworkPropertyMetadata(null, OnPropertyChanged));
        public static StylizedBehaviorCollection GetBehaviors(DependencyObject uie)
        {
            return (StylizedBehaviorCollection)uie.GetValue(BehaviorsProperty);
        }

        public static void SetBehaviors(DependencyObject uie, StylizedBehaviorCollection value)
        {
            uie.SetValue(BehaviorsProperty, value);
        }
        private static Behavior GetOriginalBehavior(DependencyObject obj)
        {
            return obj.GetValue(OriginalBehaviorProperty) as Behavior;
        }

        private static int GetIndexOf(BehaviorCollection itemBehaviors, Behavior behavior)
        {
            var index = -1;

            var orignalBehavior = GetOriginalBehavior(behavior);

            for (var i = 0; i < itemBehaviors.Count; i++)
            {
                Behavior currentBehavior = itemBehaviors[i];

                if (Equals(currentBehavior, behavior)
                    || Equals(currentBehavior, orignalBehavior))
                {
                    index = i;
                    break;
                }

                Behavior currentOrignalBehavior = GetOriginalBehavior(currentBehavior);

                if (Equals(currentOrignalBehavior, behavior)
                    || Equals(currentOrignalBehavior, orignalBehavior))
                {
                    index = i;
                    break;
                }
            }

            return index;
        }

        private static void OnPropertyChanged(DependencyObject dpo, DependencyPropertyChangedEventArgs e)
        {
            var uie = dpo as UIElement;

            if (uie == null)
            {
                return;
            }

            BehaviorCollection itemBehaviors = Interaction.GetBehaviors(uie);

            var newBehaviors = e.NewValue as StylizedBehaviorCollection;
            var oldBehaviors = e.OldValue as StylizedBehaviorCollection;

            if (Equals(newBehaviors, oldBehaviors))
            {
                return;
            }

            if (oldBehaviors != null)
            {
                foreach (var behavior in oldBehaviors)
                {
                    var index = GetIndexOf(itemBehaviors, behavior);

                    if (index >= 0)
                    {
                        itemBehaviors.RemoveAt(index);
                    }
                }
            }

            if (newBehaviors == null) return;

            foreach (var behavior in newBehaviors)
            {
                var index = GetIndexOf(itemBehaviors, behavior);

                if (index >= 0) continue;
                var clone = (Behavior) behavior.Clone();
                SetOriginalBehavior(clone, behavior);
                itemBehaviors.Add(clone);
            }

        }

        private static void SetOriginalBehavior(DependencyObject obj, Behavior value)
        {
            obj.SetValue(OriginalBehaviorProperty, value);
        }
    }
}
