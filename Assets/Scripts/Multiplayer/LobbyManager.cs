using System.Collections;
using System.Collections.Generic;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;

public class LobbyManager : MonoBehaviour {
    public static LobbyManager manager;
    [SerializeField]
    private Transform roomArea;
    [SerializeField]
    private Menu menuManager;
    [SerializeField]
    private GameObject roomInfo;
    private List<GameObject> currentRoomList = new List<GameObject>();
    [SerializeField]
    private GameObject noRoom;
    private Lobby hostRoom, joinedRoom;
    private void OnEnable() {
        if(LobbyManager.manager == null) {
            LobbyManager.manager = this;
        } else {
            Destroy(this);
        }
    }
    
    async void Start() {
        await UnityServices.InitializeAsync();
        StartCoroutine(SearchRooms());
    }
    IEnumerator SearchRooms() {
        //Search for Avaliable Rooms every 10 sec
        while(true) {
            ListRooms();
            yield return new WaitForSecondsRealtime(10);
        }
    }
    private async void ListRooms() {
        //Authenticate user
        await AuthenticationManager.manager.Authenticate();

        try {
            QueryLobbiesOptions queryLobbiesOptions = new QueryLobbiesOptions {
                Order = new List<QueryOrder> {
                    new QueryOrder(false, QueryOrder.FieldOptions.AvailableSlots)
                }
            };
            //query for the rooms
            QueryResponse queryResponse = await Lobbies.Instance.QueryLobbiesAsync(queryLobbiesOptions);

            //destroy previous rooms
            foreach(GameObject room in currentRoomList) {
                Destroy(room);
            }
            //instantiate current
            foreach(Lobby lobby in queryResponse.Results) {
                GameObject room = Instantiate(roomInfo, roomArea);
                room.GetComponent<RoomCell>().SetData(lobby, menuManager);
                currentRoomList.Add(room);
            }
            //if it has no rooms, warn user
            noRoom.SetActive(queryResponse.Results.Count == 0);

        } catch(LobbyServiceException e) {
            Debug.Log(e);
        }
    }
    public async void CreateRoom(string playerName, string roomName, int maxPlayers, bool isPrivate = false) {
        //Authenticate user
        await AuthenticationManager.manager.Authenticate();
        try {
            CreateLobbyOptions options = new CreateLobbyOptions {
                Player = GetPlayer(playerName),
                IsPrivate = isPrivate
            };

            //create room
            hostRoom = await Lobbies.Instance.CreateLobbyAsync(roomName, maxPlayers, options);
            joinedRoom = hostRoom;
            StartCoroutine(SendHeartbeat());
        }catch (LobbyServiceException e) {
            Debug.Log(e);
        }

    }
    public async void JoinRoom(string playerName, string roomCode = "", string roomId = "") {
        //Authenticate User
        await AuthenticationManager.manager.Authenticate();
        try {
            if(roomCode.Length == 6) {
                //join by code
                JoinLobbyByCodeOptions options = new JoinLobbyByCodeOptions {
                    Player = GetPlayer(playerName)
                };
                joinedRoom = await Lobbies.Instance.JoinLobbyByCodeAsync(roomCode);

            } else {
                //join by id
                JoinLobbyByIdOptions options = new JoinLobbyByIdOptions {
                    Player = GetPlayer(playerName)
                };
                joinedRoom = await Lobbies.Instance.JoinLobbyByIdAsync(roomId);
            }
        }catch (LobbyServiceException e) {
            Debug.Log(e);
        }



    }
    //assign a name to player
    private Player GetPlayer(string playerName) {
        Player player = new Player {
            Data = new Dictionary<string, PlayerDataObject> {
                {"playerName", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Public, playerName) }
            }
        };
        return player;
    }
    IEnumerator SendHeartbeat() {
        //send heartbeat every 15sec to keep room avaliable
        while(true) {
            LobbyService.Instance.SendHeartbeatPingAsync(hostRoom.Id);
            yield return new WaitForSecondsRealtime(15);
        }
    }

}
