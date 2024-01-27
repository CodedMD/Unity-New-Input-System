using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class TriggerPoints : MonoBehaviour
{
    [SerializeField] private PlayerMovement _player;
    [SerializeField] private InputActionAsset _playerMovement;
    [SerializeField] private Forklift_Movement _forkLift;
    [SerializeField] private int _triggerID;
    [SerializeField] private Camera_manager _camManager;
    [SerializeField] private bool _canEnter = false;
    [SerializeField] private The_UI_Manager _theUIManager;
    // Start is called before the first frame update
    void Start()
    {
       // _player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        _camManager = GetComponent<Camera_manager>();
       
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
            PlayerMovement player = other.transform.GetComponent<PlayerMovement>();

            if (player != null)
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


                        break;
                    case 2:
                        if (_canEnter == true)
                        {
                            _forkLift.enabled = true;
                            player.Forklift();
                            player.CamSwitch2();
                            _playerMovement.Disable();
                        }
                        if (_canEnter == false)
                            _theUIManager.FindTheKey();
                        break;

                    default:
                        Debug.Log("Default Value");
                        break;
                }

            }
        }
    }
}
