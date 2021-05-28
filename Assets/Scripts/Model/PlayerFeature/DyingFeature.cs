using UnityEngine;
using System;


public sealed class DyingFeature : BasePlayerFeature
{
    #region Fields

    private Player _player;
    public IDyingStategy DyingStrategy;

    #endregion


    #region ClassLifeCycles

    public DyingFeature(Player player, NetworkServices netServices, IDyingStategy dyingStategy)
    {
        _player = player;
        _player.OnHealthZeroOrBelow += Die;
        DyingStrategy = dyingStategy;
    }

    ~DyingFeature()
    {
        _player.OnHealthZeroOrBelow -= Die;
    }

    #endregion


    #region Methods

    private void Die()
    {
        if (!IsActive)
            return;

        DyingStrategy.Perform();
    }

    #endregion
}