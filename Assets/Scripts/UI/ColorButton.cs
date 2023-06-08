using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ColorButton : MonoBehaviour
{
    private Color color;
    private bool selected;
    [SerializeField]
    private Image fill, border;
    private ColorSelection selectionManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetColor(Color color, ColorSelection selectionManager) {
        this.color = color;
        this.selectionManager = selectionManager;
        fill.color = color;
    }
    public void Select() {
        if(selectionManager.currentColor != this) {
            if(selectionManager.currentColor != null) {
                selectionManager.currentColor.Deselect();
            }
            GetComponent<Animator>().Play("Base Layer.select");
            selectionManager.currentColor = this;
            selected = true;
        }

    }
    public void Deselect() {
        selected = false;
        GetComponent<Animator>().Play("Base Layer.default");
    }
    public void PointEnter() {
        if(!selected) {
            GetComponent<Animator>().Play("Base Layer.hover");
        }
    }
    public void PointExit() {
        if(!selected) {
            GetComponent<Animator>().Play("Base Layer.default");
        }
    }
}
