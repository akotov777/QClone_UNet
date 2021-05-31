using System.Collections.Generic;
using UnityEngine;


public class RespawnFeature : ExecutablePlayerFeature
{
    #region Fields

    private Player _player;
    private NetworkServices _netServices;
    private SpawnPoint[] _spawnPoints;
    private IChooseSpawnPointLogic _logic;
    private Dictionary<FeatureType, BasePlayerFeature> _featureTable;
    private bool _canSpawn;
    private DelayedAction _respawnDelay;

    public IRespawnStrategy _respawnStrategy;
    public float TimeToRespawn;

    #endregion


    #region ClassLifeCycles

    public RespawnFeature(Player player,
                          NetworkServices netServices,
                          Dictionary<FeatureType, BasePlayerFeature> featureTable,
                          IChooseSpawnPointLogic logic, 
                          IRespawnStrategy respawnStrategy)
    {
        IsActive = false;
        _player = player;
        _netServices = netServices;
        _featureTable = featureTable;
        _logic = logic;
        _respawnStrategy = respawnStrategy;
        _spawnPoints = GameObject.FindObjectsOfType<SpawnPoint>();
        TimeToRespawn = 0.1f;

        _respawnDelay = new DelayedAction(SetCanSpawnTrue, TimeToRespawn);
        OnEnable += () => Inputs.Respawning.IsActive = true;
        OnEnable += SetCanSpawnFalse;
        OnEnable += _respawnDelay.AddDelayedAction;
        OnDisable += () => Inputs.Respawning.IsActive = false;
    }

    #endregion


    #region Methods

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
        SpawnPoint point = _logic.ChooseSpawnPoint(_spawnPoints);
        _netServices.CmdTeleportObject(_player.gameObject, point.transform.localToWorldMatrix);
        _respawnStrategy.Perform();

        _featureTable[FeatureType.TakingDamageFeature].IsActive = true;
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
