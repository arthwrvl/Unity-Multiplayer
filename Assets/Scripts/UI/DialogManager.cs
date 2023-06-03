using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogManager : MonoBehaviour
{
    public static DialogManager manager;
    [SerializeField]
    private string nameValue;
    [SerializeField]
    private string playersValue;
    [SerializeField]
    private TMP_InputField nameField, playersField;
    private string playerName;
    private void OnEnable() {
        if(DialogManager.manager == null) {
            DialogManager.manager = this;
        } else {
            Destroy(this);
        }
    }
    public void SetInitialValues(string playerName, string RoomName, int maxPlayers = 10) {
        this.playerName = playerName;
        nameValue = RoomName;
        playersValue = maxPlayers.ToString();
        nameField.text = nameValue;
        playersField.text = playersValue;
    }
    public void GetRoomName(string value) {
        nameValue = value;
    }
    public void GetMaxPlayers(string value) {
        playersValue = value;
    }
    public void CreateRoom() {
        if(nameValue != "" && playersValue != "") {
            LobbyManager.manager.CreateRoom(playerName, nameValue, int.Parse(playersValue));
            GetComponent<Animator>().SetBool("Open", false);
        }
    }

}
