using UnityEngine.Networking;
using UnityEngine;
using System;
using System.Collections.Generic;


public sealed class NetworkServices : NetworkBehaviour
{
    #region Fields

    public SpawnerPool Pool;
    private static RegisteredPrefabs _prefabs;
    private static Dictionary<NetworkHash128, GameObject> _table;

    #endregion


    #region UnityMethods

    private void Start()
    {
        _prefabs = Resources.Load<RegisteredPrefabs>("Data/RegisteredPrefabs");
        _table = new Dictionary<NetworkHash128, GameObject>();

        for (int i = 0; i < _prefabs.Prefabs.Count; i++)
        {
            var id = _prefabs.Prefabs[i].GetComponent<NetworkIdentity>().assetId;

            if (!_table.ContainsKey(id))
                _table.Add(id, _prefabs.Prefabs[i]);
        }
    }

    #endregion


    #region Methods

    public void SpawnWithSetUp(GameObject setUpable, SetUpSettings settings)
    {
        CmdSpawnWithSetUp(Convert(setUpable), settings);
    }

    public void Spawn(GameObject gameObject, Vector3 position)
    {
        CmdSpawn(Convert(gameObject), position);
    }

    private NetworkHash128 Convert(GameObject gameObject)
    {
        return gameObject.GetComponent<NetworkIdentity>().assetId;
    }

    private GameObject Convert(NetworkHash128 hash)
    {
        return _table[hash];
    }

    [Command]
    private void CmdSpawn(NetworkHash128 hash, Vector3 position)
    {
        var chekingGO = Convert(hash);

        if (chekingGO.HasComponent<IPoolable>())
        {
            CmdSpawnFromPool(chekingGO, position);
        }
        else
        {
            GameObject go = Instantiate(chekingGO, position, Quaternion.identity);
            NetworkServer.Spawn(go);
        }
    }

    [Command]
    private void CmdSpawnFromPool(GameObject prefab, Vector3 position)
    {
        GameObject go = Pool.GetFromPool(position, prefab);
        NetworkServer.Spawn(go);
    }

    [Command]
    private void CmdSpawnWithSetUp(NetworkHash128 hash, SetUpSettings settings)
    {
        GameObject chekingInstance = Convert(hash);
        if (!chekingInstance.HasComponent<ISpawnSetUpable>())
        {
            throw new Exception("Given GameObject is not SpawnSetUpable");
        }

        GameObject goToSpawn;
        if (chekingInstance.HasComponent<IPoolable>())
        {
            goToSpawn = Pool.GetFromPool(settings.position, chekingInstance);
        }
        else
        {
            goToSpawn = Instantiate(chekingInstance, settings.position, settings.rotation);
        }

        goToSpawn.GetComponent<ISpawnSetUpable>().SpawnSetUp(settings);
        NetworkServer.Spawn(goToSpawn);
    }

    #endregion
}
