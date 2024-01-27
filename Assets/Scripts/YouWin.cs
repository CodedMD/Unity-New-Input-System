using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YouWin : MonoBehaviour
{

    [SerializeField]private bool _playerCanExit = false;
    [SerializeField]private GameObject _player;
    [SerializeField] private GameObject _button;
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
    {PlayerMovement player = other.GetComponent<PlayerMovement>();
        if (player != null && _playerCanExit == true)
        {
            _player.SetActive(false);
            _button.SetActive(true);

        }
    }
}
