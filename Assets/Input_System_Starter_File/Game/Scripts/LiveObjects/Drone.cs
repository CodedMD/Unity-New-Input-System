using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Game.Scripts.UI;
using UnityEngine.Windows;
using Game.Scripts.Player;

namespace Game.Scripts.LiveObjects
{
    public class Drone : MonoBehaviour
    {
        private enum Tilt
        {
            NoTilt, Forward, Back, Left, Right
        }

        [SerializeField]
        private Rigidbody _rigidbody;
        [SerializeField]
        private float _speed = 5f;
        private bool _inFlightMode = false;
        [SerializeField]
        private Animator _propAnim;
        [SerializeField]
        private CinemachineVirtualCamera _droneCam;
        [SerializeField]
        private InteractableZone _interactableZone;
        [SerializeField] private Rigidbody _rb;
        private PlayerInteractionInput _input;
        [SerializeField] private PlayerControl _player;



        public static event Action OnEnterFlightMode;
        public static event Action onExitFlightmode;

        private void OnEnable()
        {
            InteractableZone.onZoneInteractionComplete += EnterFlightMode;
            _rb = GetComponent<Rigidbody>();
            _rb.useGravity = false;
            _input = new PlayerInteractionInput();
            _input.Drone.Enable();
            _input.Player.Disable();
            _input.PlayerInteractions.Disable();
            _input.Player.Move.Disable();
            _input.Drone.Movement.started += Movement_started;
            _input.Drone.Rotation.started += Rotation_performed;
            _input.Drone.ThrustUpDown.started += ThrustUp_performed;
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

        private void EnterFlightMode(InteractableZone zone)
        {
            if (_inFlightMode != true && zone.GetZoneID() == 4) // drone Scene
            {
                _propAnim.SetTrigger("StartProps");
                _droneCam.Priority = 11;
                _inFlightMode = true;
                OnEnterFlightMode?.Invoke();
                UIManager.Instance.DroneView(true);
                _interactableZone.CompleteTask(4);
            }
        }

        private void ExitFlightMode()
        {            
            _droneCam.Priority = 9;
            _inFlightMode = false;
            UIManager.Instance.DroneView(false);            
        }

        private void Update()
        {
            if (_inFlightMode)
            {
                _player.enabled = false;

                CalculateTilt();
                CalculateMovementUpdate();

                if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
                {
                    _player.enabled = true;
                    _rb.useGravity = true;
                    _inFlightMode = false;
                    onExitFlightmode?.Invoke();
                    ExitFlightMode();
                }
            }
        }

        /*private void FixedUpdate()
        {
            _rigidbody.AddForce(transform.up * (9.81f), ForceMode.Acceleration);
            if (_inFlightMode)
                CalculateMovementFixedUpdate();
        }*/

        private void CalculateMovementUpdate()
        {
            var move = _input.Drone.Movement.ReadValue<Vector2>();
            transform.Translate(new Vector3(move.x, 0, move.y) * Time.deltaTime * 3.0f);
            var moveRotate = _input.Drone.Rotation.ReadValue<float>();
            transform.Rotate((Vector3.up * Time.deltaTime * 30f * moveRotate), Space.Self);
            var moveUp = _input.Drone.ThrustUpDown.ReadValue<Vector2>();
            transform.Translate(new Vector3(0, moveUp.y, 0) * Time.deltaTime * 3.0f);

            /* if (Input.GetKey(KeyCode.LeftArrow))
             {
                 var tempRot = transform.localRotation.eulerAngles;
                 tempRot.y -= _speed / 3;
                 transform.localRotation = Quaternion.Euler(tempRot);
             }
             if (Input.GetKey(KeyCode.RightArrow))
             {
                 var tempRot = transform.localRotation.eulerAngles;
                 tempRot.y += _speed / 3;
                 transform.localRotation = Quaternion.Euler(tempRot);
             }*/
        }

        private void CalculateMovementFixedUpdate()
        {
           


            /* if (Input.GetKey(KeyCode.Space))
             {
                 _rigidbody.AddForce(transform.up * _speed, ForceMode.Acceleration);
             }
             if (Input.GetKey(KeyCode.V))
             {
                 _rigidbody.AddForce(-transform.up * _speed, ForceMode.Acceleration);
             }*/
        }

        private void CalculateTilt()
        {

            



            /*if (Input.GetKey(KeyCode.A)) 
                transform.rotation = Quaternion.Euler(00, transform.localRotation.eulerAngles.y, 30);
            else if (Input.GetKey(KeyCode.D))
                transform.rotation = Quaternion.Euler(0, transform.localRotation.eulerAngles.y, -30);
            else if (Input.GetKey(KeyCode.W))
                transform.rotation = Quaternion.Euler(30, transform.localRotation.eulerAngles.y, 0);
            else if (Input.GetKey(KeyCode.S))
                transform.rotation = Quaternion.Euler(-30, transform.localRotation.eulerAngles.y, 0);
            else 
                transform.rotation = Quaternion.Euler(0, transform.localRotation.eulerAngles.y, 0);*/
        }
        /*private void MoveThatDrone()
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

        }*/

        private void OnDisable()
        {
            InteractableZone.onZoneInteractionComplete -= EnterFlightMode;
        }
    }
}
