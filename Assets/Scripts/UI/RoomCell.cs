using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Lobbies.Models;
using TMPro;
public class RoomCell : MonoBehaviour
{
    public Menu menuManager;
    public TextMeshProUGUI lobbyName, playerCount;
    private Lobby lobby;
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
    public void SetData(Lobby lobby, Menu menuManager) {
        this.menuManager = menuManager;
        this.lobby = lobby;
        UpdateValues();
    }
    private void UpdateValues() {
        if(lobby != null) {
            lobbyName.text = lobby.Name;
            playerCount.text = lobby.Players.Count + "/" + lobby.MaxPlayers;
        }
    }
    public string GetLobbyId() {
        return lobby.Id;
    }
}
