using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PowerManager : MonoBehaviour
{
    public Tilemap tilemap;
    public Transform startPoint;
    public Transform endPoint;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Use a key to trigger the check for demo purposes
        {
            if (IsPathComplete())
            {
                Debug.Log("Puzzle Completed!");
                HighlightPath();
            }
            else
            {
                Debug.Log("Path is not complete.");
            }
        }
    }

    private bool IsPathComplete()
    {
        Vector3Int startCell = tilemap.WorldToCell(startPoint.position);
        Vector3Int endCell = tilemap.WorldToCell(endPoint.position);

        Debug.Log($"Start Cell: {startCell}, End Cell: {endCell}");

        if (!tilemap.HasTile(startCell) || !tilemap.HasTile(endCell))
        {
            Debug.LogError("Start or End cell is not valid.");
            return false;
        }

        Queue<Vector3Int> queue = new Queue<Vector3Int>();
        HashSet<Vector3Int> visited = new HashSet<Vector3Int>();

        queue.Enqueue(startCell);
        visited.Add(startCell);

        while (queue.Count > 0)
        {
            Vector3Int current = queue.Dequeue();
            Debug.Log($"Visiting cell: {current}");

            if (current == endCell)
            {
                Debug.Log("Path found!");
                return true;
            }

            foreach (Vector3Int neighbor in GetNeighbors(current))
            {
                Debug.Log($"Checking neighbor: {neighbor}");

                if (tilemap.HasTile(neighbor))
                {
                    Debug.Log($"Neighbor {neighbor} has tile.");
                    if (AreTilesConnected(current, neighbor))
                    {
                        queue.Enqueue(neighbor);
                        visited.Add(neighbor);
                        Debug.Log($"Enqueueing neighbor: {neighbor}");
                    }
                    else
                    {
                        Debug.Log($"Neighbor {neighbor} is not connected.");
                    }
                }
                else
                {
                    Debug.Log($"Neighbor {neighbor} does not have a tile.");
                }
            }
        }

        Debug.Log("No path found.");
        return false;
    }



    private List<Vector3Int> GetNeighbors(Vector3Int cell)
    {
        List<Vector3Int> neighbors = new List<Vector3Int>
    {
        cell + Vector3Int.up,
        cell + Vector3Int.down,
        cell + Vector3Int.left,
        cell + Vector3Int.right
    };

        foreach (var neighbor in neighbors)
        {
            Debug.Log($"Neighbor: {neighbor}");
        }

        return neighbors;
    }


    private bool AreTilesConnected(Vector3Int from, Vector3Int to)
    {
        CableTile fromTile = tilemap.GetTile<CableTile>(from);
        CableTile toTile = tilemap.GetTile<CableTile>(to);

        if (fromTile == null || toTile == null)
        {
            Debug.Log($"Tiles at {from} or {to} are null.");
            return false;
        }

        bool connected = CableTile.AreConnected(fromTile, toTile, from, to);
        Debug.Log($"Tiles at {from} and {to} connected: {connected}. From Tile Connections: {fromTile.connections}, To Tile Connections: {toTile.connections}");

        return connected;
    }




    private void HighlightPath()
    {
        Vector3Int startCell = tilemap.WorldToCell(startPoint.position);
        Vector3Int endCell = tilemap.WorldToCell(endPoint.position);

        Queue<Vector3Int> queue = new Queue<Vector3Int>();
        HashSet<Vector3Int> visited = new HashSet<Vector3Int>();

        queue.Enqueue(startCell);
        visited.Add(startCell);

        while (queue.Count > 0)
        {
            Vector3Int current = queue.Dequeue();
            tilemap.SetColor(current, Color.green); // Ensure this method works and does not affect pathfinding logic

            if (current == endCell)
            {
                Debug.Log("Path highlighted.");
                break;
            }

            foreach (Vector3Int neighbor in GetNeighbors(current))
            {
                if (tilemap.HasTile(neighbor) && !visited.Contains(neighbor))
                {
                    if (AreTilesConnected(current, neighbor))
                    {
                        queue.Enqueue(neighbor);
                        visited.Add(neighbor);
                    }
                }
            }
        }
    }
}
