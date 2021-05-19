using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections.Generic;


public sealed class PlayerController
{
    #region Fields

    private Player _player;
    private IPlayerFeature[] _features;
    private PlayerStateMachine _playerStateMachine;
    private NetworkServices _netServices;

    #endregion


    #region ClassLifeCycles

    public PlayerController()
    {
        _player = FindLocalPlayerOnScene();
        _netServices = _player.NetworkServices;

        Dictionary<Type, IPlayerFeature> featureTable = PopulateFeatureTable();

        featureTable.Values.CopyTo(_features, 0);

        _playerStateMachine = new PlayerStateMachine(featureTable);
    }

    #endregion


    #region Methods

    public void Execute()
    {
        _playerStateMachine.Execute();

        for (int i = 0; i < _features.Length; i++)
        {
            _features[i].ExecuteFeature();
        }
    }

    private Player FindLocalPlayerOnScene()
    {
        var players = GameObject.FindObjectsOfType<Player>();
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].isLocalPlayer)
            {
                return players[i];
            }
        }
        throw new Exception("There is no local player on scene");
    }

    private Dictionary<Type, IPlayerFeature> PopulateFeatureTable()
    {
        Dictionary<Type, IPlayerFeature> featureTable = new Dictionary<Type, IPlayerFeature>();

        FiringFeature firing = new FiringFeature(_player.Projectile, _player.PositionToSpawnProjectile, _player.Camera.transform, _netServices);
        MovementFeature movement = new MovementFeature(_player.Camera, _player.CharacterController);

        featureTable.Add(typeof(FiringFeature), firing);
        featureTable.Add(typeof(MovementFeature), movement);

        return featureTable;
    }

    #endregion
}
