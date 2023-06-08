using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{
    public bool isEnabled;
    [SerializeField]
    private bool isSelectable = false;
    private bool selected = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Hover() {
        if(!selected)
            GetComponent<Animator>().SetBool("Hover", true);
    }
    public void Release() {
        if(!selected)
            GetComponent<Animator>().SetBool("Hover", false);
    }
    public void Press() {
        if(!isSelectable) {
            GetComponent<Animator>().SetBool("Press", true);
        } else {
            GetComponent<Animator>().SetBool("Selected", !selected);
            selected = !selected;
        }


    }
    public void CancelPress() {
        if(!selected)
            GetComponent<Animator>().SetBool("Press", false);
    }
    public void Disable() {
        GetComponent<Animator>().SetBool("Disabled", true);
        isEnabled = false;

    }
    public void Enable() {
        GetComponent<Animator>().SetBool("Disabled", false);
        isEnabled = true;
    }
}
