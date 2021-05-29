using System.Collections.Generic;


    public static partial class DelayedActionExtentions
    {
        #region Fields

        private static readonly List<IDelayedAction> _delayedActions = new List<IDelayedAction>();

        #endregion


        #region Properties

        public static List<IDelayedAction> DelayedActions => _delayedActions;

        #endregion


        #region Methods

        public static void AddDelayedAction(this IDelayedAction value)
        {
            if (_delayedActions.Contains(value)) return;

            value.RemainingTime = value.TimeToInvoke;
            _delayedActions.Add(value);
        }

        public static void RemoveDelayedAction(this IDelayedAction value)
        {
            if (!_delayedActions.Contains(value)) return;

            _delayedActions.Remove(value);
        }

        public static void AddWithReplace(this IDelayedAction value)
        {
            value.RemoveDelayedAction();
            value.AddDelayedAction();
        }

        #endregion
    }
