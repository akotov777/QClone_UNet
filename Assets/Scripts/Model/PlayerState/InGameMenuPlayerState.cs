using System;
using System.Collections.Generic;
using UnityEngine;


public class InGameMenuPlayerState : PlayerState
{
    #region Fields

    private UIController _ui;

    #endregion


    #region ClassLifeCycles

    public InGameMenuPlayerState(PlayerStateMachine stateMachine, Dictionary<FeatureType, BasePlayerFeature> featureTable, UIController ui)
    {
        _featureTable = featureTable;
        _stateMachine = stateMachine;
        _ui = ui;
    }

    #endregion


    #region Methods

    public override void Execute()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Inputs.Movement.IsActive = true;
            Inputs.Looking.IsActive = true;
            Inputs.Firing.IsActive = true;
            Cursor.lockState = CursorLockMode.Locked;

            _ui.CloseGameMenu();
            ChangeStateToInGame();
        }
    }

    public void ChangeStateToInGame()
    {
        _stateMachine.SetState(_stateMachine.InGame);
    }

    #endregion
}