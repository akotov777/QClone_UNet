using UnityEngine;


public class UIController : MonoBehaviour
{
	#region Fields

	[SerializeField] private BaseUI _mainMenu;
	[SerializeField] private BaseUI _gameMenu;

	#endregion


	#region Properties



	#endregion


	#region UnityMethods

	void Start()
	{
		ShowMainMenu();
		CloseGameMenu();
	}

	void Update()
	{

	}

	#endregion


	#region Methods

	public void ShowMainMenu()
	{
		_mainMenu.ToggleOn();
	}

	public void ShowGameMenu()
	{
		_gameMenu.ToggleOn();
	}

	public void CloseMainMenu()
    {
		_mainMenu.ToggleOff();
    }

	public void CloseGameMenu()
    {
		_gameMenu.ToggleOff();
    }


	#endregion
}
