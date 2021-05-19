public abstract class BasePlayerFeature : IPlayerFeature
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


    #region Methods

    public abstract void ExecuteFeature();

    #endregion
}