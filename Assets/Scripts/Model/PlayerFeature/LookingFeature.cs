using UnityEngine;


public class LookingFeature : ExecutablePlayerFeature
{
    #region Fields

    private Transform _cameraRotationFollower;
    private Camera _camera;
    private PlayerMovementSettings _settings;
    private Quaternion _followerTargetRotate;
    private Quaternion _cameraTargetRotate;

    #endregion


    #region Properties



    #endregion


    #region ClassLifeCycles

    public LookingFeature(Camera camera, Transform cameraRotationFollower)
    {
        _camera = camera;
        _cameraRotationFollower = cameraRotationFollower;
        _settings = Resources.Load<PlayerMovementSettings>("Data/PlayerMovementSettings");

        _followerTargetRotate = _camera.transform.localRotation;
        _cameraTargetRotate = _camera.transform.localRotation;
        Cursor.lockState = CursorLockMode.Locked;
    }

    #endregion


    #region Methods

    private void LookRotation(Transform camera, Transform cameraRotationFollower)
    {
        float yRot = Inputs.Looking.GetXAxis() * _settings.XSensitivity;
        float xRot = Inputs.Looking.GetYAxis() * _settings.YSensitivity;

        _followerTargetRotate *= Quaternion.Euler(0f, yRot, 0f);
        _cameraTargetRotate *= Quaternion.Euler(-xRot, 0f, 0f);

        _cameraTargetRotate = ClampRotationAroundXAxis(_cameraTargetRotate);

        cameraRotationFollower.localRotation = _followerTargetRotate;
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

    public override void ExecuteFeature()
    {
        if (!IsActive)
            return;

        LookRotation(_camera.transform, _cameraRotationFollower);
    }

    #endregion
}
