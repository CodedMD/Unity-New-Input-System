using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MakeChild : MonoBehaviour
{
    [SerializeField] private Transform _parentTransform;
    [SerializeField] private GameObject _parent;
    private bool _isChild = false;

    private void Start()
    {
    }
    private void Update()
    {
       
        if (_parentTransform.transform.position.y <= .605)
        { _isChild=false;
            ReleaseParent();
        }
    }

    public void SetParent()
    {
        transform.SetParent(_parentTransform,true);
    }

    public void ReleaseParent()
    {
        
        transform.SetParent(null);
        
    }
  
    public void IsChildNo()
    {
        _isChild = false;
    }


    public void OnTriggerEnter(Collider other)
    {
        _isChild=true;
        if (_isChild)
        {
            if (other.tag == "Forks")
            {
                SetParent();
            }
        }
       
    }
}
