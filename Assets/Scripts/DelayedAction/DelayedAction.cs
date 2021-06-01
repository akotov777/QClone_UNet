using System;


public sealed class DelayedAction : IDelayedAction
{
    #region ClassLifeCycles

    public DelayedAction(Action method, float timeToNextInoke, bool isRepeating = false)
    {
        Method = method;
        TimeToInvoke = timeToNextInoke;
        RemainingTime = timeToNextInoke;
        IsRepeating = isRepeating;
    }

    #endregion


    #region IDelayedAction

    public Action Method { get; }
    public bool IsRepeating { get; }
    public float TimeToInvoke { get; }
    public float RemainingTime { get; set; }

    #endregion
}
