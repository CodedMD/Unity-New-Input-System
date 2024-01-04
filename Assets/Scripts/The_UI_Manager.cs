using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class The_UI_Manager : MonoBehaviour
{
    [SerializeField] private Image[] _images = new Image[3];
    [SerializeField] private Sprite[] _keySprites = new Sprite[3];
    [SerializeField] private Sprite[] _objectiveSprites = new Sprite[4];
    [SerializeField] private Sprite[] _controlSprites = new Sprite[2];
    private int _spriteIndex = 0;
    private int _keyIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        _images[0].enabled = true;
        _images[0].sprite = _keySprites[0];
        _images[1].enabled = true; 
        _images[1].sprite = _objectiveSprites[0];
        _images[2].enabled = false;
        _images[3].enabled = false;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        CurrentObjective();
        KeyUpdate();
    }
    /// <summary>
    /// Key's
    /// </summary>

    public void KeyUpdate()
    {
        _images[0].sprite = _keySprites[_keyIndex];
    }
  
    public void FindTheKey()
    {
        StartCoroutine(FindKeyRoutine());
    }

    IEnumerator FindKeyRoutine() 
    {
        yield return null;
        _images[3].enabled = true;
        yield return new WaitForSeconds(5);
        _images[3].enabled = false;
    }

    
    public void CurrentObjective()
    {
        _images[1].sprite = _objectiveSprites[_spriteIndex];
    }

    public void NextObjective()
    {

        _spriteIndex++;
        _keyIndex++;
            
    }

   /* public void ForkKeyObjective()
    {
        _images[1].sprite = _objectiveSprites[1];
    }

    public void ForkLiftObjective() 
    {
        _images[1].sprite = _objectiveSprites[2];
    }

    public void MovethePallet()
    {
        _images[1].sprite = _objectiveSprites[3];

    }
    public void CrateBreak()
    {
        _images[1].sprite = _objectiveSprites[4];

    }*/

}
