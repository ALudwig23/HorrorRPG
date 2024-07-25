using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PipeRotator : MonoBehaviour
{
    private Tilemap tilemap;
    private PipeChecker pipeChecker;

    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        pipeChecker = GetComponent<PipeChecker>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPosition = tilemap.WorldToCell(worldPoint);

            if (tilemap.HasTile(gridPosition))
            {
                TileBase tile = tilemap.GetTile(gridPosition);
                RotateTile(gridPosition, tile);

                // Check connections after rotation
                bool connected = pipeChecker.ArePipesConnected();
                Debug.Log("Are pipes connected? " + connected);
            }
        }
    }

    void RotateTile(Vector3Int position, TileBase tile)
    {
        Matrix4x4 matrix = tilemap.GetTransformMatrix(position);
        matrix *= Matrix4x4.Rotate(Quaternion.Euler(0, 0, -90));
        tilemap.SetTransformMatrix(position, matrix);
    }
}


