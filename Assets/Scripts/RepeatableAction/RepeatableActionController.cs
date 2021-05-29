using System.Collections.Generic;
using UnityEngine;



public sealed class RepeatableActionController
{
    #region Fields

    private readonly List<IRepeatableAction> _timeRemainings;

    #endregion


    #region ClassLifeCycles

    public RepeatableActionController()
    {
        _timeRemainings = RepeatableActionExtentions.TimeRemainings;
    }

    #endregion


    #region IExecute

    public void Execute()
    {
        var time = Time.deltaTime;
        for (var i = 0; i < _timeRemainings.Count; i++)
        {
            var obj = _timeRemainings[i];
            obj.RemainingTime -= time;
            if (obj.RemainingTime <= 0.0f)
            {
                obj.Method.Invoke();
                if (!obj.IsRepeating)
                    obj.RemoveTimeRemaining();
                else
                    obj.RemainingTime = obj.TimeToInvoke;
            }
        }
    }

    #endregion
}
