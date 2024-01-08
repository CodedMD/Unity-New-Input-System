using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableKeys : MonoBehaviour
{
    [SerializeField] private int _keyID;
    [SerializeField] private The_UI_Manager _theUIManager;
    [SerializeField] private GameObject _hBox;
    [SerializeField] private ParticleSystem _hPrefab;
    private bool _psEnabled;

    // Start is called before the first frame update
    void Start()
    {
        var emission = _hPrefab.emission;
        emission.enabled = false;
        
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
            if(_keyID == 2)
            {
                _theUIManager.NextObjective();

            }

            if (_keyID == 1)
            {
                var emission = _hPrefab.emission;
                emission.enabled = true;
                player.DoorKey();
                _theUIManager.NextObjective();
                _hBox.SetActive(false);
               
                _theUIManager.ObjectiveVisable();
                _theUIManager.ControlsNotVisable();
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
