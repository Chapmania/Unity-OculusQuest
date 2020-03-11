using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class GameController : MonoBehaviour
{
    void Start()
    {
        Debug.Log($"Game started with XR {XRSettings.enabled}");
        
    }

    void Update()
    {
    }
}
