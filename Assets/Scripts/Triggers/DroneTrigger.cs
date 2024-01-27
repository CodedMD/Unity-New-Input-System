using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;



public class DroneTrigger : MonoBehaviour
{
    [SerializeField] private PlayerMovement _player;
    [SerializeField] private PlayerControls  _input;
    [SerializeField] private int _triggerID;
    [SerializeField] private Camera_manager _camManager;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        _camManager = GetComponent<Camera_manager>();
        if(_player == null)
        {
            Debug.LogError("Drone Is Null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player"||other.tag == "Drone" )
        {
            PlayerMovement player = other.transform.GetComponent<PlayerMovement>();

            if (player != null )
            {

                switch (_triggerID)
                {
                    case 0:
                     
                        player.CamSwith0();
                        _input.Enable();
                     
                        break;
                    case 1:
                        player.FlyDrone();
                        player.CamSwitch1();
                        _input.Disable();
                       
                        //_playerMovement.Enable();
                        //player.StopFlyingDrone();
                        break;
                         default:
                        Debug.Log("Default Value");
                        break;
                }
               
            }
        }
    }

   
}
