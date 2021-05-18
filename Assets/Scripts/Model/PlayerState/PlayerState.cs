using System;
using UnityEngine;


public abstract class PlayerState
{
	#region Fields

	private PlayerStateMachine _stateMachine;

	#endregion


	#region Methods

	public abstract void Execute();
	
	#endregion
}
