using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CharacterButton : MonoBehaviour
{
    private Character character;
    private bool selected;
    private bool disabled;
    [SerializeField]
    private Image fill, border;
    private CharacterSelection selectionManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetCharacter(Character character, CharacterSelection selectionManager) {
        this.character = character;
        this.selectionManager = selectionManager;
        fill.color = character.GetColor();
    }
    public void Select() {
        if(!disabled) {
            if(selectionManager.currentCharacter != this) {
                if(selectionManager.currentCharacter != null) {
                    selectionManager.currentCharacter.Deselect();
                }
                GetComponent<Animator>().Play("Base Layer.select");
                selectionManager.currentCharacter = this;
                selected = true;
            }
        }
    }
    public void Deselect() {
        if(!disabled) {
            selected = false;
            GetComponent<Animator>().Play("Base Layer.default");
        }
    }
    public void PointEnter() {
        if(!disabled) {
            if(!selected) {
                GetComponent<Animator>().Play("Base Layer.hover");
            }
        }
    }
    public void PointExit() {
        if(!disabled) {
            if(!selected) {
                GetComponent<Animator>().Play("Base Layer.default");
            }
        }
    }
    public void Disable() {
        GetComponent<Animator>().Play("Base Layer.disable");
        disabled = false;
    }
    public void Enable() {
        GetComponent<Animator>().Play("Base Layer.default");
        disabled = true;
    }
    public Character GetCharacter() {
        return character;
    }
}
