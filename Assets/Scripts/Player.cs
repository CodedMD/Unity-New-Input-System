using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class Player : MonoBehaviour
{
    private PlayerInteractionInput _input;

    [SerializeField] private InputActionAsset _playerMovement;
    [SerializeField]private Drone_Movement _drone;
    [SerializeField] private Camera_manager _camManager;

    // private MeshRenderer _render;
    // private bool _walking = false;
    // private InputActionReference _inputActionReference;

    [SerializeField] private float _rotSpeed;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _currentSpeed = 5;
    private float _vertical;
    private float _horizontal;
    [SerializeField] private float _maxRotate;
    // Start is called before the first frame update
    void Start()
    {

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



    public void InitializedInteractionMap()
    {
        _input = new PlayerInteractionInput();
        _input.PlayerInteractions.Enable();

        _input.PlayerInteractions.Interact.started +=
    context =>
    {
        if (context.interaction is HoldInteraction)
        {
            //ShowChargingUI();
            Debug.Log("Hold");
        }

    };

        _input.PlayerInteractions.Interact.performed +=
            context =>
            {
                if (context.interaction is TapInteraction)
                {
                    Debug.Log("Tap");
                    // ChargedFire();
                }
                else
                {
                    Debug.Log("Trigger");
                    //  Fire();
                }

            };

        _input.PlayerInteractions.Interact.canceled +=
            _ =>
            {
                Debug.Log("Canceled");
                //HideChargingUI();
            };

    }

    

   public void FlyDrone()
    {
        _drone.DroneCanFly();
    }

    public void CamSwith0()
    {
        _camManager.PlayerCamera();
    }
    public void CamSwitch1()
    {
        _camManager.DroneCamera();
    }

    public void CamSwitch02()
    {

    }


}
