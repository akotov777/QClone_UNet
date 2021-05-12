using UnityEngine.Networking;
using UnityEngine;

public class FiringFeature : IPlayerFeature
{
    private GameObject _obj;

    public void ExecuteFeature()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            CmdSpawnObj(_obj);
    }

    [Command]
    private void CmdSpawnObj(GameObject obj)
    {
        var go = GameObject.Instantiate(_obj, Vector3.up, Quaternion.identity);
        NetworkServer.Spawn(go);
    }

    public FiringFeature(GameObject obj)
    {
        _obj = obj;
    }
}
