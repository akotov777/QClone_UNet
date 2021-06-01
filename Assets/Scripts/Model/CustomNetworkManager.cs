using UnityEngine.Networking;
using UnityEngine;


public class CustomNetworkManager : NetworkManager
{
	#region Fields

	[SerializeField]private RegisteredPrefabs _prefabsToRegister;

    #endregion


    #region Properties

    public RegisteredPrefabs RegisteredPrefabs { get { return _prefabsToRegister; } }

    #endregion


    #region UnityMethods

    private void Start()
    {
        for (int i = 0; i < _prefabsToRegister.Prefabs.Count; i++)
            if(!_prefabsToRegister.Prefabs[i].HasComponent<IPoolable>())
                spawnPrefabs.Add(_prefabsToRegister.Prefabs[i]);
    }

    #endregion
}
