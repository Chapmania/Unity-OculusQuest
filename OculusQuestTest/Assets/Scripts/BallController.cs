using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class BallController : MonoBehaviour {
    public Transform player;
    public int speed = 2;

    void Start() {

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
        //var moveHorizontally = Input.GetAxis("Horizontally");
        // Debug.Log($"Horizontally {moveHorizontally}");
        // var moveVertically = Input.GetAxis("Vertically");
        var current = player.position;
        // current.x += moveHorizontally * speed * Time.deltaTime;
        // current.z += moveVertically * speed * Time.deltaTime;
        player.position = current;
    }
}