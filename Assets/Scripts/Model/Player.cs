using UnityEngine.Networking;
using UnityEngine;


public class Player : NetworkBehaviour
{
	#region Fields

	[SerializeField] private Camera _camera;
	
	#endregion
	
	
	#region Properties
	
	public Camera Camera { get { return _camera; } }
	
	#endregion
	
	
	#region UnityMethods
	
	void Start()
    {
		_camera = GetComponentInChildren<Camera>();
    }

    void Update()
    {
        
    }
	
	#endregion
	
	
	#region Methods
	
	
	
	#endregion
}
