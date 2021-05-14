using UnityEngine.Networking;
using UnityEngine;


public class FiringFeature : IPlayerFeature
{
    #region Fields

    private Transform _startPosition;
    private GameObject _obj;
    private SpawnerPool _pool;
    private NetworkServices _netServices;
    private NetworkHash128 _assId;

    #endregion


    #region ClassLifeCycles

    public FiringFeature(GameObject obj, Transform startPosition, NetworkServices spawnService, SpawnerPool pool)
    {
        _obj = obj;
        _netServices = spawnService;
        _startPosition = startPosition;
        _pool = pool;
        _assId = _obj.GetComponent<NetworkIdentity>().assetId;
    }

    #endregion


    #region Methods

    public void ExecuteFeature()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _netServices.CmdSpawn(_assId, _startPosition.position, Quaternion.identity);
        }
    }

    #endregion
}
