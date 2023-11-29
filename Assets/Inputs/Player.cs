using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerInputActions _input;
    private MeshRenderer _render;
    // Start is called before the first frame update
    void Start()
    {
        _input = new PlayerInputActions();
        _input.Player.Enable();

        _input.Player.Color.performed += Color_performed;
        _input.Player.DrivingMap.performed += DrivingMap_performed;

        _render = GetComponent<MeshRenderer>();
    }

    
    // Update is called once per frame
    void Update()
    {
        Rotating();
        Driving();
    }

    private void DrivingMap_performed(InputAction.CallbackContext context)
    {
        _input.Player.Disable();
        _input.Driving.Enable();
    }

    private void Driving()
    {
        var move = _input.Driving.Drive.ReadValue<Vector2>();
        transform.Translate(new Vector3(move.x, 0, move.y) * Time.deltaTime * 3f);

    }


    private void Rotating()
    {
        Debug.Log("Acis Value:" + _input.Player.Rotation.ReadValue<float>());
        var rotationDirection = _input.Player.Rotation.ReadValue<float>();
        transform.Rotate(Vector3.up * Time.deltaTime * 30f * rotationDirection);
    }
    private void Color_performed(InputAction.CallbackContext context)
    {
        _render.material.color = Random.ColorHSV();

    }


}
