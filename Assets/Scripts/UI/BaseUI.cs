using System;
using UnityEngine;


public abstract class BaseUI : MonoBehaviour
{
    #region Fields

    internal GameController _gameController;
    internal UIController _uIController;

    #endregion


    #region Properties



    #endregion


    #region UnityMethods

    internal virtual void Start()
    {
        _gameController = FindObjectOfType<GameController>();
        _uIController = FindObjectOfType<UIController>();
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
