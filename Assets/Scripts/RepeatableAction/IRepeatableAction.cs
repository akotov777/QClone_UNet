using System;


public interface IRepeatableAction
{
    #region Properties

    Action Method { get; }
    bool IsRepeating { get; }
    float TimeToInvoke { get; }
    float RemainingTime { get; set; }

    #endregion
}