using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Scripts.LiveObjects;
using Cinemachine;
using UnityEngine.Animations;

namespace Game.Scripts.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerControl : MonoBehaviour
    {



        private CharacterController _controller;
        [SerializeField]
        private Animator _anim;
        [SerializeField]
        private float _speed;
        private bool _playerGrounded;
        [SerializeField]
        private Detonator _detonator;
        private bool _canMove = true;
        [SerializeField]
        private CinemachineVirtualCamera _followCam;
        [SerializeField]
        private GameObject _model;
        [SerializeField]
        private PlayerInteractionInput _input;


        private void OnEnable()
        {
            InteractableZone.onZoneInteractionComplete += InteractableZone_onZoneInteractionComplete;
            Laptop.onHackComplete += ReleasePlayerControl;
            Laptop.onHackEnded += ReturnPlayerControl;
            Forklift.onDriveModeEntered += ReleasePlayerControl;
            Forklift.onDriveModeExited += ReturnPlayerControl;
            Forklift.onDriveModeEntered += HidePlayer;
            Drone.OnEnterFlightMode += ReleasePlayerControl;
            Drone.onExitFlightmode += ReturnPlayerControl;
            _input = new PlayerInteractionInput();
            _input.Player.Enable();
            _input.Player.Move.started += Move_started;
            _input.Player.Move.canceled += Move_canceled;
            _anim.gameObject.SetActive(true);


        }

        private void Move_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            _anim.SetFloat("Speed", 0);

        }

        private void Move_started(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {

            var velocity = transform.forward;
            _anim.SetFloat("Speed", Mathf.Abs(velocity.magnitude));
            _playerGrounded = _controller.isGrounded;

            if (_playerGrounded)
            {
                velocity.y = 0f;
            }
            if (!_playerGrounded)
            {
                velocity.y += -20f * Time.deltaTime;
            }

            _controller.Move(velocity * Time.deltaTime);
        }

        private void Start()
        {
            _controller = GetComponent<CharacterController>();

            if (_controller == null)
                Debug.LogError("No Character Controller Present");

            _anim = GetComponentInChildren<Animator>();

            if (_anim == null)
                Debug.Log("Failed to connect the Animator");
        }

        private void Update()
        {
            if (_canMove == true)
                CalcutateMovement();

        }
        public void DisableAnimation()
        {
            _anim.gameObject.SetActive(false);
        }

        private void CalcutateMovement()
        {
            var move = _input.Player.Move.ReadValue<Vector2>();
            var velocity = transform.forward;
            var moveRotate = new Vector3(0, 0, move.y);
            transform.Translate(moveRotate * Time.deltaTime * 3.0f);
            transform.Rotate(0, move.x, 0);

        }

        private void InteractableZone_onZoneInteractionComplete(InteractableZone zone)
        {
            switch(zone.GetZoneID())
            {
                case 1: //place c4
                    _detonator.Show();
                    break;
                case 2: //Trigger Explosion
                    TriggerExplosive();
                    break;
            }
        }

        private void ReleasePlayerControl()
        {
            _canMove = false;
            _followCam.Priority = 9;
        }

        private void ReturnPlayerControl()
        {
            _model.SetActive(true);
            _canMove = true;
            _followCam.Priority = 10;
        }

        private void HidePlayer()
        {
            _model.SetActive(false);
        }
               
        private void TriggerExplosive()
        {
            _detonator.TriggerExplosion();
        }

        private void OnDisable()
        {
            _input.Player.Move.Disable();
            _anim.SetFloat("Speed", 0);
            _canMove = false;

            InteractableZone.onZoneInteractionComplete -= InteractableZone_onZoneInteractionComplete;
            Laptop.onHackComplete -= ReleasePlayerControl;
            Laptop.onHackEnded -= ReturnPlayerControl;
            Forklift.onDriveModeEntered -= ReleasePlayerControl;
            Forklift.onDriveModeExited -= ReturnPlayerControl;
            Forklift.onDriveModeEntered -= HidePlayer;
            Drone.OnEnterFlightMode -= ReleasePlayerControl;
            Drone.onExitFlightmode -= ReturnPlayerControl;
           
        }

    }
}

