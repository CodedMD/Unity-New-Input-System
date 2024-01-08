using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Drone_Movement : MonoBehaviour
{
    private PlayerInteractionInput _input;
    [SerializeField] private InputActionAsset _playerMovement;
    [SerializeField] private Player _player;
    private Animator _anim;
    [SerializeField] private TriggerPoints _trigger;
    [SerializeField] private The_UI_Manager _uiManager;
    [SerializeField]private Rigidbody _rb;
    private enum _playerMovementState { idle, Running }
    private bool _canFly = false;
    _playerMovementState state;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;
        _anim = GetComponent<Animator>();
        _input = new PlayerInteractionInput();
        _input.Drone.Enable();
        _input.Drone.Movement.started += Movement_started;
        _input.Drone.Rotation.started += Rotation_performed;
        _input.Drone.ThrustUpDown.started += ThrustUp_performed;
        _anim.SetInteger("state", (int)state);
        if (_input == null)
        {
            Debug.LogError("_input Is Null");
        }
        if(_player == null)
        {
            Debug.LogError("Player is null");
        }
    }

    private void ThrustUp_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
     
    }

    private void ThrustDown_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
       
    }

    private void Rotation_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
    }

    private void Movement_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        
    }

    // Update is called once per frame
    void Update()
    {


       

        if (_canFly == true)
        {
            DroneCanFly();
            MoveThatDrone();
            ThrustUpDown();
            RotateThatDrone();
         
        }else if (_canFly == false)
        {
            
            _playerMovementState state;
            state = _playerMovementState.idle;
            _anim.SetInteger("state", (int)state);
        }
       
    }
 
    public void DroneCanFly()
    {
       _canFly = true;
        _input.PlayerInteractions.Disable();

        _playerMovementState state;
        state = _playerMovementState.Running;
        _anim.SetInteger("state", (int)state);

        //print("Drone Runs");
        if (Input.GetKeyDown(KeyCode.I))
        {
            _rb.useGravity = true;
            _player.CamSwith0();
            _playerMovement.Enable();
            _player.PlayerBackOnline();
            _uiManager.ControlsNotVisable();
            _canFly = false;

        }
    }

    public void ForkliftKey()
    {
        _trigger.PlayerCanEnter();
    }



    private void MoveThatDrone()
    {
        var move = _input.Drone.Movement.ReadValue<Vector2>();
        transform.Translate(new Vector3(move.x, 0, move.y) * Time.deltaTime * 3.0f);
    }
    private void RotateThatDrone()
    {
        var moveRotate = _input.Drone.Rotation.ReadValue<float>();
        transform.Rotate((Vector3.up * Time.deltaTime * 30f * moveRotate), Space.Self);
    }

    private void ThrustUpDown()
    {
        var moveUp = _input.Drone.ThrustUpDown.ReadValue<Vector2>();
        transform.Translate(new Vector3(0, moveUp.y, 0) * Time.deltaTime * 3.0f);

    }

}
