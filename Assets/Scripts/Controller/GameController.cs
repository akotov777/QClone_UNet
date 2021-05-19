using UnityEngine;
using UnityEngine.Networking;


public class GameController : MonoBehaviour
{
	#region Fields

	[SerializeField] private PlayerController _playerController;
	[SerializeField] private UIController _uIController;
	
	#endregion
	
	
	#region Properties
	
	
	
	#endregion
	
	
	#region UnityMethods
	
	void Start()
    {
		_uIController = Instantiate(_uIController);
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
