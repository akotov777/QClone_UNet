using System;
using System.Collections.Generic;


public abstract class PlayerState
{
	#region Fields

	internal PlayerStateMachine _stateMachine;
	internal Dictionary<FeatureType, BasePlayerFeature> _featureTable;

	#endregion


	#region Methods

	public abstract void Execute();
	
	#endregion
}
