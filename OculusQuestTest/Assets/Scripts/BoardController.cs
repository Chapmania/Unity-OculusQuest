using UnityEngine;
using UnityEngine.UI;

public class BoardController : MonoBehaviour {
    public Button button;

    void Start() {
        button.onClick.AddListener(() => { Debug.Log("Start!"); });
    }

    // Update is called once per frame
    void Update() {
    }
}