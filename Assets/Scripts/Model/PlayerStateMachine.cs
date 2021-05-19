using System;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStateMachine
{
    #region Fields

    private PlayerState _state;
    private Dictionary<FeatureType, BasePlayerFeature> _featuresTable;

    private InMainMenuPlayerState _inMainMenu;
    private InGameMenuPlayerState _inGameMenu;
    private InGamePlayerState _inGame;

    #endregion


    #region Properties

    public InMainMenuPlayerState InMainMenu { get => _inMainMenu; }
    public InGameMenuPlayerState InGameMenu { get => _inGameMenu; }
    public InGamePlayerState InGame { get => _inGame; }

    #endregion


    #region ClassLifeCycles

    public PlayerStateMachine(Dictionary<FeatureType, BasePlayerFeature> featuresTable)
    {
        _featuresTable = featuresTable;
        UIController ui = GameObject.FindObjectOfType<UIController>();
        _inGame = new InGamePlayerState(this, _featuresTable, ui);
        _inGameMenu = new InGameMenuPlayerState(this, _featuresTable, ui);
        _inMainMenu = new InMainMenuPlayerState(this, _featuresTable, ui);

        SetState(_inGame);
    }

    #endregion


    #region Methods

    public void Execute()
    {
        _state.Execute();
    }

    public void SetState(PlayerState state)
    {
        _state = state;
    }

    #endregion
}
