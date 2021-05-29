using System.Collections.Generic;
using UnityEngine;



public sealed class DelayedActionController
{
    #region Fields

    private readonly List<IDelayedAction> _delayedActions;

    #endregion


    #region ClassLifeCycles

    public DelayedActionController()
    {
        _delayedActions = DelayedActionExtentions.DelayedActions;
    }

    #endregion


    #region Methods

    public void Execute()
    {
        var time = Time.deltaTime;
        for (var i = 0; i < _delayedActions.Count; i++)
        {
            var obj = _delayedActions[i];
            obj.RemainingTime -= time;
            if (obj.RemainingTime <= 0.0f)
            {
                obj.Method.Invoke();
                if (!obj.IsRepeating)
                    obj.RemoveDelayedAction();
                else
                    obj.RemainingTime = obj.TimeToInvoke;
            }
        }
    }

    #endregion
}
