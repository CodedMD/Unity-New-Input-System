using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
public class The_UI_Manager : MonoBehaviour
{
    [SerializeField] private Image[] _images = new Image[3];
    [SerializeField] private Sprite[] _keySprites = new Sprite[3];
    [SerializeField] private Sprite[] _objectiveSprites = new Sprite[4];
    [SerializeField] private Sprite[] _controlSprites = new Sprite[2];
    [SerializeField] private GameObject _dir;
    private int _objectiveIndex = 0;
    private int _keyIndex = 0;
    private int _controlIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        _images[0].enabled = true;
        _images[0].sprite = _keySprites[0];
        _images[1].enabled = true; 
        _images[1].sprite = _objectiveSprites[0];
        _images[2].sprite = _controlSprites[0];
        _images[2].enabled = false;
        _images[3].enabled = false;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        CurrentObjective();
        KeyUpdate();
        ControlUi();
    }
    /// <summary>
    /// Key's
    /// </summary>

    public void KeyUpdate()
    {
        _images[0].sprite = _keySprites[_keyIndex];
    }
    public void CurrentObjective()
    {
        _images[1].sprite = _objectiveSprites[_objectiveIndex];
    }
    public void ControlUi()
    {

        _images[2].sprite = _controlSprites[_controlIndex];
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

    public void NextObjective()
    {

        _objectiveIndex++;
        _keyIndex++;
            
    }

    public void SoloObjective()
    {
        _objectiveIndex++;
    }

  public void NextControlUI()
    {
        _controlIndex++;
    }
    public void ObjectiveVisable()
    {
        _images[1].enabled = true;
    }

    public void ObjectiveNotVisable()
    {
        _images[1].enabled = false;
    }
    public void ControlsVisable()
    {
        _images[2].enabled = true;
    }

   public void ControlsNotVisable()
    {
        _images[2].enabled = false;
    }

    public void UIActive()
    {
        _dir.SetActive(false);
        gameObject.SetActive(true);
    }
}
