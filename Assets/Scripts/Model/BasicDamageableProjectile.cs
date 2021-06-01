using UnityEngine.Networking;
using UnityEngine;
using System.Collections;


public class BasicDamageableProjectile : NetworkBehaviour, IPoolable, ISpawnSetUpable
{
    #region Fields

    [SerializeField] private float _lifeTime;
    [SerializeField] private float _speed;
    [SerializeField, Range(1, 100)] private int _dealingDamage;
    [SerializeField, Range(1, 20)] private float _splashRadius;
    [SerializeField] private ParticleSystem _particleSystem;
    private bool _isInPool;
    private Rigidbody _rigidBody;
    [SyncVar]
    private Vector3 _direction;

    #endregion


    #region UnityMethods

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    [ServerCallback]
    private void OnEnable()
    {
        StartCoroutine(LifeTimeDestroy());
    }

    [ServerCallback]
    private void OnDisable()
    {
        StopCoroutine(LifeTimeDestroy());
    }

    [ServerCallback]
    void Update()
    {
    }

    [ServerCallback]
    private void OnCollisionEnter(Collision collision)
    {
        RpcDoDamage();
        StartCoroutine(PlayParticleAndUnSpawn());
    }

    #endregion


    #region Methods

    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
    }

    private void DoDamage()
    { 
        var info = new CollisionInfo();
        info.IntDamage = _dealingDamage;

        var colliders = Physics.OverlapSphere(transform.position, _splashRadius);
        foreach (var coll in colliders)
        {
            if (coll.gameObject.HasComponent<Player>())
            {
                coll.GetComponent<Player>().Collider.HandleCollision(info);
            }
        }
    }

    private void PlayParticle()
    {
        GetComponent<Renderer>().enabled = false;
        _particleSystem.Play();
    }

    [ClientRpc]
    private void RpcPlayParticle()
    {
        PlayParticle();
    }

    private IEnumerator PlayParticleAndUnSpawn()
    {
        _rigidBody.velocity = Vector3.zero;
        StopCoroutine(LifeTimeDestroy());
        RpcPlayParticle();
        yield return new WaitForSeconds(_particleSystem.main.startLifetime.constant);
        UnSpawn();
    }

    [ClientRpc, ClientCallback]
    public void RpcDoDamage()
    {
        DoDamage();
    }

    private void UnSpawn()
    {
        ReturnToPool();
        NetworkServer.UnSpawn(gameObject);
    }

    private IEnumerator LifeTimeDestroy()
    {
        yield return new WaitForSeconds(_lifeTime);
        UnSpawn();
    }

    #endregion


    #region IPoolable

    public bool IsInPool { get => _isInPool; }

    public GameObject GetFromPool()
    {
        gameObject.SetActive(true);
        GetComponent<Renderer>().enabled = true;
        _isInPool = false;
        return gameObject;
    }

    public void ReturnToPool()
    {
        _direction = Vector3.zero;
        _rigidBody.velocity = Vector3.zero;
        gameObject.SetActive(false);
        _isInPool = true;
    }

    #endregion


    #region ISpawnSetUpable

    public void SpawnSetUp(SetUpSettings settings)
    {
        SetDirection(settings.direction.normalized);
        _rigidBody.velocity = _direction * _speed;
        gameObject.SetActive(true);
    }

    #endregion
}
