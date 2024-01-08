using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TriggerPoints : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private InputActionAsset _playerMovement;
    [SerializeField] private Forklift_Movement _forkLift;
    [SerializeField] private int _triggerID;
    [SerializeField] private Camera_manager _camManager;
    [SerializeField] private bool _canEnter = false;
    [SerializeField] private The_UI_Manager _theUIManager;
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

            if (player != null )
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
                        _theUIManager.SoloObjective();
                        _theUIManager.ControlsVisable();


                         break;
                    case 2:
                        if (_canEnter == true)
                        {
                            _forkLift.enabled = true;
                            _playerMovement.Disable();
                            player.Forklift();
                            player.CamSwitch2();
                            _theUIManager.ControlsVisable();
                            _theUIManager.NextControlUI();
                            _theUIManager.SoloObjective();
                        }
                        if (_canEnter == false)
                        _theUIManager.FindTheKey();
                        break;
                    case 3:
                        _theUIManager.ControlsVisable();
                        _theUIManager.ObjectiveNotVisable();
                        _theUIManager.NextControlUI();
                            break;
                    default:
                        Debug.Log("Default Value");
                        break;
                }

            }
        }
    }
}
