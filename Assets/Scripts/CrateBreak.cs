using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class CrateBreak : MonoBehaviour
{
    [SerializeField]private GameObject _baseCrate, _breakCrate;
    [SerializeField] public Rigidbody[] _pieces;
    private bool _canBreak = false;
    [SerializeField] private PunchBox _punchBox;
    private List<Rigidbody> _breakOff = new List<Rigidbody>();
   // [SerializeField]private CratePart[] _part;    
    // Start is called before the first frame update
    void Start()
    {
        _baseCrate.SetActive(true);
        _breakCrate.SetActive(false);
        _breakOff.AddRange(_pieces);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BreakableCrate()
    {
        _baseCrate.SetActive(false);
        _breakCrate.SetActive(true);   
    }

    public void BreakPart()
    {
        //.AddForce(0, -_thrust, 0, ForceMode.Force);

        int rng = Random.Range(0, _breakOff.Count);
        _breakOff.Remove(_breakOff[rng]);
        Destroy(_pieces[rng]);  
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PunchBox")
        {
            
            print("Hit");
        }
        else
        {
            print("miss");
        }
        
    }


}
