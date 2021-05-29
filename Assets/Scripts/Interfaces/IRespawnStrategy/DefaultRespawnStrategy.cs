﻿public sealed class DefaultRespawnStrategy : IRespawnStrategy
{
    #region Fields

    private NetworkServices _netServices;
    private Player _player;

    #endregion


    #region ClassLifeCycles

    public DefaultRespawnStrategy(Player player, NetworkServices netServices)
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
            Constants.ResourcesPaths.Materials.Default);
    }

    #endregion
}