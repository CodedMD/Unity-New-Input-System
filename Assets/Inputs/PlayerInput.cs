using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private PlayerInputActions _input;

    private void Start()
    {
        _input = new PlayerInputActions();
        _input.Dog.Enable();
        _input.Dog.Bark.performed += Bark_performed;
        _input.Dog.Bark.canceled += Bark_canceled;
        //// _input.Dog.Bark.performed += Bark_performed => Debug.Log("Barking" + Bark_performed);
        //_input.Dog.Bark.canceled += Bark_canceled => Debug.Log("Done Barking" + Bark_canceled);
        _input.Dog.Walk.performed += Walk_performed => Debug.Log("Walking" + Walk_performed);
        _input.Dog.Walk.canceled += Walk_canceled => Debug.Log("Not Walking" + Walk_canceled);
        _input.Dog.Run.performed += Run_performed => Debug.Log("Running" + Run_performed);
        _input.Dog.Run.canceled += Run_canceled => Debug.Log("Done Running" + Run_canceled);
        _input.Dog.Die.performed += Die_performed => Debug.Log("Dying" + Die_performed);
        _input.Dog.Die.performed += MyCustomAction => Debug.Log("Not Dying" + MyCustomAction);
    }
    
    private void Bark_performed(InputAction.CallbackContext context)
    {
        Debug.Log("Barking" + context);
    }
    private void Bark_canceled(InputAction.CallbackContext context)
    {
        Debug.Log("Done Barking" + context);
    }


}
