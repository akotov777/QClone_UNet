using System;
using System.Collections.Generic;


public class PlayerStateMachine
{
	#region Fields

	private PlayerState _state;
	private Dictionary<Type, BasePlayerFeature> _featuresTable;
	
	#endregion
	
	
	#region Properties
	
	
	
	#endregion
	
	
	#region ClassLifeCycles
	
	public PlayerStateMachine(Dictionary<Type, BasePlayerFeature> featuresTable)
    {
		_featuresTable = featuresTable;
    }
	
	#endregion
	
	
	#region Methods
	
	public void Execute()
    {
		//_state.Execute();
    }

	public void SetState(PlayerState state)
    {
		_state = state;
    }
	
	#endregion
}
