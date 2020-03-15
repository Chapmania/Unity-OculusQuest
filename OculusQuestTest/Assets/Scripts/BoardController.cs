using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class BoardController : MonoBehaviour {
    public Button button;
    public TMP_Text title;

    void Start() {
        button.onClick.AddListener(() => { Debug.Log("Start!"); });
        var inputDevices = new List<InputDevice>();
        InputDevices.GetDevices(inputDevices);

        if (inputDevices.Count == 0) {
            Debug.Log("No input device found!");
            title.text = "No input device found!";
        }
        else {
            foreach (var device in inputDevices) {
                Debug.Log($"Device found with name '{device.name}' and role '{device.characteristics.ToString()}'");
                title.text = $"Device found with name '{device.name}' and role '{device.characteristics.ToString()}'";
            }
        }
    }

    void Update() {
    }
}