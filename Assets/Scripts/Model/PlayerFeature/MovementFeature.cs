using UnityEngine;


public class MovementFeature : ExecutablePlayerFeature
{
    #region Fields

    private CharacterController _characterController;
    private PlayerMovementSettings _settings;
    private Vector3 _moveDirection = Vector3.zero;

    #endregion


    #region Properties



    #endregion


    #region ClassLifeCycles

    public MovementFeature(CharacterController characterController)
    {
        _characterController = characterController;
        _settings = Resources.Load<PlayerMovementSettings>("Data/PlayerMovementSettings");
    }

    #endregion


    #region Methods

    public override void ExecuteFeature()
    {
        if (!IsActive)
            return;

        if (_characterController.isGrounded)
        {
            Vector3 desiredMove = _characterController.transform.forward * Inputs.Movement.GetVerticalAxis()
                                  + _characterController.transform.right * Inputs.Movement.GetHorizontalAxis();

            _moveDirection = desiredMove * _settings.Speed;

            if (Inputs.Movement.JumpButtonPressed())
            {
                _moveDirection.y = _settings.JumpSpeed;
            }
        }

        _moveDirection.y -= _settings.Gravity * Time.deltaTime;

        _characterController.Move(_moveDirection * Time.deltaTime);
    }

    #endregion
}
