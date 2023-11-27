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
        _input.Dog.Bark.canceled += Bark_canceled => Debug.Log("Done Barking" + Bark_canceled);
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
