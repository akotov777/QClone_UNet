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

    private NetworkClient _client;

    #endregion


    #region Properties



    #endregion


    #region UnityMethods

    private void Awake()
    {
        _connect = Connect;
        _exit = Exit;
        _host = Host;
        _button_Connect.onClick.AddListener(_connect);
        _button_Exit.onClick.AddListener(_exit);
        _button_Host.onClick.AddListener(_host);
    }

    #endregion


    #region Methods

    private void Connect()
    {
        NetworkClient v = new NetworkClient();
        string ip = _field_IP.text;
        int port = int.Parse(_field_Port.text);
        _client.Connect(ip, port);
    }

    private void Exit()
    {
        Application.Quit(0);
    }
    private void Host()
    {
        _client = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkManager>().StartHost();
        ToggleOff();
    }

    #endregion
}
