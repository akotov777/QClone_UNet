using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Networking;


public class MainMenu : BaseUI
{
    #region Fields

    [SerializeField] private Button _button_Exit;
    [SerializeField] private Button _button_Connect;
    [SerializeField] private Button _button_Host;
    [SerializeField] private InputField _field_IP;
    [SerializeField] private InputField _field_Port;

    private UnityAction _connect;
    private UnityAction _exit;
    private UnityAction _host;

    #endregion


    #region Properties



    #endregion


    #region UnityMethods

    internal override void Start()
    {
        base.Start();
        ToggleOn();

        _connect = ConnectWrapper;
        _connect += ToggleOff;
        _connect += _uIController.ShowHUD;
        _exit = QuitWrapper;
        _host = HostWrapper;
        _host += ToggleOff;
        _host += _uIController.ShowHUD;
        _button_Connect.onClick.AddListener(_connect);
        _button_Exit.onClick.AddListener(_exit);
        _button_Host.onClick.AddListener(_host);
    }

    #endregion


    #region Methods

    private void ConnectWrapper()
    {
        _gameController.Connect(_field_IP.text, int.Parse(_field_Port.text));
    }

    private void QuitWrapper()
    {
        _gameController.QuitApplication();
    }

    private void HostWrapper()
    {
        _gameController.Host();
    }

    #endregion
}
