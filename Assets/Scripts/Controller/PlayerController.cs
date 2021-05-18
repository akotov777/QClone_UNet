using UnityEngine;
using UnityEngine.Networking;
using System;


public sealed class PlayerController
{
    #region Fields

    private Player _player;
    private IPlayerFeature[] _features;
    private PlayerStateMachine _playerStateMachine;

    [SerializeField] private GameObject _objToSpawn;
    [SerializeField] private Transform _positionToSpawn;

    #endregion


    #region UnityMethods

    private void Start()
    {
        CommonInitialization();
        if (isLocalPlayer)
            LocalInitialization();
        if (!isLocalPlayer)
            OtherClientsInitialization();
    }

    private void Update()
    {
        if (!isLocalPlayer) return;

        for (int i = 0; i < _features.Length; i++)
        {
            _features[i].ExecuteFeature();
        }
    }

    #endregion


    #region Methods

    internal void Execute()
    {
        throw new NotImplementedException();
    }

    private void CommonInitialization()
    {
        _features = new IPlayerFeature[2];
        NetworkServices netServices = gameObject.GetComponent<NetworkServices>();

        _features[0] = new FiringFeature(_objToSpawn,
                                         _positionToSpawn,
                                         GetComponentInChildren<Camera>().transform,
                                         netServices);
        _features[1] = new MovementFeature(GetComponentInChildren<Camera>(),
                                           GetComponentInChildren<CharacterController>());
    }

    private void LocalInitialization()
    {
    }

    private void OtherClientsInitialization()
    {
        gameObject.GetComponentInChildren<Camera>().enabled = false;
        gameObject.GetComponentInChildren<AudioListener>().enabled = false;
    }

    #endregion
}
