using System;
using System.Collections.Generic;
using UnityEngine;


public class InMainMenuPlayerState : PlayerState
{
    #region Fields

    private UIController _ui;

    #endregion


    #region ClassLifeCycles

    public InMainMenuPlayerState(PlayerStateMachine stateMachine, Dictionary<FeatureType, BasePlayerFeature> featureTable, UIController ui)
    {
        _featureTable = featureTable;
        _stateMachine = stateMachine;
        _ui = ui;
    }

    #endregion


    #region Methods

    public override void Execute()
    {
    }

    #endregion
}