using System;
using UnityEngine;


public abstract class BaseUI : MonoBehaviour
{
    #region Fields



    #endregion


    #region Properties



    #endregion


    #region UnityMethods



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
