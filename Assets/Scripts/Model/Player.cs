using UnityEngine.Networking;
using UnityEngine;
using System;


public class Player : NetworkBehaviour
{
    #region Fields

    [SerializeField, Range(1, 200)] private int _maxHP;
    [SerializeField, Range(1, 200)] private int _maxArmor;
    [SyncVar] private int _healthPoints;
    [SyncVar] private int _armorPoints;

    [SerializeField] private Camera _camera;
    [SerializeField] private NetworkServices _netServices;

    [SerializeField] private GameObject _objToSpawn;
    [SerializeField] private Transform _positionToSpawn;
    [SerializeField] private Collider _collider;
    private CharacterController _characterController;

    public Action OnHealthZeroOrBelow;

    #endregion


    #region Properties

    public Camera Camera { get { return _camera; } }
    public NetworkServices NetworkServices { get { return _netServices; } }
    public GameObject Projectile { get { return _objToSpawn; } }
    public Transform PositionToSpawnProjectile { get { return _positionToSpawn; } }
    public Collider Collider { get { return _collider; } }
    public CharacterController CharacterController { get { return _characterController; } }
    public int HP
    {
        get { return _healthPoints; }
        set
        {
            if (value <= 0)
            {
                OnHealthZeroOrBelow.Invoke();
                _healthPoints = 0;
            }
            else if(value > _maxHP)
            {
                _healthPoints = _maxHP;
            }
            else
            {
                _healthPoints = value;
            }
        }
    }
    public int Armor { get { return _armorPoints; } }

    #endregion


    #region UnityMethods

    void Start()
    {
        CommonInitialization();
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
        _healthPoints = _maxHP;
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
