﻿using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MapGenerator))]
public class MapGeneratorEditor : Editor {
    public override void OnInspectorGUI() {
        var mapGen = (MapGenerator) target;

        if (DrawDefaultInspector()) {
            if (mapGen.autoUpdate) {
                mapGen.GenerateNoiseMap();
            }
        }

        if (GUILayout.Button("Generate")) {
            mapGen.GenerateNoiseMap();
        }
    }
}