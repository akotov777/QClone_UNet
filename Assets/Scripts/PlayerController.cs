using UnityEngine;
using UnityEngine.Networking;
using System;


public sealed class PlayerController : NetworkBehaviour
{
    #region Fields

    private CharacterController _characterController;
    private SpawnerPool _pool;
    private IPlayerFeature[] _features;

    [SerializeField] private GameObject _objToSpawn;
    [SerializeField] private Transform _positionToSpawn;
    [SerializeField] private float _speed = 6.0f;
    [SerializeField] private float _jumpSpeed = 8.0f;
    [SerializeField] private float _gravity = 20.0f;
    private Vector3 _moveDirection = Vector3.zero;
    private Quaternion _characterTargetRotate;
    private Quaternion _cameraTargetRotate;
    private Camera _camera;

    public float XSensitivity = 2f;
    public float YSensitivity = 2f;
    public float MinimumX = -90F;
    public float MaximumX = 90F;

    #endregion


    #region UnityMethods

    private void Start()
    {
        CommonInitialization();
        if (isLocalPlayer)
            LocalInitialization();
        if (!isLocalPlayer)
            OtherClientsInitialization();
    }

    private void Update()
    {
        if (!isLocalPlayer) return;

        for (int i = 0; i < _features.Length; i++)
        {
            _features[i].ExecuteFeature();
        }


        if (_characterController.isGrounded)
        {
            Vector3 desiredMove = transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");
            _moveDirection = desiredMove * _speed;

            if (Input.GetButton("Jump"))
            {
                _moveDirection.y = _jumpSpeed;
            }
        }

        _moveDirection.y -= _gravity * Time.deltaTime;

        _characterController.Move(_moveDirection * Time.deltaTime);

        LookRotation(_characterController.transform, _camera.transform);

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (_objToSpawn.HasComponent<IPoolable>())
            {
                Debug.Log("True");
            }
            else
            {
                Debug.Log("False");
            }
        }
    }

    #endregion


    #region Methods

    private void LookRotation(Transform character, Transform camera)
    {
        float yRot = Input.GetAxis("Mouse X") * XSensitivity;
        float xRot = Input.GetAxis("Mouse Y") * YSensitivity;

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

        angleX = Mathf.Clamp(angleX, MinimumX, MaximumX);

        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }

    private void CommonInitialization()
    {
        _characterController = gameObject.GetComponent<CharacterController>();
        _camera = gameObject.GetComponentInChildren<Camera>();

        _features = new IPlayerFeature[1];
        NetworkServices netServices = gameObject.GetComponent<NetworkServices>();
        _pool = FindObjectOfType<SpawnerPool>();

        _features[0] = new FiringFeature(_objToSpawn, _positionToSpawn, _camera.transform, netServices);

        netServices.Pool = _pool;
    }

    private void LocalInitialization()
    {
        _characterTargetRotate = _camera.transform.localRotation;
        _cameraTargetRotate = _camera.transform.localRotation;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OtherClientsInitialization()
    {
        _camera.enabled = false;
        gameObject.GetComponentInChildren<AudioListener>().enabled = false;
    }

    #endregion
}
