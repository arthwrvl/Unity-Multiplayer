using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class CharacterSelection : NetworkBehaviour
{
    [SerializeField]
    private List<Character> characters = new List<Character>();
    [SerializeField]
    private GameObject characterButtonPrefab;
    [SerializeField]
    public CharacterButton currentCharacter;
    private List<CharacterButton> characterInstances = new List<CharacterButton>();

    private NetworkList<CharacterState> players;


    private void Awake() {
        players = new NetworkList<CharacterState>();
    }
    public override void OnNetworkSpawn() {
        if(IsClient) {
            foreach(Character character in characters) {
                CharacterButton charObj = Instantiate(characterButtonPrefab, transform).GetComponent<CharacterButton>();
                charObj.SetCharacter(character, this);
                characterInstances.Add(charObj);
            }

            players.OnListChanged += PlayerListChanged;
        }
        if(IsServer) {
            NetworkManager.Singleton.OnClientConnectedCallback += ClientConnection;
            NetworkManager.Singleton.OnClientConnectedCallback += ClientDisconnection;
        }
        foreach(NetworkClient client in NetworkManager.Singleton.ConnectedClientsList) {
            ClientConnection(client.ClientId);
        }
    }
    public override void OnNetworkDespawn() {
        if(IsServer) {
            NetworkManager.Singleton.OnClientConnectedCallback -= ClientConnection;
            NetworkManager.Singleton.OnClientConnectedCallback -= ClientDisconnection;
        }
        players.OnListChanged -= PlayerListChanged;
    }
    private void ClientConnection(ulong clientID) {
        players.Add(new CharacterState(clientID));
    }
    private void ClientDisconnection(ulong clientID) {
        for(int i = 0; i < players.Count; i++) {
            if(players[i].clientID == clientID) {
                players.RemoveAt(i);
                break;
            }
        }
    }
    public void Select(Character character) {
        for(int i = 0;i < players.Count; i++) {

            //if its not this client
            if(players[i].clientID != NetworkManager.Singleton.LocalClientId) {
                continue;
            }
            //not picking the character you've already picked
            if(players[i].characterID == character.GetId()) {
                return;
            }
            //not picking a character that has already been taken
            if(isCharacterPicked(character.GetId(), false)) {
                return;
            }
        }
        SelectServerRpc(character.GetId());
    }

    [ServerRpc(RequireOwnership = false)]
    private void SelectServerRpc(int characterID, ServerRpcParams serverRpcParams = default) {
        for(int i = 0; i < players.Count; i++) {
            if(players[i].clientID != serverRpcParams.Receive.SenderClientId) {
                continue;
            }
            if(!IsCharacterValid(characterID)) {
                return;
            }
            if(isCharacterPicked(characterID, true)) {
                return;
            }

            players[i] = new CharacterState(players[i].clientID, characterID, players[i].isPicked);

        }
    }
    public void PickCharacter() {
        PickCharacterServerRpc();
    }
    [ServerRpc(RequireOwnership = false)]
    private void PickCharacterServerRpc(ServerRpcParams serverRpcParams = default) {
        for(int i = 0; i < players.Count; i++) {
            if(players[i].clientID == serverRpcParams.Receive.SenderClientId) {
                continue;
            }
            if(!IsCharacterValid(players[i].characterID)) {
                return;
            }
            if(isCharacterPicked(players[i].characterID, true)) {
                return;
            }
            players[i] = new CharacterState(players[i].clientID, players[i].characterID, true);

        }
    }
    private void PlayerListChanged(NetworkListEvent<CharacterState> networkListEvent) {

        //for (int i = 0; i < pla)
        foreach(var button in characterInstances) {
            if(isCharacterPicked(button.GetCharacter().GetId(), false)) {
                button.Disable();
            }
        }
    }
    private bool IsCharacterValid(int ID) {
        bool valid = false;
        for(int i = 0; i < characters.Count; i++) {
            if(characters[i].GetId() == ID) {
                valid = true;
                break;
            }
        }
        return valid;
    }
    private bool isCharacterPicked(int characterID, bool checkAll) {
        for(int i = 0; i < players.Count; i++) {
            //avoid check the local player
            if(!checkAll) {
                //if local player is the client, skip
                if(players[i].clientID == NetworkManager.Singleton.LocalClientId) {
                    continue;
                }
            }

            if(players[i].isPicked && players[i].characterID == characterID) {
                return true;
            }
        }
        return false;
    }

}
