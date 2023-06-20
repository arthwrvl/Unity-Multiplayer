using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Characters/Create Character")]
public class Character : ScriptableObject
{
   [SerializeField] private int id = -1;
   [SerializeField] private string characterName = "new name";
   [SerializeField] private Color characterColor;


    public int GetId() {
        return id;
    }
    public string GetName() {
        return characterName;
    }
    public Color GetColor() {
        return characterColor;
    }
}
