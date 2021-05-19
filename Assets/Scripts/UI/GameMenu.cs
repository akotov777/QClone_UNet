using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Networking;


public class GameMenu : BaseUI
{
    #region Fields

    [SerializeField] private Button _button_Exit;
    [SerializeField] private Button _button_Disconnect;

    private UnityAction _exit;
    private UnityAction _disconnect;

    #endregion


    #region Properties



    #endregion


    #region UnityMethods

    internal override void Start()
    {
        base.Start();
        _exit = _gameController.QuitApplication;
        _disconnect = _gameController.Disconnect;
        _button_Disconnect.onClick.AddListener(_disconnect);
        _button_Exit.onClick.AddListener(_exit);
    }

    #endregion


    #region Methods



    #endregion
}
