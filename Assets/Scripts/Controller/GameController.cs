using UnityEngine;
using UnityEngine.Networking;


public class GameController : MonoBehaviour
{
    #region Fields

    [SerializeField] private PlayerController _playerController;
    [SerializeField] private UIController _uIController;
    private RepeatableActionController _repeatableActionController;

    [SerializeField] private NetworkManager _netManager;
    private NetworkClient _client;

    #endregion


    #region Properties



    #endregion


    #region UnityMethods

    void Start()
    {
        _uIController = Instantiate(_uIController);
        _netManager = FindObjectOfType<NetworkManager>();
        _repeatableActionController = new RepeatableActionController();
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
        _repeatableActionController.Execute();
    }

    public void QuitApplication()
    {
        Application.Quit(0);
    }

    public void Disconnect()
    {
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

    public void SetUpPlayerController()
    {
        _playerController = new PlayerController();
    }

    #endregion
}
