using System;


public sealed class RepeatableAction : IRepeatableAction
{
    #region ClassLifeCycles

    public RepeatableAction(Action method, float timeToNextInoke, bool isRepeating = false)
    {
        Method = method;
        TimeToInvoke = timeToNextInoke;
        RemainingTime = timeToNextInoke;
        IsRepeating = isRepeating;
    }

    #endregion


    #region IRepeatableAction

    public Action Method { get; }
    public bool IsRepeating { get; }
    public float TimeToInvoke { get; }
    public float RemainingTime { get; set; }

    #endregion
}
