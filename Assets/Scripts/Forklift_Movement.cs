using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Forklift_Movement : MonoBehaviour
{
    private PlayerInteractionInput _input;
    [SerializeField] private InputActionAsset _playerMovement;
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _forks;
    private float _lowerLiftLimit = 0.6f;
    private float _raiseLiftLimit = 2.25f;


    // Start is called before the first frame update
    void Start()
    {
      //  _forkLift = GetComponent<Lift_Forks>();
        _input = new PlayerInteractionInput();
        _input.Forklift.Enable();
        _input.Forklift.Movement.started += Movement_started;
        _input.Forklift.LiftLow.started += LiftLow_started;

    }

    private void LiftLow_started(InputAction.CallbackContext obj)
    {


    }

    private void Movement_started(InputAction.CallbackContext obj)
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveThatDrone();


        MovingForks();
    }


    private void MoveThatDrone()
    {
        var move = _input.Forklift.Movement.ReadValue<Vector2>();
        var moveRotate = new Vector3(0, 0, move.y);
        transform.Translate(moveRotate * Time.deltaTime * 3.0f);
        transform.Rotate(0,move.x,0);
    }



    private void MovingForks()
    {
        

        var liftForks = _input.Forklift.LiftLow.ReadValue<float>();
        Vector3 yPos = _forks.transform.localPosition;
        yPos.y = Mathf.Clamp(yPos.y, _lowerLiftLimit, _raiseLiftLimit);
        _forks.transform.localPosition = yPos;
        _forks.transform.Translate((Vector3.up * Time.deltaTime * 3.0f * liftForks), transform.parent);
    
    }

 



}
