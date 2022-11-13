using UnityEngine;
using UnityEngine.UI;

public class Quest : MonoBehaviour {
    Text text;
    public int count;
    Color clearColor;
    void Start() {
        clearColor = Color.gray;
        clearColor.a = 0.5f;
        text = GetComponent<Text>();
    }

    void Update() {
        if (count <= 0) {
            text.color = clearColor;
        }
    }
}
