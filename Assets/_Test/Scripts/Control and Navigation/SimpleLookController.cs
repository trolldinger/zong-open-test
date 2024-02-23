using UnityEngine;
using UnityEngine.InputSystem;

public class SimpleLookController : MonoBehaviour
{

    [Header("Controller Settings")]
    [SerializeField]private GameObject _cineMachineTarget;
    [SerializeField]private PlayerInput _playerInput;

    [Header("Look Settings")]
    [SerializeField]private float _rotationSpeed = 1.0f;
	[SerializeField]private float _topClamp = 90.0f;
	[SerializeField]private float _bottomClamp = -90.0f;

    private bool _canControl=true;
    private float _cinemachineTargetPitch;
    private float _rotationVelocity;
    private Vector2 _currentLook;
    private InteractableItem _hoveredItem;

    private const float _threshold = 0.01f;

    private bool IsCurrentDeviceMouse
    {
        get
        {
            #if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
            return _playerInput.currentControlScheme == "KeyboardMouse";
            #else
            return false;
            #endif
        }
    }

    private void Update()
    {
        //Raycast and find interactable objects
        if(!_canControl)
            return;
        if(Physics.Raycast(Camera.main.transform.position,Camera.main.transform.forward, out RaycastHit hit))
        {
            InteractableItem item = hit.collider.GetComponent<InteractableItem>();
            
            if(item != null)
            {
                if(!item.CanInteract)
                    return;
                item.ShowItem();
                _hoveredItem=item;
            }
            else
            {
                if(_hoveredItem != null)
                {
                    _hoveredItem.HideItem();
                    _hoveredItem = null;
                }
            }
        }
    }

    private void LateUpdate()
    {
        CameraRotation();
    }

    private void CameraRotation()
    {
        // if there is an input
        if (_currentLook.sqrMagnitude >= _threshold)
        {
            //Don't multiply mouse input by Time.deltaTime
            float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;
            
            _cinemachineTargetPitch += _currentLook.y * _rotationSpeed * deltaTimeMultiplier;
            _rotationVelocity = _currentLook.x * _rotationSpeed * deltaTimeMultiplier;

            // clamp our pitch rotation
            _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, _bottomClamp, _topClamp);

            // Update Cinemachine camera target pitch
            _cineMachineTarget.transform.localRotation = Quaternion.Euler(_cinemachineTargetPitch, 0.0f, 0.0f);

            // rotate the player left and right
            transform.Rotate(Vector3.up * _rotationVelocity);
        }
    }

    private float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }

    public void OnLook(InputValue inputValue)
    { 
        _currentLook = inputValue.Get<Vector2>();
    }

    public void OnInteract(InputValue inputValue)
    {
        //Make interact button skip cinemachine animation?
        if(!_canControl)
            return;

        //Pick or give the item if is hovered
        if(inputValue.isPressed && _hoveredItem)
        {
            if(_hoveredItem is ItemReceiver receiver)
                receiver.PickItem();
            else
                _hoveredItem.PickItem();
        }
    }

    public void SetControl(bool playerControlEnabled)
    {
        _canControl = playerControlEnabled;
    }
}