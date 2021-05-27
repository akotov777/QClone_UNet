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

    private float GetVerticalAxis()
    {
        if (!IsActive)
            return 0.0f;
        return Input.GetAxis("Vertical");
    }

    private float GetHorizontalAxis()
    {
        if (!IsActive)
            return 0.0f;
        return Input.GetAxis("Horizontal");
    }

    private bool IsJumpPressed()
    {
        if (!IsActive)
            return false;
        return Input.GetButton("Jump");
    }


    #endregion


    #region IPlayerFeature

    public override void ExecuteFeature()
    {
        if (_characterController.isGrounded)
        {
            Vector3 desiredMove = _characterController.transform.forward * GetVerticalAxis()
                                  + _characterController.transform.right * GetHorizontalAxis();

            _moveDirection = desiredMove * _settings.Speed;

            if (IsJumpPressed())
            {
                _moveDirection.y = _settings.JumpSpeed;
            }
        }

        _moveDirection.y -= _settings.Gravity * Time.deltaTime;

        _characterController.Move(_moveDirection * Time.deltaTime);
    }

    #endregion
}
