using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileClickHandler : MonoBehaviour
{
    public Tilemap tilemap;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPosition = tilemap.WorldToCell(mouseWorldPos);

            if (tilemap.HasTile(gridPosition))
            {
                TileBase clickedTile = tilemap.GetTile(gridPosition);
                RotateTile(gridPosition);
            }
        }
    }

    void RotateTile(Vector3Int position)
    {
        TileBase tile = tilemap.GetTile(position);
        Matrix4x4 matrix = tilemap.GetTransformMatrix(position);
        matrix.SetTRS(Vector3.zero, Quaternion.Euler(0, 0, 90) * matrix.rotation, Vector3.one);
        tilemap.SetTransformMatrix(position, matrix);
    }
}
