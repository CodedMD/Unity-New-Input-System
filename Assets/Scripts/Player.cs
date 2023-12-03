using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerInputActions _input;
    private MeshRenderer _render;
    private bool _jumped = false;
    // Start is called before the first frame update
    void Start()
    {
        _input = new PlayerInputActions();
        _input.Player.Enable();
        _input.Player.Jump.performed += Jump_performed;
        _input.Player.Jump.canceled += Jump_canceled;
        /*_input.Player.Color.performed += Color_performed;
        _input.Player.DrivingMap.performed += DrivingMap_performed;
        _render = GetComponent<MeshRenderer>();*/
    }

    
    // Update is called once per frame
    void Update()
    {
       
    }

    private void Jump_performed(InputAction.CallbackContext obj)
    {
        Debug.Log("full Jump");
        _jumped = true;
        GetComponent<Rigidbody>().AddForce(Vector3.up * 25, ForceMode.Impulse);
    }
    private void Jump_canceled(InputAction.CallbackContext context)
    {
        var forceEffect = context.duration;
        GetComponent<Rigidbody>().AddForce(Vector3.up * 5, ForceMode.Impulse);

    }
    private void DrivingMap_performed(InputAction.CallbackContext context)
    {
        _input.Player.Disable();
        //_input.Driving.Enable();
    }

    private void Driving()
    {
       // var move = _input.Driving.Drive.ReadValue<Vector2>();
       // transform.Translate(new Vector3(move.x, 0, move.y) * Time.deltaTime * 3f);

    }


   /* private void Rotating()
    {
        Debug.Log("Acis Value:" + _input.Player.Rotation.ReadValue<float>());
        var rotationDirection = _input.Player.Rotation.ReadValue<float>();
        transform.Rotate(Vector3.up * Time.deltaTime * 30f * rotationDirection);
    }
    private void Color_performed(InputAction.CallbackContext context)
    {
        _render.material.color = Random.ColorHSV();

    }*/


}
