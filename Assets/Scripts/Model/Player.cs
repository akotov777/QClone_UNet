using UnityEngine.Networking;
using UnityEngine;
using System;


public class Player : NetworkBehaviour
{
    #region Fields

    [SerializeField, Range(1, 200)] private int _maxHP;
    [SerializeField, Range(1, 200)] private int _startHP;
    [SerializeField, Range(1, 200)] private int _maxArmor;
    [SerializeField, Range(0, 200)] private int _startArmor;
    [SyncVar] private int _healthPoints;
    [SyncVar] private int _armorPoints;

    [SerializeField] private Camera _camera;
    [SerializeField] private NetworkServices _netServices;

    [SerializeField] private GameObject _objToSpawn;
    [SerializeField] private Transform _positionToSpawn;
    [SerializeField] private Collider _collider;
    private CharacterController _characterController;

    public Action OnHealthZeroOrBelow;
    public Action OnHPChanged;
    public Action OnArmorChanged;

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
                CmdChangeHP(0);
                OnHealthZeroOrBelow.Invoke();
            }
            else if (value > _maxHP)
                CmdChangeHP(_maxHP);
            else
                CmdChangeHP(value);
            OnHPChanged.Invoke();
        }
    }
    public int StartHP { get { return _startHP; } }
    public int Armor
    {
        get { return _armorPoints; }
        set
        {
            if (value <= 0)
                _armorPoints = 0;
            else if (value > _maxHP)
                _armorPoints = _maxArmor;
            else
                _armorPoints = value;
            OnArmorChanged.Invoke();
        }
    }
    public int StartArmor { get { return _startArmor; } }

    #endregion


    #region UnityMethods

    void Start()
    {
        CommonInitialization();
        if (isLocalPlayer)
            LocalInitialization();
        if (!isLocalPlayer)
            OtherClientsInitialization();

        FindObjectOfType<GameController>().PlayerOnCreatedCallBack();
    }

    private void OnDestroy()
    {
        if (isLocalPlayer)
            FindObjectOfType<GameController>().PlayerOnDestroyedCallBack();
    }

    #endregion


    #region Methods

    private void CommonInitialization()
    {
        _netServices = gameObject.GetComponent<NetworkServices>();
        _characterController = gameObject.GetComponent<CharacterController>();
        _healthPoints = _startHP;
        _armorPoints = _startArmor;
        OnHealthZeroOrBelow = () => { };
    }

    private void LocalInitialization()
    {

    }

    private void OtherClientsInitialization()
    {
        gameObject.GetComponentInChildren<Camera>().enabled = false;
        gameObject.GetComponentInChildren<AudioListener>().enabled = false;
    }

    [Command]
    private void CmdChangeHP(int newHP)
    {
        _healthPoints = newHP;
    }

    #endregion
}
