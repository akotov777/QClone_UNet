using System.Collections.Generic;


    public static partial class RepeatableActionExtentions
    {
        #region Fields

        private static readonly List<IRepeatableAction> _timeRemainings = new List<IRepeatableAction>();

        #endregion


        #region Properties

        public static List<IRepeatableAction> TimeRemainings => _timeRemainings;

        #endregion


        #region Methods

        public static void AddTimeRemaining(this IRepeatableAction value)
        {
            if (_timeRemainings.Contains(value)) return;

            value.RemainingTime = value.TimeToInvoke;
            _timeRemainings.Add(value);
        }

        public static void RemoveTimeRemaining(this IRepeatableAction value)
        {
            if (!_timeRemainings.Contains(value)) return;

            _timeRemainings.Remove(value);
        }

        public static void AddWithReplace(this IRepeatableAction value)
        {
            value.RemoveTimeRemaining();
            value.AddTimeRemaining();
        }

        #endregion
    }
