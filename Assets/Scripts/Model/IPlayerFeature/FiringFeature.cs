﻿using UnityEngine.Networking;
using UnityEngine;


public class FiringFeature : IPlayerFeature
{
    #region Fields

    private Transform _startPosition;
    private Transform _direction;
    private SpawnerPool _pool;
    private GameObject _projectile;
    private NetworkServices _netServices;

    #endregion


    #region ClassLifeCycles

    public FiringFeature(GameObject projectile, Transform startPositionTransform, Transform directionTransform, SpawnerPool pool, NetworkServices netServices)
    {
        _projectile = projectile;
        _netServices = netServices;
        _startPosition = startPositionTransform;
        _direction = directionTransform;
        _pool = pool;
    }

    #endregion


    #region Methods

    private void Fire(Vector3 direction, Vector3 position)
    {
        _netServices.SpawnWithSetUp(_projectile, new SetUpSettings(position, direction));
    }

    #endregion


    #region IPLayerFeature

    public void ExecuteFeature()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Fire(_direction.rotation * Vector3.forward, _startPosition.position);
        }
    }

    #endregion
}
