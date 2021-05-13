using UnityEngine.Networking;
using UnityEngine;


public class FiringFeature : NetworkBehaviour, IPlayerFeature
{
    #region Fields

    private GameObject _obj;
    private NetworkServices _netServices;

    #endregion


    #region ClassLifeCycles

    public FiringFeature(GameObject obj, NetworkServices spawnService)
    {
        _obj = obj;
        _netServices = spawnService;
    }

    #endregion


    #region Methods

    public void ExecuteFeature()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            _netServices.CmdSpawn(_obj, Vector3.up, Quaternion.identity);
    }

    #endregion
}
