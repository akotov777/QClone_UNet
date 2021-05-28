using System;


public abstract class BasePlayerFeature
{
    #region Fields

    internal bool _isActive = true;
    internal Action OnEnable;
    internal Action OnDisable;

    #endregion


    #region Properties

    public bool IsActive
    {
        get { return _isActive; }
        set
        {
            bool oldValue = _isActive;
            _isActive = value;
            if (oldValue != _isActive)
            {
                if (oldValue == true)
                    OnDisable.Invoke();
                else
                    OnEnable.Invoke();
            }
        }
    }

    #endregion
}