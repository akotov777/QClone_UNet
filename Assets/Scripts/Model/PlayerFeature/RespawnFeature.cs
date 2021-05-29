using System.Collections.Generic;
using UnityEngine;


public class RespawnFeature : ExecutablePlayerFeature
{
    #region Fields

    private Player _player;
    private NetworkServices _netServices;
    private Transform[] _spawnPoints;
    private IChooseSpawnPointLogic _logic;
    private Dictionary<FeatureType, BasePlayerFeature> _featureTable;
    private bool _canSpawn;
    private DelayedAction _respawnDelay;

    public IRespawnStrategy _respawnStrategy;
    public float TimeToRespawn;

    #endregion


    #region ClassLifeCycles

    public RespawnFeature(Player player, NetworkServices netServices, IChooseSpawnPointLogic logic, IRespawnStrategy respawnStrategy)
    {
        _logic = logic;
        _netServices = netServices;
        _player = player;
        _respawnStrategy = respawnStrategy;
        TimeToRespawn = 5.0f;

        _respawnDelay = new DelayedAction(RespawnDelay, TimeToRespawn);
        OnEnable += SetCanSpawnFalse;
        OnEnable += _respawnDelay.AddDelayedAction;
    }

    #endregion


    #region Methods

    private void RespawnDelay()
    {
        SetCanSpawnTrue();
    }

    private void SetCanSpawnTrue()
    {
        _canSpawn = true;
    }
    private void SetCanSpawnFalse()
    {
        _canSpawn = false;
    }

    private void Respawn()
    {
        Transform point = _logic.ChooseSpawnPoint(_spawnPoints);
        _netServices.CmdTeleportObject(_player.gameObject, point.localToWorldMatrix);
        _respawnStrategy.Perform();

        _featureTable[FeatureType.DamageableFeature].IsActive = true;
        _featureTable[FeatureType.FiringFeature].IsActive = true;
        _featureTable[FeatureType.MovementFeature].IsActive = true;
        _featureTable[FeatureType.DyingFeature].IsActive = true;
        _featureTable[FeatureType.RespawnFeature].IsActive = false;
    }

    public override void ExecuteFeature()
    {
        if (IsActive && _canSpawn && Inputs.Respawning.RespawnButtonPressed())
            Respawn();
    }

    #endregion
}
