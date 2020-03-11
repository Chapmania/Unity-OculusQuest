using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class GameController : MonoBehaviour {
    void Start() {
        Debug.Log($"Game started with XR {XRSettings.enabled}");
        Debug.Log($"XR device is present: {XRDevice.isPresent}");

        var inputDevices = new List<InputDevice>();
        InputDevices.GetDevices(inputDevices);

        if (inputDevices.Count == 0) {
            Debug.Log("No input device found!");
        }
        else {
            foreach (var device in inputDevices) {
                Debug.Log($"Device found with name '{device.name}' and role '{device.characteristics.ToString()}'");
            }
        }
    }

    void Update() {
    }
}