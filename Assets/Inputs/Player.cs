using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerInputActions _input;
    // Start is called before the first frame update
    void Start()
    {
        _input = new PlayerInputActions();
        _input.Player.Enable();

       
        
    }

    

    // Update is called once per frame
    void Update()
    {
        Movement();
    }


    private void Movement()
    {
        var move = _input.Player.Movement.ReadValue<Vector2>();
        transform.Translate(move * Time.deltaTime * 3);
    }
}
