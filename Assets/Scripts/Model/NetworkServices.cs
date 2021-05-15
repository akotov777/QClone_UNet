using UnityEngine.Networking;
using UnityEngine;
using System;


public sealed class NetworkServices : NetworkBehaviour
{
    #region Fields

    public SpawnerPool Pool;

    #endregion


    #region Methods

    [Command]
    public void CmdSpawnFromPool(NetworkHash128 assetId, Vector3 position)
    {
        GameObject go = Pool.GetFromPool(position, assetId);
        NetworkServer.Spawn(go, assetId);
    }

    [Command]
    public void CmdSpawn(GameObject obj, Vector3 position)
    {
        GameObject chekingInstance = Instantiate(obj);
        if (chekingInstance.HasComponent<IPoolable>())
        {
            CmdSpawnFromPool(obj.GetComponent<NetworkIdentity>().assetId, position);
        }
        else
        {
            GameObject go = Instantiate(obj, position, Quaternion.identity);
            NetworkServer.Spawn(go);
        }
        Destroy(chekingInstance);
    }

    [Command]
    public void CmdSpawnWithSetUp(GameObject setUpable, SetUpSettings settings)
    {
        GameObject chekingInstance = Instantiate(setUpable);
        if (!chekingInstance.HasComponent<ISpawnSetUpable>())
        {
            throw new Exception("Given GameObject is not SpawnSetUpable");
        }

        GameObject goToSpawn;
        if (chekingInstance.HasComponent<IPoolable>())
        {
            goToSpawn = Pool.GetFromPool(settings.position, setUpable);
        }
        else
        {
            goToSpawn = Instantiate(setUpable, settings.position, settings.rotation);
        }

        goToSpawn.GetComponent<ISpawnSetUpable>().SpawnSetUp(settings);
        NetworkServer.Spawn(goToSpawn);
        Destroy(chekingInstance);
    }

    #endregion
}
