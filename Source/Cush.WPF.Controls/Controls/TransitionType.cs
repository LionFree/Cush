namespace Cush.WPF.Controls
{
    /// <summary>
    /// enumeration for the different transition types
    /// </summary>
    public enum TransitionType
    {
        /// <summary>
        /// Use the VisualState DefaultTransition
        /// </summary>
        Default = 0,
        /// <summary>
        /// Use the VisualState Normal
        /// </summary>
        Normal = 1,
        /// <summary>
        /// Use the VisualState UpTransition
        /// </summary>
        Up = 2,
        /// <summary>
        /// Use the VisualState DownTransition
        /// </summary>
        Down = 3,
        /// <summary>
        /// Use the VisualState RightTransition
        /// </summary>
        Right = 4,
        /// <summary>
        /// Use the VisualState RightReplaceTransition
        /// </summary>
        RightReplace = 5,
        /// <summary>
        /// Use the VisualState LeftTransition
        /// </summary>
        Left = 6,
        /// <summary>
        /// Use the VisualState LeftReplaceTransition
        /// </summary>
        LeftReplace = 7,
        /// <summary>
        /// Use a custom VisualState, the name must be set using CustomVisualStatesName property
        /// </summary>
        Custom = 8
    }
}