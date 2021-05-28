using UnityEngine;


public class DyingFeature : BasePlayerFeature
{
    #region Fields

    private Player _player;
    private NetworkServices _netServices;

    #endregion


    #region ClassLifeCycles

    public DyingFeature(Player player, NetworkServices netServices)
    {
        _player = player;
        _player.OnHealthZeroOrBelow += Die;
        _netServices = netServices;
    }

    #endregion


    #region Methods

    private void Die()
    {
        var renderers = _player.GetComponents<Renderer>();
        for (int i = 0; i < renderers.Length; i++)
        {

        }
    }

    #endregion
}