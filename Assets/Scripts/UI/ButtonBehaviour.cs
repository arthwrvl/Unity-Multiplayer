using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{
    public bool isEnabled;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Hover() {
        GetComponent<Animator>().SetBool("Hover", true);
    }
    public void Release() {
        GetComponent<Animator>().SetBool("Hover", false);
    }
    public void Press() {
        GetComponent<Animator>().SetBool("Press", true);
    }
    public void CancelPress() {
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
