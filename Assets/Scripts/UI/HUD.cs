using System;
using UnityEngine;
using UnityEngine.UI;


public class HUD : BaseUI
{
	#region Fields

	[SerializeField] private Text _armorText;
	[SerializeField] private Text _healthText;
    private Player _player;

    #endregion


    #region UnityMethods

    internal override void Start()
    {
        base.Start();
        _gameController.PlayerCreatedCallBack += InitializeWithPlayer;
    }

    #endregion


    #region Methods

    private void FindLocalPlayerOnScene()
    {
        var players = GameObject.FindObjectsOfType<Player>();
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].isLocalPlayer)
            {
                _player = players[i];
            }
        }
        _player.OnArmorChanged += UpdateText;
        _player.OnHPChanged += UpdateText;
    }

    private void InitializeWithPlayer()
    {
        FindLocalPlayerOnScene();
        UpdateText();
    }

    private void UpdateText()
    {
        _armorText.text = _player.Armor.ToString();
        _healthText.text = _player.HP.ToString();
    }

    #endregion
}
