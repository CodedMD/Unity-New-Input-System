using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CratePart : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    private float _thrust = 5f;
    private bool _canHit = false;
    [SerializeField] private int _partID;
    [SerializeField]private CrateBreak _crate;
    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; 

    }

    // Update is called once per frame
    void Update()
    {
        if (_canHit == true)
        {
            BreakOff();

        }
        // rb.AddForce(transform.up, ForceMode.Force);
        //transform.Translate(Vector3.up * Time.deltaTime * 3.0f);
    }

    public void BreakOff()
    {

        rb.AddForce(0, -_thrust, 0, ForceMode.Force);
        StartCoroutine(WaitToDestroyRoutine());
        //_crate.BreakPart();
    }

    public void HasGravity()
    {
        rb.isKinematic = false;
    }

    IEnumerator WaitToDestroyRoutine()
    {
        yield return null;
       yield return new WaitForSeconds(4f);
        Destroy(gameObject);
        _crate._pieces[_partID] = null;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PunchBox")
        {
            _canHit = true;
            HasGravity();
        }

        if (other.tag == "KickBox")
        {
            _canHit = true;
            HasGravity();
            _thrust = _thrust * 3;
        }
    }

}
