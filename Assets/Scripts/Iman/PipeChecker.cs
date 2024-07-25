using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PipeChecker : MonoBehaviour
{
    public Tilemap tilemap;
    public Vector3Int startPoint; // Define your start point in the Unity Editor
    public Vector3Int endPoint; // Define your end point in the Unity Editor

    void Start()
    {
        tilemap = GetComponent<Tilemap>();

        // Optionally set default start and end points here if not set in the Unity Editor
        // startPoint = new Vector3Int(0, 0, 0);
        // endPoint = new Vector3Int(tilemap.cellBounds.xMax - 1, tilemap.cellBounds.yMax - 1, 0);
    }

    public bool ArePipesConnected()
    {
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile != null)
                {
                    Vector3Int tilePosition = new Vector3Int(bounds.xMin + x, bounds.yMin + y, 0);
                    PipeTile pipeTile = GetPipeTile(tile);

                    // Debugging output
                    Debug.Log($"Checking tile at {tilePosition} with type {pipeTile.type}");

                    if (pipeTile.top && !IsConnected(tilePosition, Vector3Int.up, Direction.Bottom))
                    {
                        Debug.Log($"Tile at {tilePosition} has top connection but is not connected to bottom.");
                        return false;
                    }
                    if (pipeTile.bottom && !IsConnected(tilePosition, Vector3Int.down, Direction.Top))
                    {
                        Debug.Log($"Tile at {tilePosition} has bottom connection but is not connected to top.");
                        return false;
                    }
                    if (pipeTile.left && !IsConnected(tilePosition, Vector3Int.left, Direction.Right))
                    {
                        Debug.Log($"Tile at {tilePosition} has left connection but is not connected to right.");
                        return false;
                    }
                    if (pipeTile.right && !IsConnected(tilePosition, Vector3Int.right, Direction.Left))
                    {
                        Debug.Log($"Tile at {tilePosition} has right connection but is not connected to left.");
                        return false;
                    }
                }
            }
        }

        Debug.Log("All pipes are connected.");
        return true;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) // Press 'C' to check connections
        {
            bool connected = ArePipesConnected();
            Debug.Log("Pipes connected: " + connected);
        }
    }
    private PipeTile GetPipeTile(TileBase tile)
    {
        PipeType type = (PipeType)System.Enum.Parse(typeof(PipeType), tile.name);
        return PipeTileDictionary.pipeTiles[type];
    }

    private bool IsConnected(Vector3Int tilePosition, Vector3Int direction, Direction requiredConnection)
    {
        Vector3Int neighborPosition = tilePosition + direction;
        TileBase neighborTile = tilemap.GetTile(neighborPosition);

        if (neighborTile != null)
        {
            PipeTile neighborPipeTile = GetPipeTile(neighborTile);

            switch (requiredConnection)
            {
                case Direction.Top: return neighborPipeTile.top;
                case Direction.Bottom: return neighborPipeTile.bottom;
                case Direction.Left: return neighborPipeTile.left;
                case Direction.Right: return neighborPipeTile.right;
            }
        }

        return false;
    }

    private enum Direction
    {
        Top,
        Bottom,
        Left,
        Right
    }
}

