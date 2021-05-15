using UnityEngine;


public interface IPoolable
{
    bool IsInPool { get;}
    GameObject GetFromPool();
    void ReturnToPool();
}

