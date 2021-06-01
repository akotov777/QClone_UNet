using UnityEngine.Networking;
using UnityEngine;


public class BasicDamageableProjectile : NetworkBehaviour, IPoolable, ISpawnSetUpable
{
    #region Fields

    [SerializeField] private float _lifeTime;
    [SerializeField] private float _speed;
    private bool _isInPool;
    [SyncVar]
    private Vector3 _direction;

    #endregion


    #region UnityMethods

    void Update()
    {
        transform.position += _direction * _speed * Time.deltaTime;
    }

    [Server]
    private void OnCollisionEnter(Collision collision)
    {
        RpcDoDamage();
        DoDamage();
        ReturnToPool();
        NetworkServer.UnSpawn(gameObject);
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
        info.IntDamage = 10;

        var colliders = Physics.OverlapSphere(transform.position, 10f);
        foreach (var coll in colliders)
        {
            if (coll.gameObject.HasComponent<Player>())
            {
                coll.GetComponent<Player>().Collider.HandleCollision(info);
            }
        }
    }

    [ClientRpc]
    public void RpcDoDamage()
    {
        DoDamage();
    }

    #endregion


    #region IPoolable

    public bool IsInPool { get => _isInPool; }

    public GameObject GetFromPool()
    {
        gameObject.SetActive(true);
        _isInPool = false;
        return gameObject;
    }

    public void ReturnToPool()
    {
        _direction = Vector3.zero;
        gameObject.SetActive(false);
        _isInPool = true;
    }

    #endregion


    #region ISpawnSetUpable

    public void SpawnSetUp(SetUpSettings settings)
    {
        SetDirection(settings.direction.normalized);
        gameObject.SetActive(true);
    }

    #endregion
}
