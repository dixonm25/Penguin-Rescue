using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public bool UsingOrbitalCamera {  get; private set; } = false;

    [SerializeField] PlayerInput _input;

    CinemachineVirtualCamera _activeCamera;
    int _activeCameraPriorityModifier = 31337;

    public Camera MainCamera;
    public CinemachineVirtualCamera cinemachine1stPerson;
    public CinemachineVirtualCamera cinemachine3rdPerson;
    public CinemachineVirtualCamera cinemachineOrbit;

    private void Start()
    {
        ChangeCamera(); // First time through, lets set the default camera.
    }

    private void Update()
    {
        if (_input.ChangeCameraWasPressedThisFrame)
        {
            ChangeCamera();
        }
    }
    
    private void ChangeCamera()
    {
        if (cinemachine3rdPerson == _activeCamera)
        {
            SetCameraPriorities(cinemachine3rdPerson, cinemachine1stPerson);
            UsingOrbitalCamera = false;
        }
        else if (cinemachine1stPerson == _activeCamera)
        {
            SetCameraPriorities(cinemachine1stPerson, cinemachineOrbit);
            UsingOrbitalCamera = true;
        }
        else if (cinemachineOrbit == _activeCamera)
        {
            SetCameraPriorities(cinemachineOrbit, cinemachine3rdPerson);
            _activeCamera = cinemachine3rdPerson;
            UsingOrbitalCamera = false;
        }
        else
        {
            cinemachine3rdPerson.Priority += _activeCameraPriorityModifier;
            _activeCamera = cinemachine3rdPerson;
        }
    }

    private void SetCameraPriorities(CinemachineVirtualCamera CurrentCameraMode, CinemachineVirtualCamera NewCameraMode)
    {
        CurrentCameraMode.Priority -= _activeCameraPriorityModifier;
        NewCameraMode.Priority += _activeCameraPriorityModifier;
        _activeCamera = NewCameraMode;
    }
}
