using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    private string playerName = "";
    private string roomCode = "";
    public ButtonBehaviour joinButton, createButton;
    public RoomCell currentRoom;
    public Animator dialog;

    // Start is called before the first frame update
    void Start()
    {
        ValidadeInputs();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            CloseDialog();
        }
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
            if(roomCode.Length != 6 && currentRoom == null) {
                joinButton.Disable();
            } else {
                joinButton.Enable();

            }
        }
    }
    public void CreateRoom() {
        if(createButton.isEnabled) {
            dialog.SetBool("Open", true);
            DialogManager.manager.SetInitialValues(playerName, playerName + "'s Room");
        }
    }
    public void JoinRoom() {
        if(joinButton.isEnabled) {
            LobbyManager.manager.JoinRoom(playerName, roomCode, currentRoom ? currentRoom.GetLobbyId() : "");
        }
    }
    public void CloseDialog() {
        dialog.SetBool("Open", false);

    }
}
