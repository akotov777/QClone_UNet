using UnityEngine.Networking;
using UnityEngine;


public class BasicProjectile : NetworkBehaviour, IPoolable, ISpawnSetUpable
{
    #region Fields

    [SerializeField] private float _lifeTime;
    [SerializeField] private float _speed;
    private bool _isInPool;
    [SyncVar]
    private Vector3 _direction;

    #endregion


    #region Properties



    #endregion


    #region UnityMethods

    void Start()
    {

    }


    void Update()
    {
        transform.position += _direction * _speed * Time.deltaTime;
    }

    private void OnEnable()
    {
        _direction.Normalize();
    }

    private void OnDisable()
    {
        ReturnToPool();
    }

    #endregion


    #region Methods

    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
    }
    
    #endregion


    #region IPoolable

    public bool IsInPool { get => _isInPool;}

    public GameObject GetFromPool()
    {
        _isInPool = false;
        return gameObject;
    }

    public void ReturnToPool()
    {
        gameObject.SetActive(false);
        _isInPool = true;
    }

    #endregion


    #region ISpawnSetUpable

    public void SpawnSetUp(SetUpSettings settings)
    {
        SetDirection(settings.direction);
        gameObject.SetActive(true);
    }

    #endregion
}
