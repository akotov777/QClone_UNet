using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections.Generic;


public sealed class PlayerController
{
    #region Fields

    private bool _hasPlayer;
    private Player _player;
    private ExecutablePlayerFeature[] _executableFeatures;
    private PlayerStateMachine _playerStateMachine;
    private NetworkServices _netServices;

    #endregion


    #region ClassLifeCycles

    public PlayerController()
    {
        _player = FindLocalPlayerOnScene();
        _hasPlayer = true;

        _netServices = _player.NetworkServices;

        Dictionary<FeatureType, BasePlayerFeature> featureTable = PopulateFeatureTable();

        _playerStateMachine = new PlayerStateMachine(featureTable);
    }

    #endregion


    #region Methods

    public void Execute()
    {
        if (!_hasPlayer)
            return;

        _playerStateMachine.Execute();

        for (int i = 0; i < _executableFeatures.Length; i++)
        {
            _executableFeatures[i].ExecuteFeature();
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

    private Dictionary<FeatureType, BasePlayerFeature> PopulateFeatureTable()
    {
        _executableFeatures = new ExecutablePlayerFeature[3];

        Dictionary<FeatureType, BasePlayerFeature> featureTable = new Dictionary<FeatureType, BasePlayerFeature>();

        FiringFeature firing = new FiringFeature(_player.Projectile,
                                                 _player.PositionToSpawnProjectile,
                                                 _player.Camera.transform,
                                                 _netServices);
        MovementFeature movement = new MovementFeature(_player.CharacterController);
        LookingFeature looking = new LookingFeature(_player.Camera, _player.CharacterController.transform);

        featureTable.Add(FeatureType.FiringFeature, firing);
        featureTable.Add(FeatureType.MovementFeature, movement);
        featureTable.Add(FeatureType.LookingFeature, looking);

        _executableFeatures[0] = firing;
        _executableFeatures[1] = movement;
        _executableFeatures[2] = looking;

        return featureTable;
    }

    #endregion
}
