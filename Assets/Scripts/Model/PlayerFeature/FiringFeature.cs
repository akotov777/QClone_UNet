using UnityEngine;


public class FiringFeature : ExecutablePlayerFeature
{
    #region Fields

    private Transform _startPosition;
    private Transform _direction;
    private GameObject _projectile;
    private NetworkServices _netServices;

    #endregion


    #region ClassLifeCycles

    public FiringFeature(GameObject projectile, Transform startPositionTransform, Transform directionTransform, NetworkServices netServices)
    {
        _projectile = projectile;
        _netServices = netServices;
        _startPosition = startPositionTransform;
        _direction = directionTransform;
    }

    #endregion


    #region Methods

    private void Fire(Vector3 direction, Vector3 position)
    {
        _netServices.SpawnWithSetUp(_projectile, new SetUpSettings(position, direction));
    }

    #endregion


    #region IPLayerFeature

    public override void ExecuteFeature()
    {
        if (!IsActive)
            return;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Fire(_direction.rotation * Vector3.forward, _startPosition.position);
        }
    }

    #endregion
}
