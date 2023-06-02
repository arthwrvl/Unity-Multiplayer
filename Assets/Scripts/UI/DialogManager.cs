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
    private void OnEnable() {
        if(DialogManager.manager == null) {
            DialogManager.manager = this;
        } else {
            Destroy(gameObject);
        }
    }
    public void SetInitialValues(string RoomName, int maxPlayers = 10) {
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
        //Implement Logic
    }

}
