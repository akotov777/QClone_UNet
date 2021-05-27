public abstract class BasePlayerFeature
{
    #region Fields

    internal bool _isActive = true;

    #endregion


    #region Properties

    public bool IsActive
    {
        get { return _isActive; }
        set { _isActive = value; }
    }

    #endregion
}