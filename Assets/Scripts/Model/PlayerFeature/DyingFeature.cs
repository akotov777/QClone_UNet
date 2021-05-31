using UnityEngine;
using System.Collections.Generic;


public sealed class DyingFeature : BasePlayerFeature
{
    #region Fields

    private Player _player;
    public IDyingStategy DyingStrategy;
    private Dictionary<FeatureType, BasePlayerFeature> _featureTable;

    #endregion


    #region ClassLifeCycles

    public DyingFeature(Player player, Dictionary<FeatureType, BasePlayerFeature> featureTable, IDyingStategy dyingStrategy)
    {
        _player = player;
        _player.OnHealthZeroOrBelow += Die;
        _featureTable = featureTable;
        DyingStrategy = dyingStrategy;
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

        _featureTable[FeatureType.TakingDamageFeature].IsActive = false;
        _featureTable[FeatureType.FiringFeature].IsActive = false;
        _featureTable[FeatureType.MovementFeature].IsActive = false;
        _featureTable[FeatureType.DyingFeature].IsActive = false;
        _featureTable[FeatureType.RespawnFeature].IsActive = true;
    }

    #endregion
}