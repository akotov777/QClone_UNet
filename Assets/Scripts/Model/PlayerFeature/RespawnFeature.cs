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
    public float TimeToRespawn;

    #endregion


    #region ClassLifeCycles

    public RespawnFeature(IChooseSpawnPointLogic logic, NetworkServices netServices, Player player)
    {
        _logic = logic;
        _netServices = netServices;
        _player = player;
    }

    #endregion


    #region Methods

    private void RespawnDelay()
    {

    }

    private void Respawn()
    {
        Transform point = _logic.ChooseSpawnPoint(_spawnPoints);
        _netServices.CmdTeleportObject(_player.gameObject, point.localToWorldMatrix);
        _netServices.CmdChangeNetworkedObjectMaterials(_player.gameObject, Constants.ResourcesPaths.Materials.Default);

        _featureTable[FeatureType.DamageableFeature].IsActive = true;
        _featureTable[FeatureType.FiringFeature].IsActive = true;
        _featureTable[FeatureType.MovementFeature].IsActive = true;
        _featureTable[FeatureType.DyingFeature].IsActive = true;
        _featureTable[FeatureType.RespawnFeature].IsActive = false;
    }

    public override void ExecuteFeature()
    {

    }

    #endregion
}
