﻿using System;
using System.Collections.Generic;
using UnityEngine;


public class InGamePlayerState : PlayerState
{
    #region Fields

    private UIController _ui;

    #endregion


    #region ClassLifeCycles

    public InGamePlayerState(PlayerStateMachine stateMachine, Dictionary<FeatureType, BasePlayerFeature> featureTable,  UIController ui)
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
            _featureTable[FeatureType.FiringFeature].IsActive = false;
            _featureTable[FeatureType.MovementFeature].IsActive = false;
            _featureTable[FeatureType.LookingFeature].IsActive = false;
            Cursor.lockState = CursorLockMode.None;

            _ui.ShowGameMenu();
            _stateMachine.SetState(_stateMachine.InGameMenu);
        }
    }

    #endregion
}