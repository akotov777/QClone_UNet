using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;


public sealed class SpawnerPool : MonoBehaviour
{
    #region Fields

    [SerializeField, Range(1, 10)] private int _objectsInitialCount;
    [SerializeField] private Vector3 _objectsInitialPosition;
    private RegisteredPrefabs _prefabs;
    private Dictionary<NetworkHash128, List<GameObject>> _pools = new Dictionary<NetworkHash128, List<GameObject>>();

    public delegate GameObject SpawnDelegate(Vector3 position, NetworkHash128 assetId);
    public delegate void UnSpawnDelegate(GameObject spawned);

    #endregion


    #region UnityMethods

    void Start()
    {
        _prefabs = FindObjectOfType<CustomNetworkManager>().RegisteredPrefabs;
        List<GameObject> gameObjectsToPool = new List<GameObject>();
        for (int i = 0; i < _prefabs.Prefabs.Count; i++)
        {
            if (_prefabs.Prefabs[i].HasComponent<IPoolable>())
            {
                gameObjectsToPool.Add(_prefabs.Prefabs[i]);
            }
        }

        for (int i = 0; i < gameObjectsToPool.Count; i++)
        {
            var assetId = gameObjectsToPool[i].GetComponent<NetworkIdentity>().assetId;
            if (!_pools.ContainsKey(assetId))
            {


                _pools.Add(assetId, new List<GameObject>());

                for (int j = 0; j < _objectsInitialCount; j++)
                {
                    _pools[assetId].Add(
                        Instantiate(gameObjectsToPool[i], _objectsInitialPosition, Quaternion.identity));
                    _pools[assetId][j].GetComponent<IPoolable>().ReturnToPool();
                }

                ClientScene.RegisterSpawnHandler(assetId, SpawnObject, UnSpawnObject);
            }
        }
    }

    #endregion


    #region Methods

    public GameObject GetFromPool(Vector3 position, NetworkHash128 assetId)
    {
        foreach (var obj in _pools[assetId])
        {
            var poolable = obj.GetComponent<IPoolable>();
            if (poolable.IsInPool)
            {
                obj.transform.position = position;
                poolable.GetFromPool();
                return obj;
            }
        }

        int countToAdd = _pools[assetId].Count;
        for (int j = countToAdd; j < countToAdd * 2; j++)
        {
            _pools[assetId].Add(
                Instantiate(_pools[assetId][0], _objectsInitialPosition, Quaternion.identity));
            _pools[assetId][j].GetComponent<IPoolable>().ReturnToPool();
        }

        return GetFromPool(position, assetId);
    }

    public GameObject GetFromPool(Vector3 position, GameObject prefab)
    {
        return GetFromPool(position, prefab.GetComponent<NetworkIdentity>().assetId);
    }


    public GameObject SpawnObject(Vector3 position, NetworkHash128 assetId)
    {
        return GetFromPool(position, assetId);
    }

    public void UnSpawnObject(GameObject spawned)
    {
        spawned.GetComponent<IPoolable>().ReturnToPool();
    }

    #endregion
}
