using UnityEngine;


public struct SetUpSettings
{
    #region Fields

    public Vector3 position;
    public Vector3 direction;
    public Quaternion quaternion;
    public Quaternion rotation;

    #endregion


    #region ClassLifeCycles

    public SetUpSettings(Vector3 position, Vector3 direction, Quaternion rotation, Quaternion quaternion)
    {
        this.position = position;
        this.direction = direction;
        this.quaternion = quaternion;
        this.rotation = rotation;
    }

    public SetUpSettings(Vector3 position, Vector3 direction, Quaternion rotation) : this(position, direction, rotation, Quaternion.identity)
    {
    }

    public SetUpSettings(Vector3 position, Vector3 direction) : this(position, direction, Quaternion.identity)
    { 
    }

    #endregion
}
