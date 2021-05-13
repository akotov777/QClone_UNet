using UnityEngine.Networking;
using UnityEngine;


public class NetworkServices : NetworkBehaviour
{	
	#region Methods
	
	[Command]
	public void CmdSpawn(GameObject obj, Vector3 position, Quaternion rotation)
    {
		GameObject go = Instantiate(obj, position, rotation);
		NetworkServer.Spawn(go);
    }
	
	#endregion
}
