using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RoomCell : MonoBehaviour
{
    public Menu menuManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Select() {
        if(menuManager.currentRoom != null)
            menuManager.currentRoom.Deselect();
        if(menuManager.currentRoom == this) {
            Deselect();
            menuManager.currentRoom = null;

        } else {
            GetComponent<Image>().color = new Color(0.04f, 0.52f, 0.89f);
            menuManager.currentRoom = this;
        }
        menuManager.ValidadeInputs();

    }
    public void Deselect() {
        GetComponent<Image>().color = new Color(0.39f, 0.43f, 0.45f);

    }
}
