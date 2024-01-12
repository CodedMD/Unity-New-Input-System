using System;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;
using Game.Scripts.Player;

namespace Game.Scripts.LiveObjects
{
    public class Forklift : MonoBehaviour
    {
        [SerializeField]
        private PlayerInteractionInput _input;
        [SerializeField]
        private GameObject _forks;
        [SerializeField]
        private GameObject _lift, _steeringWheel, _leftWheel, _rightWheel, _rearWheels;
        [SerializeField]
        private Vector3 _liftLowerLimit, _liftUpperLimit;
        [SerializeField]
        private float _speed = 5f, _liftSpeed = 1f;
        [SerializeField]
        private CinemachineVirtualCamera _forkliftCam;
        [SerializeField]
        private GameObject _driverModel;
        private bool _inDriveMode = false;
        [SerializeField]
        private InteractableZone _interactableZone;
        private float _lowerLiftLimit = 0.6f;
        private float _raiseLiftLimit = 2.25f;
        public static event Action onDriveModeEntered;
        public static event Action onDriveModeExited;

        private void OnEnable()
        {
            InteractableZone.onZoneInteractionComplete += EnterDriveMode;
            _input = new PlayerInteractionInput();
            _input.Forklift.Enable();
            _input.PlayerInteractions.Disable();
            _input.Forklift.Movement.started += Movement_started;   ;
            _input.Forklift.LiftLow.started += LiftLow_started; ;

        }

        private void LiftLow_started(InputAction.CallbackContext obj)
        {
        }

        private void Movement_started(InputAction.CallbackContext obj)
        {
        }

        private void EnterDriveMode(InteractableZone zone)
        {
            if (_inDriveMode !=true && zone.GetZoneID() == 5) //Enter ForkLift
            {
                _inDriveMode = true;
                _forkliftCam.Priority = 11;
                onDriveModeEntered?.Invoke();
                _driverModel.SetActive(true);
                _interactableZone.CompleteTask(5);
            }
        }

        private void ExitDriveMode()
        {
            _inDriveMode = false;
            _forkliftCam.Priority = 9;            
            _driverModel.SetActive(false);
            onDriveModeExited?.Invoke();
            
        }

        private void Update()
        {
            if (_inDriveMode == true)
            {

                LiftControls();
                CalcutateMovement();
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    ExitDriveMode();

                }
            }

        }

        private void CalcutateMovement()
        {
            var move = _input.Forklift.Movement.ReadValue<Vector2>();
            var moveRotate = new Vector3(0, 0, move.y);
            transform.Translate(moveRotate * Time.deltaTime * 3.0f);
            transform.Rotate(0, move.x, 0);
        }

        private void LiftControls()
        {
            var liftForks = _input.Forklift.LiftLow.ReadValue<float>();
            Vector3 yPos = _forks.transform.localPosition;
            yPos.y = Mathf.Clamp(yPos.y, _lowerLiftLimit, _raiseLiftLimit);
            _forks.transform.localPosition = yPos;
            _forks.transform.Translate((Vector3.up * Time.deltaTime * 3.0f * liftForks), transform.parent);
        }

       /* private void LiftUpRoutine()
        {
            if (_lift.transform.localPosition.y < _liftUpperLimit.y)
            {
                Vector3 tempPos = _lift.transform.localPosition;
                tempPos.y += Time.deltaTime * _liftSpeed;
                _lift.transform.localPosition = new Vector3(tempPos.x, tempPos.y, tempPos.z);
            }
            else if (_lift.transform.localPosition.y >= _liftUpperLimit.y)
                _lift.transform.localPosition = _liftUpperLimit;
        }

        private void LiftDownRoutine()
        {
            if (_lift.transform.localPosition.y > _liftLowerLimit.y)
            {
                Vector3 tempPos = _lift.transform.localPosition;
                tempPos.y -= Time.deltaTime * _liftSpeed;
                _lift.transform.localPosition = new Vector3(tempPos.x, tempPos.y, tempPos.z);
            }
            else if (_lift.transform.localPosition.y <= _liftUpperLimit.y)
                _lift.transform.localPosition = _liftLowerLimit;
        }*/

        private void OnDisable()
        {
            InteractableZone.onZoneInteractionComplete -= EnterDriveMode;
        }

    }
}