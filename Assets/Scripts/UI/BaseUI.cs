using System;
using UnityEngine;


public abstract class BaseUI : MonoBehaviour
{
    #region Fields

    internal GameController _gameController;

    #endregion


    #region Properties



    #endregion


    #region UnityMethods

    internal virtual void Start()
    {
        _gameController = GetComponent<GameController>();
        ToggleOff();
    }

    #endregion


    #region Methods

    public void ToggleOn()
    {
        if (gameObject.activeInHierarchy)
            return;

        gameObject.SetActive(true);
    }

    public void ToggleOff()
    {
        if (!gameObject.activeInHierarchy)
            return;

        gameObject.SetActive(false);
    }

    #endregion
}
