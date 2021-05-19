using System;
using System.Collections.Generic;


public class InGamePlayerState : PlayerState
{
    #region Fields

    private UIController _ui;

    #endregion


    #region ClassLifeCycles

    public InGamePlayerState(PlayerStateMachine stateMachine, Dictionary<Type, IPlayerFeature> featureTable,  UIController ui)
    {
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