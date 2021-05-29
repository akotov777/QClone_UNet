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
        //if (isClient)
        //    return;
        transform.position += _direction * _speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collide");

        if (collision.collider.gameObject.HasComponent<Player>())
        {
            Debug.Log("Collide Player");
            var info = new CollisionInfo();
            info.FloatDamage = 10;
            collision.collider.HandleCollision(info);
        }
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
