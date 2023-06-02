using System.Collections;
using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;

public class TestLobby : MonoBehaviour
{
    private Lobby hostLobby;
    // Start is called before the first frame update
    async void Start()
    {
        await UnityServices.InitializeAsync();
        AuthenticationService.Instance.SignedIn += () => {
            Debug.Log("Signed in " + AuthenticationService.Instance.PlayerId);
        };
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.C)) {
            CreateLobby();
        }
        if(Input.GetKeyDown(KeyCode.S)) {
            ListLobbies();
        }
    }
    private async void CreateLobby() {
        try {
            string lobbyName = "Lobby";
            int maxPlayers = 10;
            Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, maxPlayers);
            hostLobby = lobby;
            StartCoroutine(SendHeartbeat());
            Debug.Log("Created Lobby! " + lobby.Name + " " + lobby.Players.Count + "/" + lobby.MaxPlayers);
        } catch (LobbyServiceException e){
            Debug.Log(e);
        }
    }
    IEnumerator SendHeartbeat() {
        while(true) {
            LobbyService.Instance.SendHeartbeatPingAsync(hostLobby.Id);
            yield return new WaitForSecondsRealtime(15);
        }
    }
    private async void ListLobbies() {
        try {
            QueryLobbiesOptions queryLobbiesOptions = new QueryLobbiesOptions {
                Order = new List<QueryOrder> {
                    new QueryOrder(false, QueryOrder.FieldOptions.AvailableSlots)
                }
            };
            QueryResponse queryResponse = await Lobbies.Instance.QueryLobbiesAsync(queryLobbiesOptions);

            Debug.Log("Lobbies found: " + queryResponse.Results.Count);
            foreach(Lobby lobby in queryResponse.Results) {
                Debug.Log(lobby.Name + " - " + lobby.Players.Count + "/" + lobby.MaxPlayers);
            }
        } catch (LobbyServiceException e) {
            Debug.Log(e);
        }

    }
}
