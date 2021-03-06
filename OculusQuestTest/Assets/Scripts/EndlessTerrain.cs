﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class EndlessTerrain : MonoBehaviour {
    public const float maxViewDst = 450;
    public Transform viewer;
    public static Vector2 viewerPosition;
    public int chunkSize;
    private int chunkVisibleInViewDst;

    Dictionary<Vector2, TerrainChunk> terrainChunkDictionary = new Dictionary<Vector2, TerrainChunk>();
    private List<TerrainChunk> terrainChunksVisibleLastUpdate = new List<TerrainChunk>();

    void Start() {
        chunkSize = MapGenerator.mapChunkSize - 1;
        chunkVisibleInViewDst = Mathf.RoundToInt(maxViewDst / chunkSize);
    }

    public void Update() {
        viewerPosition = new Vector2(viewer.position.x, viewer.position.z);
        UpdateVisibleChunks();
    }

    void UpdateVisibleChunks() {
        terrainChunksVisibleLastUpdate.ForEach(chunk => chunk.SetVisible(false));
        terrainChunksVisibleLastUpdate.Clear();

        int currentChunkCoordX = Mathf.RoundToInt(viewerPosition.x / chunkSize);
        int currentChunkCoordY = Mathf.RoundToInt(viewerPosition.y / chunkSize);
        for (int yOffset = -chunkVisibleInViewDst; yOffset < chunkVisibleInViewDst; yOffset++) {
            for (int xOffset = -chunkVisibleInViewDst; xOffset <= chunkVisibleInViewDst; xOffset++) {
                var viewedChunkCoord = new Vector2(currentChunkCoordX + xOffset, currentChunkCoordY + yOffset);
                if (terrainChunkDictionary.ContainsKey(viewedChunkCoord)) {
                    var terrainChunk = terrainChunkDictionary[viewedChunkCoord];
                    terrainChunk.UpdateTerrainChunk();
                    if (terrainChunk.IsVisible()) {
                        terrainChunksVisibleLastUpdate.Add(terrainChunk);
                    }
                }
                else {
                    terrainChunkDictionary.Add(viewedChunkCoord, new TerrainChunk(viewedChunkCoord, chunkSize, transform));
                }
            }
        }
    }

    public class TerrainChunk {
        GameObject meshObject;
        Vector2 position;
        private Bounds bounds;

        public TerrainChunk(Vector2 coord, int size, Transform parent) {
            position = coord * size;
            bounds = new Bounds(position, Vector3.one * size);
            var positionV3 = new Vector3(position.x, 0, position.y);

            meshObject = GameObject.CreatePrimitive(PrimitiveType.Plane);
            meshObject.transform.position = positionV3;
            meshObject.transform.localScale = Vector3.one * size / 10f;
            meshObject.transform.parent = parent;
            SetVisible(false);
        }

        public void UpdateTerrainChunk() {
            var viewerDstFromNearestEdge = Math.Sqrt(bounds.SqrDistance(viewerPosition));
            var visible = viewerDstFromNearestEdge <= maxViewDst;
            SetVisible(visible);
        }

        public void SetVisible(bool visible) {
            meshObject.SetActive(visible);
        }

        public bool IsVisible() {
            return meshObject.activeSelf;
        }
    }
}