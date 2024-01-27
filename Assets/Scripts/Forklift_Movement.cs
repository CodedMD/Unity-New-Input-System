using System.Collections;
using System.Collections.Generic;
using Game.Scripts.UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class Forklift_Movement : MonoBehaviour
{
    private PlayerControls _input;
    [SerializeField] private The_UI_Manager _uiManager;

    [SerializeField] private PlayerMovement _player;
   // [SerializeField] private MakeChild _child;
    [SerializeField] private GameObject _forks;
    [SerializeField] private GameObject _driver;
    [SerializeField] private GameObject _playerObj;
    private float _lowerLiftLimit = 0.6f;
    private float _raiseLiftLimit = 2.25f;
    private bool _isMoving = false;


    // Start is called before the first frame update
    void Start()
    {
        //  _forkLift = GetComponent<Lift_Forks>();
        _input = new PlayerControls();
        _input.Forklift.Enable();
        _driver.SetActive(true);
        _playerObj.SetActive(false);
    }

 

    // Update is called once per frame
    void Update()
    {
        if (_isMoving== true)
        {
            MoveThatForklift();
            MovingForks();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            _player.CamSwith0();
            _input.Enable();
            _playerObj.SetActive(true);
            _driver.SetActive(false);
            _uiManager.ControlsNotVisable();
            _isMoving = false;

            _player.ForkLiftUpdate();
        }

    }

    public void ForkLiftActive()
    {
        _driver.SetActive(true);
        _playerObj.SetActive(false);
        _isMoving = true;
    }


    public void MoveThatForklift()
    {
        var move = _input.Forklift.movement.ReadValue<Vector2>();
        var moveRotate = new Vector3(0, 0, move.y);
        transform.Translate(moveRotate * Time.deltaTime * 3.0f);
        transform.Rotate(0,move.x,0);



    }



    private void MovingForks()
    {
        

        var liftForks = _input.Forklift.LiftForks.ReadValue<float>();
        Vector3 yPos = _forks.transform.localPosition;
        yPos.y = Mathf.Clamp(yPos.y, _lowerLiftLimit, _raiseLiftLimit);
        _forks.transform.localPosition = yPos;
        _forks.transform.Translate((Vector3.up * Time.deltaTime * 3.0f * liftForks), transform.parent);

       
    }

 



}
