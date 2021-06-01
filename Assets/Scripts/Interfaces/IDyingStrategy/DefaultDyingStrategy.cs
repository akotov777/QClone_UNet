public sealed class DefaultDyingStrategy : IDyingStategy
{
    #region Fields

    private NetworkServices _netServices;
    private Player _player;

    #endregion


    #region ClassLifeCycles

    public DefaultDyingStrategy(Player player, NetworkServices netServices)
    {
        _player = player;
        _netServices = netServices;
    }

    #endregion


    #region IDyingStrategy

    public void Perform()
    {
        _netServices.CmdChangeNetworkedObjectMaterials(
            _player.gameObject,
            Constants.ResourcesPaths.Materials.TestDead);
    }

    #endregion
}