using UnityEngine.Networking;
using UnityEngine;


public class NetworkServices : NetworkBehaviour
{
    #region Fields

    public SpawnerPool Pools;

    #endregion


    #region Methods

    [Command]
    public void CmdSpawn(NetworkHash128 assetId, Vector3 position, Quaternion rotation)
    {
        GameObject go = Pools.GetFromPool(position, assetId);
        NetworkServer.Spawn(go, assetId);
    }

    #endregion
}
