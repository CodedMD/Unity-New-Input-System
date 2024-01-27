using System.Collections;
using System.Collections.Generic;
using Game.Scripts.LiveObjects;
using Game.Scripts.UI;
using StarterAssets;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInteractionInput _input;

    [SerializeField] private InputActionAsset _playerMovement;
    [SerializeField] private Drone_Movement _drone;
    [SerializeField] private Forklift_Movement _forkLift;
    [SerializeField] private GameObject _forkliftPlayerposition;
    [SerializeField] private TriggerPoints _trigger;
    [SerializeField] private int _currentkey;
    [SerializeField] private YouWin _exit;
    [SerializeField] private Camera_manager _camManager;
    [SerializeField] private The_UI_Manager _uiManager;
    private Animator _anim;
    [SerializeField] private CrateBreak _crate;
    [SerializeField] private BoxCollider _punchCollider;
    [SerializeField] private CapsuleCollider _kickCollider;
    // Start is called before the first frame update
    void Start()
    {
        _punchCollider.enabled = false;
        _kickCollider.enabled = false;

        // _crate = GetComponent<CrateBreak>();
        _anim = GetComponent<Animator>();
        InitializedInteractionMap();
    }

    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// if the player walk into the trigger box in front of the drone the third person follow camera priority fall
    /// and the thirdperson follow camera following the drone take higher priority
    /// if player is controling a drone player can not move
    /// we will disable the "_playerMovement" input system action and "_input.PlayerInteractions" the enable "_input.Drone"
    /// </summary>

    public void CanBreakCrate()
    {

    }

    public void CanNotBreakCrate()
    {
        _punchCollider.enabled = false;
        _kickCollider.enabled = false;
    }

    public void InitializedInteractionMap()
    {

        _input = new PlayerInteractionInput();
        _input.PlayerInteractions.Enable();

        _input.PlayerInteractions.Interact.performed +=
            context =>
            {
                if (context.interaction is HoldInteraction)
                {
                    _anim.SetTrigger("Elbow");
                    _kickCollider.enabled = true;

                    Debug.Log("Tap");

                }
                else
                {

                    _anim.SetTrigger("Punch");
                    _punchCollider.enabled = true;

                    Debug.Log("Trigger");
                    //  Fire();
                }

            };

        _input.PlayerInteractions.Interact.canceled +=
            _ =>
            {
                _punchCollider.enabled = false;
                _kickCollider.enabled = false;

            };


    }

    public void ForkLiftUpdate()
    {
        transform.position = _forkliftPlayerposition.transform.position;

    }


    public void DoorKey()
    {
        _exit.PlayerHasKey();
        print("Key01");


    }

    public void ForkliftKey()
    {
        _trigger.PlayerCanEnter();
    }
    public void FlyDrone()
    {
        _drone.DroneCanFly();
        _input.PlayerInteractions.Disable();

    }

    public void PlayerBackOnline()
    {
        _input.PlayerInteractions.Enable();
    }

    public void Forklift()
    {
        _forkLift.ForkLiftActive();
    }

    public void CamSwith0()
    {
        _camManager.PlayerCamera();
    }
    public void CamSwitch1()
    {
        _camManager.DroneCamera();
    }

    public void CamSwitch2()
    {
        _camManager.ForkliftCamera();
    }


}
