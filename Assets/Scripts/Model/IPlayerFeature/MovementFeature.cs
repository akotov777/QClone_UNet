using UnityEngine;


public class MovementFeature : BasePlayerFeature
{
    #region Fields

    private CharacterController _characterController;
    private Camera _camera;
    private PlayerMovementSettings _settings;
    private Vector3 _moveDirection = Vector3.zero;
    private Quaternion _characterTargetRotate;
    private Quaternion _cameraTargetRotate;

    #endregion


    #region Properties



    #endregion


    #region ClassLifeCycles

    public MovementFeature(Camera camera, CharacterController characterController)
    {
        _camera = camera;
        _characterController = characterController;
        _settings = Resources.Load<PlayerMovementSettings>("Data/PlayerMovementSettings");

        _characterTargetRotate = _camera.transform.localRotation;
        _cameraTargetRotate = _camera.transform.localRotation;
        Cursor.lockState = CursorLockMode.Locked;
    }

    #endregion


    #region Methods

    private void LookRotation(Transform character, Transform camera)
    {
        float yRot = Input.GetAxis("Mouse X") * _settings.XSensitivity;
        float xRot = Input.GetAxis("Mouse Y") * _settings.YSensitivity;

        _characterTargetRotate *= Quaternion.Euler(0f, yRot, 0f);
        _cameraTargetRotate *= Quaternion.Euler(-xRot, 0f, 0f);

        _cameraTargetRotate = ClampRotationAroundXAxis(_cameraTargetRotate);

        character.localRotation = _characterTargetRotate;
        camera.localRotation = _cameraTargetRotate;
    }

    private Quaternion ClampRotationAroundXAxis(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

        angleX = Mathf.Clamp(angleX, _settings.MinimumX, _settings.MaximumX);

        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }

    #endregion


    #region IPlayerFeature

    public override void ExecuteFeature()
    {
        if (!IsActive)
            return;

        if (_characterController.isGrounded)
        {
            Vector3 desiredMove = _characterController.transform.forward * Input.GetAxis("Vertical") 
                                  + _characterController.transform.right * Input.GetAxis("Horizontal");

            _moveDirection = desiredMove * _settings.Speed;

            if (Input.GetButton("Jump"))
            {
                _moveDirection.y = _settings.JumpSpeed;
            }
        }

        _moveDirection.y -= _settings.Gravity * Time.deltaTime;

        _characterController.Move(_moveDirection * Time.deltaTime);

        LookRotation(_characterController.transform, _camera.transform);
    }

    #endregion
}
