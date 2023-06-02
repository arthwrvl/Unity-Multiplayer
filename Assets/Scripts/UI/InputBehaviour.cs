using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputBehaviour : MonoBehaviour
{
    [SerializeField]
    private Animator animatorController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Select() {
        animatorController.SetBool("Selected", true);
    }
    public void Deselect() {
        animatorController.SetBool("Selected", false);
    }
}
