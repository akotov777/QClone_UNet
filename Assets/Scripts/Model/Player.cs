using UnityEngine.Networking;
using UnityEngine;


public class Player : NetworkBehaviour
{
	#region Fields

	[SerializeField] private Camera _camera;
	[SerializeField] private NetworkServices _netServices;

	[SerializeField] private GameObject _objToSpawn;
	[SerializeField] private Transform _positionToSpawn;
	private CharacterController _characterController;

	#endregion


	#region Properties

	public Camera Camera { get { return _camera; } }
	public NetworkServices NetworkServices { get { return _netServices; } }
	public GameObject Projectile { get { return _objToSpawn; } }
	public Transform PositionToSpawnProjectile { get { return _positionToSpawn; } }
	public CharacterController CharacterController { get { return _characterController; } }

    #endregion


    #region UnityMethods

    private void Awake()
    {
		Debug.Log("Awake");
		CommonInitialization();
	}

	void Start()
    {
		if (isLocalPlayer)
			LocalInitialization();
		if (!isLocalPlayer)
			OtherClientsInitialization();
	}

    void Update()
    {
        
    }

	#endregion


	#region Methods

	private void CommonInitialization()
	{
		_netServices = gameObject.GetComponent<NetworkServices>();
		_characterController = gameObject.GetComponent<CharacterController>();
	}

	private void LocalInitialization()
	{
		FindObjectOfType<GameController>().SetUpPlayerController();
	}

	private void OtherClientsInitialization()
	{
		gameObject.GetComponentInChildren<Camera>().enabled = false;
		gameObject.GetComponentInChildren<AudioListener>().enabled = false;
	}

	#endregion
}
