using UnityEngine;
using UnityEngine.Networking;


public class GameController : MonoBehaviour
{
	#region Fields

	[SerializeField] private PlayerController _playerController;
	[SerializeField] private UIController _uIController;

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
    }

    void Update()
    {
		Execute();
    }
	
	#endregion
	
	
	#region Methods
	
	private void Execute()
    {
		//_playerController.Execute();
    }

	public void QuitApplication()
	{
		Application.Quit(0);
	}

	public void Disconnect()
	{
		_client.Disconnect();
	}
	public void Connect(string ip, int port)
	{
		_client = new NetworkClient();
		_client.Connect(ip, port);
	}

	public void Host()
	{
		_client = _netManager.StartHost();
	}

	#endregion
}
