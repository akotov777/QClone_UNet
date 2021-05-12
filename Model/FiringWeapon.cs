using UnityEngine;
using UnityEngine.Networking;


class FiringWeapon : IPlayerFeature
{
    private GameObject obj;

    [Command]
    public void ExecuteFeature()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
            NetworkServer.Spawn(obj);
    }

    public FiringWeapon(GameObject obj)
    {
        this.obj = obj;
    }
}
