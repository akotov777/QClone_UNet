using UnityEngine;


class DisableStrategy : IReturnToPoolStrategy
{
    #region Fields

    private GameObject _gameObject;

    #endregion


    #region ClassLifeCycles

    public DisableStrategy(GameObject gameObject)
    {
        _gameObject = gameObject;
    }

    #endregion


    #region IReturnToPoolStrategy

    public void ReturnToPool()
    {
        _gameObject.SetActive(false);
    }

    #endregion
}

