using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    private string playerName = "";
    private string roomCode = "";
    public ButtonBehaviour joinButton, createButton;
    public RoomCell CurrentRoom;

    // Start is called before the first frame update
    void Start()
    {
        ValidadeInputs();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void getPlayerName(string playerName) {
        this.playerName = playerName;
        ValidadeInputs();
    }
    public void getRoomCode(string roomCode) {
        this.roomCode = roomCode;
        ValidadeInputs();
    }
    public void ValidadeInputs() {
        if(playerName.Length < 3) {
            createButton.Disable();
            joinButton.Disable();
        } else {
            createButton.Enable();
            if(roomCode.Length != 6 && CurrentRoom == null) {
                joinButton.Disable();
            } else {
                joinButton.Enable();

            }
        }
    }
    public void CreateRoom() {
        if(createButton.isEnabled) {
            //code
        }
    }
    public void JoinRoom() {
        if(joinButton.isEnabled) {
            //code
        }
    }
}
