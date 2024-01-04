using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CrateTrigger : MonoBehaviour
{

    [SerializeField] private CrateBreak _crate;
   // [SerializeField] private GameObject _wholeCrate, _brokenCrate;
    [SerializeField]private Player _player;

    // Start is called before the first frame update
    void Start()
    {
       


    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnTriggerEnter(Collider other)
    {
       
        Player player = other.GetComponent<Player>();   
        if ( player != null)
        {

            _crate.BreakableCrate();
            //_crate.BreakPart();
            _crate.BreakableCrate();   

        }
    }
    public void OnTriggerExit(Collider other) 
    {
        if (other.tag == "Player")
        {
           
            _player.CanNotBreakCrate();
  


        }
    }
}
