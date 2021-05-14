using UnityEngine.Networking;
using UnityEngine;


public class CustomNetworkManager : NetworkManager
{
	#region Fields

	[SerializeField]private RegisteredPrefabs _prefabsToRegister;

    #endregion


    #region UnityMethods

    private void Start()
    {
        for (int i = 0; i < _prefabsToRegister.Prefabs.Count; i++)
            spawnPrefabs.Add(_prefabsToRegister.Prefabs[i]);
    }

    #endregion
}
