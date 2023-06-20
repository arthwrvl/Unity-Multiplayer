using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectionScreen : MonoBehaviour
{
    [SerializeField]
    private List<Character> characters = new List<Character>();
    [SerializeField]
    private List<GameObject> characterInstances = new List<GameObject>();

    [SerializeField]
    public CharacterButton currentCharacter;
    // Start is called before the first frame update
    void Start()
    {
        DrawColors();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void DrawColors() {

    }
}
