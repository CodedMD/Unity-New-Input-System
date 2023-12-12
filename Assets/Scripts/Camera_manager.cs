using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class Camera_manager : MonoBehaviour
{
    [SerializeField] private PlayableDirector _director;
    [SerializeField] private GameObject _uiDisplay;
    [SerializeField] private InputActionAsset _playerMovemeent;
    [SerializeField] private Drone_Movement _drone;
    [SerializeField] private GameObject[] _vCams;

    private int _currentCam;
    private float _playTime;
    private bool _droneCam = false;
    private bool _playerCam = false;
    // calculation variables



    // Start is called before the first frame update
    void Start()
    {
        _playerMovemeent = GameObject.Find("Player").GetComponent<InputActionAsset>();
        _drone = GameObject.Find("Drone").GetComponent<Drone_Movement>();
       // _uiDisplay.SetActive(true);
        _vCams[0].GetComponent<CinemachineVirtualCamera>().Priority = 15;
        

    }


    // Update is called once per frame
    void Update()
    {

        if (_droneCam == true)
        {
            DroneCamera();


        }
        if (_playerCam == true)
        {
            PlayerCamera();
        }
        //DirectorControls();


    }


    public void PlayerCamera()
    {
        _playerCam = true;
        print("Player Camera");
        _vCams[0].GetComponent<CinemachineVirtualCamera>().Priority = 15;
        _vCams[1].GetComponent<CinemachineVirtualCamera>().Priority = 10;
        _playerCam = false;
    }


    public void DroneCamera()
    {
        _droneCam = true;
        print("Drone Camera");
        _vCams[1].GetComponent<CinemachineVirtualCamera>().Priority = 15;
        _vCams[0].GetComponent<CinemachineVirtualCamera>().Priority = 10;
        _droneCam = false;

    }


    /* public void DirectorControls()
     {

         _playTime += Time.deltaTime;

         if (Input.GetAxis("Mouse X") == 0f && Input.anyKey == false)
         {
             _canPlay = true;
         }
         else
         {
             _canPlay = false;
         }


         if ( _canPlay && _playTime >= 5.0f)
         {
             _uiDisplay.SetActive(false);
             _director.Play();
         }
         else if( !_canPlay)
         {
             _uiDisplay.SetActive(true);
             _director.Stop();
             _playTime = 0;  
         }

     }*/




    public void SetLowCamPriorities()
    {
        foreach (var c in _vCams)
        {
            if (c.GetComponent<CinemachineVirtualCamera>())
            {
                c.GetComponent<CinemachineVirtualCamera>().Priority = 10;

            }

            if (c.GetComponent<CinemachineBlendListCamera>())
            {
                c.GetComponent<CinemachineBlendListCamera>().Priority = 10;

            }


        }
    }

    public void SetCurrentCamera()
    {

        if (_vCams[_currentCam].GetComponent<CinemachineBlendListCamera>())
        {
            _vCams[_currentCam].GetComponent<CinemachineBlendListCamera>().Priority = 15;

        }

        if (_vCams[_currentCam].GetComponent<CinemachineVirtualCamera>())
        {
            _vCams[_currentCam].GetComponent<CinemachineVirtualCamera>().Priority = 15;
        }


    }
}
