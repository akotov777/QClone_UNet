using UnityEngine;


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


    #region Methods



    #endregion


    #region IDyingStrategy

    public void Perform()
    {
        var renderers = _player.GetComponents<Renderer>();
        for (int i = 0; i < renderers.Length; i++)
        {

        }
    }

    #endregion
}