using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;


public class SpawnerPool : NetworkBehaviour
{
	#region Fields

	[SerializeField, Range(1,10)] private int _objectsInitialCount;
	[SerializeField] private Vector3 _objectsInitialPosition;
	[SerializeField] private RegisteredPrefabs _prefabs;
	private Dictionary<NetworkHash128, List<GameObject>> _pools = new Dictionary<NetworkHash128, List<GameObject>>();

	public delegate GameObject SpawnDelegate(Vector3 position, NetworkHash128 assetId);
	public delegate void UnSpawnDelegate(GameObject spawned);

	#endregion


	#region UnityMethods

	void Start()
    {
        for (int i = 0; i < _prefabs.Prefabs.Count; i++)
        {
			var assetId = _prefabs.Prefabs[i].GetComponent<NetworkIdentity>().assetId;
			_pools.Add(assetId, new List<GameObject>());

            for (int j = 0; j < _objectsInitialCount; j++)
            {
				_pools[assetId].Add(
					Instantiate(_prefabs.Prefabs[i], _objectsInitialPosition, Quaternion.identity));
				_pools[assetId][j].SetActive(false);
			}

			ClientScene.RegisterSpawnHandler(assetId, SpawnObject, UnSpawnObject);
		}
	}

	#endregion


	#region Methods

	public GameObject GetFromPool(Vector3 position, NetworkHash128 assetId)
	{
		foreach (var obj in _pools[assetId])
		{
			if (!obj.activeInHierarchy)
			{
				obj.transform.position = position;
				obj.SetActive(true);
				return obj;
			}
		}

		int countToAdd = _pools[assetId].Count;
		for (int j = countToAdd; j < countToAdd * 2; j++)
		{
			_pools[assetId].Add(
				Instantiate(_pools[assetId][0], _objectsInitialPosition, Quaternion.identity));
			_pools[assetId][j].SetActive(false);
		}

		return GetFromPool(position, assetId);
	}

	public GameObject SpawnObject(Vector3 position, NetworkHash128 assetId)
	{
		return GetFromPool(position, assetId);
	}

	public void UnSpawnObject(GameObject spawned)
	{
		spawned.SetActive(false);
	}

	#endregion
}
