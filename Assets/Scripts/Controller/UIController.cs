using UnityEngine;


public class UIController : MonoBehaviour
{
	#region Fields

	[SerializeField] private BaseUI _mainMenu;
	[SerializeField] private BaseUI _gameMenu;
	[SerializeField] private BaseUI _hUD;

	#endregion


	#region Properties



	#endregion


	#region UnityMethods

	void Start()
	{
		ShowMainMenu();
		CloseGameMenu();
		CloseHUD();
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

	public void ShowHUD()
	{
		_hUD.ToggleOn();
	}

	public void CloseMainMenu()
    {
		_mainMenu.ToggleOff();
    }

	public void CloseGameMenu()
    {
		_gameMenu.ToggleOff();
    }

	public void CloseHUD()
	{
		_hUD.ToggleOff();
	}

	#endregion
}
