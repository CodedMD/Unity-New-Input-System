using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YouWin : MonoBehaviour
{

    [SerializeField]private bool _playerCanExit = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayerHasKey()
    {
        _playerCanExit= true;
    }
    public void OnTriggerEnter(Collider other)
    {Player player = other.GetComponent<Player>();
        if (player != null && _playerCanExit == true)
        {
            print("YouWin");
        }
    }
}
