using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Lift_Forks : MonoBehaviour
{
    private PlayerInteractionInput _input;
   // [SerializeField] private InputActionAsset _playerMovement;
   // [SerializeField] private Player _player;
    // Start is called before the first frame update
    void Start()
    {
        _input.Forklift.LiftLow.started += LiftLow_started;
    }

    private void LiftLow_started(InputAction.CallbackContext obj)
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ForksUp()
    {
        var liftForks = _input.Forklift.LiftLow.ReadValue<float>();

        transform.Translate(new Vector3(0, liftForks, 0) * Time.deltaTime);
        /*_raiseLiftLimit = 4f;
       if (_forks.transform.localPosition.y < _raiseLiftLimit)
        {
            Vector3 tempPos = _forks.transform.localPosition;
            tempPos.y += Time.deltaTime * 3.0f;
            _forks.transform.localPosition = new Vector3(tempPos.x, tempPos.y, tempPos.z);

        }*/
    }

    private void ForksDown()
    {
        /*Vector2 _raiseLiftLimit;
         if (_forks.transform.localPosition.y < _raiseLiftLimit.y)
         {
             Vector3 tempPos = _forks.transform.localPosition;
             tempPos.y += Time.deltaTime * 3.0f;
             _forks.transform.localPosition = new Vector3(tempPos.x, tempPos.y, tempPos.z);

         }*/

    }
}
