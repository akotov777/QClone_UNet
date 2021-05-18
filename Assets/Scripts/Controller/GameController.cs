using UnityEngine;


public class GameController : MonoBehaviour
{
	#region Fields

	[SerializeField] private PlayerController _playerController;
	[SerializeField] private UIController _uIController;
	private NetworkServices _netServices;
	
	#endregion
	
	
	#region Properties
	
	
	
	#endregion
	
	
	#region UnityMethods
	
	void Start()
    {
        
    }

    void Update()
    {
        
    }
	
	#endregion
	
	
	#region Methods
	
	private void Execute()
    {
		_playerController.Execute();
    }
	
	#endregion
}
