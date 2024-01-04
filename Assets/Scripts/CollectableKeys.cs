using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableKeys : MonoBehaviour
{
    [SerializeField] private int _keyID;
    [SerializeField] private The_UI_Manager _theUIManager;

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
        Drone_Movement drone = other.GetComponent<Drone_Movement>();
        if (player != null)
        {
            if(_keyID == 1)
            {
                player.DoorKey();
                _theUIManager.NextObjective();
                Destroy(gameObject);
            }
           
        }

        if(drone != null)
        {
            if (_keyID == 0)
            {
                drone.ForkliftKey();
                _theUIManager.NextObjective();

                Destroy(gameObject);
            }

        }
    }
}
