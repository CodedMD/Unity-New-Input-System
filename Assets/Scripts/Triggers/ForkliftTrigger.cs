using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class ForkliftTrigger : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private InputActionAsset _playerMovement;
    [SerializeField] private Forklift_Movement _forkLift;
    [SerializeField] private int _triggerID;
    [SerializeField] private Camera_manager _camManager;
    [SerializeField] private bool _canEnter = false;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _camManager = GetComponent<Camera_manager>();
        if (_player == null)
        {
            Debug.LogError("Drone Is Null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void PlayerCanEnter()
    {
        _canEnter = true;
    }


    public void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player" || other.tag == "Drone")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null && _canEnter == true )
            {

                switch (_triggerID)
                {
                    case 0:

                        player.CamSwith0();
                        _playerMovement.Enable();

                        break;
                    case 1:
                        player.FlyDrone();
                        player.CamSwitch1();
                        _playerMovement.Disable();

                        //_playerMovement.Enable();
                        //player.StopFlyingDrone();
                        break;
                    case 2:
                        _forkLift.enabled = true;
                        player.Forklift();
                        player.CamSwitch2();
                        _playerMovement.Disable();
                        break;

                    default:
                        Debug.Log("Default Value");
                        break;
                }

            }
        }
    }


}
