using System;
using System.Collections.Generic;


public class PlayerStateMachine
{
	#region Fields

	private PlayerState _state;
	private Dictionary<Type, IPlayerFeature> _playerFeatures;
	
	#endregion
	
	
	#region Properties
	
	
	
	#endregion
	
	
	#region ClassLifeCycles
	
	public PlayerStateMachine(Dictionary<Type, IPlayerFeature> playerfeatures)
    {
		_playerFeatures = _playerFeatures;
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
