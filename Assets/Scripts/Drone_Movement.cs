using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;


    public class Drone_Movement : MonoBehaviour
    {
        private PlayerInteractionInput _input;
        [SerializeField] private InputActionAsset _playerMovement;
        [SerializeField] private PlayerMovement _player;
        private Animator _anim;
        [SerializeField] private TriggerPoints _trigger;
        [SerializeField] private The_UI_Manager _uiManager;
        [SerializeField] private Rigidbody _rb;
       // [SerializeField] private CinemachineConfiner _confiner;
        [SerializeField]
        private float _speed = 5f;

        private enum _playerMovementState { idle, Running }
        private bool _canFly = false;
        _playerMovementState state;


        // Start is called before the first frame update
        void Start()
        {

            _anim = GetComponent<Animator>();

            _input = new PlayerInteractionInput();
            _input.Drone.Enable();
            _input.Drone.Exit.performed += Exit_performed;



            _anim.SetInteger("state", (int)state);
            if(_input == null)
            {
                Debug.LogError("_input Is Null");
            }
            if(_player == null)
            {
                Debug.LogError("Player is null");
            }
        }
    private void Exit_performed(InputAction.CallbackContext obj)
    {

        _rb.useGravity = true;
        _player.CamSwith0();
        _playerMovement.Enable();
        _player.PlayerBackOnline();
        _uiManager.ControlsNotVisable();
        _canFly = false;
    }

    void Update()
        {
            PlayerBoundaries();
            if (_canFly == true)
            {
                DroneCanFly();
                _player.enabled = false;

                CalculateTilt();
               CalculateMovementUpdate();

            }
            else if (_canFly == false)
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
        }
        private void FixedUpdate()
        {
             _rb.AddForce(transform.up * (8.81f), ForceMode.Acceleration);
            if (_canFly)
                CalculateMovementFixedUpdate();
        }
        public void ForkliftKey()
        {
            _trigger.PlayerCanEnter();
        }


        private void CalculateMovementUpdate()
        {
            var moveRotate = _input.Drone.Rotation.ReadValue<float>();
            var tempRot = transform.localRotation.eulerAngles;
            tempRot.y -= _speed/3 * moveRotate;
            transform.localRotation = Quaternion.Euler(tempRot);
        
        }

        private void CalculateMovementFixedUpdate()
        {
            var moveUp = _input.Drone.ThrustUpDown.ReadValue<float>();
            _rb.AddForce(transform.up * moveUp * _speed, ForceMode.Acceleration);

        }

        private void CalculateTilt()
        {
            var move = _input.Drone.Movement.ReadValue<Vector2>();
            transform.Translate(new Vector3(move.x, 0, move.y) * Time.deltaTime * 2.0f);
            transform.Rotate(0, -move.x, 0);


            //controls the tilt
            if (move.x > 0)
                transform.rotation = Quaternion.Euler(00, transform.localRotation.eulerAngles.y, 15);
            else if (move.x < 0)
                transform.rotation = Quaternion.Euler(0, transform.localRotation.eulerAngles.y, -15);
            else if (move.y > 0)
                transform.rotation = Quaternion.Euler(15, transform.localRotation.eulerAngles.y, 0);
            else if (move.y < 0)
                transform.rotation = Quaternion.Euler(-15, transform.localRotation.eulerAngles.y, 0);
            else
                transform.rotation = Quaternion.Euler(00, transform.localRotation.eulerAngles.y, 0);

        }

        private void PlayerBoundaries()
        {
        if (transform.position.y >= 35)
        {
            transform.position = new Vector3(transform.position.x, 35, transform.position.z);
        }
        else if (transform.position.y <= 0.1f)
        {
            transform.position = new Vector3(transform.position.x, 0.1f, transform.position.z);
        }

        if (transform.position.x >= 65f)
        {
            transform.position = new Vector3(65f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x <= -118f)
        {
            transform.position = new Vector3(-118f, transform.position.y, transform.position.z);
        }
        if (transform.position.z >= 44)
        {
            transform.position = new Vector3(transform.position.x,transform.position.y, 44);
        }else if  (transform.position.z <= -4)
        {
            transform.position= new Vector3(transform.position.x, transform.position.y, -4);
        }

    }
    }