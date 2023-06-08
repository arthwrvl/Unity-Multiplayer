using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSelection : MonoBehaviour
{
    [SerializeField]
    private List<Color> colors = new List<Color>();
    [SerializeField]
    private List<GameObject> colorInstances = new List<GameObject>();
    [SerializeField]
    private GameObject colorPrefab;
    [SerializeField]
    private Transform colorArea;
    public ColorButton currentColor;
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
        foreach(Color color in colors) {
           GameObject colorObj =  Instantiate(colorPrefab, transform);
            colorObj.GetComponent<ColorButton>().SetColor(color, this);
            colorInstances.Add(colorObj);
        }
    }
}
