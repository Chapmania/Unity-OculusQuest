using UnityEngine;
using UnityEngine.UI;

public class BoardController : MonoBehaviour {
    public Button button;

    void Start() {
        button.onClick.AddListener(() => { Debug.Log("Start!"); });
    }

    void Update() {
    }
}