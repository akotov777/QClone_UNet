using UnityEngine.Networking;
using UnityEngine;
using System;
using System.Collections.Generic;


public sealed class NetworkServices : NetworkBehaviour
{
    #region Fields

    private SpawnerPool _pool;
    private static RegisteredPrefabs _prefabs;
    private static Dictionary<NetworkHash128, GameObject> _table;

    #endregion


    #region UnityMethods

    private void Start()
    {
        _prefabs = FindObjectOfType<CustomNetworkManager>().RegisteredPrefabs;
        _table = new Dictionary<NetworkHash128, GameObject>();
        _pool = FindObjectOfType<SpawnerPool>();

        if (_pool == null)
        {
            throw new Exception("There is no object pool on the scene");
        }
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
    public void CmdTeleportObject(GameObject netObj, Matrix4x4 transformTo, bool useRPC = false)
    {
        if (useRPC)
        {
            RpcTeleportObject(netObj, transformTo);
            return;
        }
        netObj.transform.SetFromMatrix(transformTo);
    }

    [ClientRpc]
    private void RpcTeleportObject(GameObject netObj, Matrix4x4 transformTo)
    {
        netObj.transform.SetFromMatrix(transformTo);
    }

    [Command]
    public void CmdChangeNetworkedObjectMaterial(GameObject netObj, string materialInResources)
    {
        RpcChangeNetworkedObjectMaterial(netObj, materialInResources);
    }

    [ClientRpc]
    private void RpcChangeNetworkedObjectMaterial(GameObject netObj, string materialInResources)
    {
        var renderer = netObj.GetComponent<Renderer>();
        Material mat = Resources.Load<Material>(materialInResources);
        renderer.sharedMaterial = mat;
    }

    [Command]
    public void CmdChangeNetworkedObjectMaterials(GameObject netObj, string materialInResources)
    {
        RpcChangeNetworkedObjectMaterials(netObj, materialInResources);
    }

    [ClientRpc]
    private void RpcChangeNetworkedObjectMaterials(GameObject netObj, string materialInResources)
    {
        var renderers = netObj.GetComponentsInChildren<Renderer>();
        Material mat = Resources.Load<Material>(materialInResources);
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].sharedMaterial = mat;
        }
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
        GameObject go = _pool.GetFromPool(position, prefab);
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
            goToSpawn = _pool.GetFromPool(settings.position, chekingInstance);
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
