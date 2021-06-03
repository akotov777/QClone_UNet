using System;
using UnityEngine;
using UnityEngine.Networking;


public class GameController : MonoBehaviour
{
    #region Fields

    [SerializeField] private PlayerController _playerController;
    [SerializeField] private UIController _uIController;
    private DelayedActionController _delayedActionController;

    [SerializeField] private NetworkManager _netManager;
    private NetworkClient _client;
    public Action PlayerCreatedCallBack;
    public Action PlayerDestroyedCallBack;

    #endregion


    #region Properties



    #endregion


    #region UnityMethods

    void Start()
    {
        PlayerCreatedCallBack = () => _playerController = new PlayerController();
        PlayerDestroyedCallBack = () => _playerController = null;
        _uIController = Instantiate(_uIController);
        _netManager = FindObjectOfType<NetworkManager>();
        _delayedActionController = new DelayedActionController();
    }

    void Update()
    {
        Execute();
    }

    #endregion


    #region Methods

    private void Execute()
    {
        _playerController?.Execute(); // ?. rude
        _delayedActionController.Execute();
    }

    public void QuitApplication()
    {
        Application.Quit(0);
    }

    public void Disconnect()
    {
        if (NetworkServer.active)
            NetworkServer.Shutdown();
        _client.Disconnect();
        _uIController.CloseGameMenu();
        _uIController.ShowMainMenu();
    }

    public void Connect(string ip, int port)
    {
        _netManager.networkAddress = ip;
        _netManager.networkPort = port;
        _client = _netManager.StartClient();
    }

    public void Host()
    {
        _client = _netManager.StartHost();
    }

    public void PlayerOnCreatedCallBack()
    {
        PlayerCreatedCallBack.Invoke();
    }

    public void PlayerOnDestroyedCallBack()
    {
        PlayerDestroyedCallBack.Invoke();
    }

    #endregion
}
