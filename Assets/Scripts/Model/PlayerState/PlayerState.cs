using System;
using System.Collections.Generic;


public abstract class PlayerState
{
	#region Fields

	internal PlayerStateMachine _stateMachine;
	internal Dictionary<Type, IPlayerFeature> _featureTable;

	#endregion


	#region Methods

	public abstract void Execute();
	
	#endregion
}
