using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Tiles/CableTile")]
public class CableTile : Tile
{
    [System.Flags]
    public enum Connection
    {
        None = 0,
        Up = 1 << 0,
        Right = 1 << 1,
        Down = 1 << 2,
        Left = 1 << 3
    }

    public Connection connections;

    public static bool AreConnected(CableTile fromTile, CableTile toTile, Vector3Int fromPos, Vector3Int toPos)
    {
        Vector3Int direction = toPos - fromPos;

        switch (direction)
        {
            case Vector3Int v when v == Vector3Int.up:
                return fromTile.HasConnection(Connection.Up) && toTile.HasConnection(Connection.Down);
            case Vector3Int v when v == Vector3Int.down:
                return fromTile.HasConnection(Connection.Down) && toTile.HasConnection(Connection.Up);
            case Vector3Int v when v == Vector3Int.left:
                return fromTile.HasConnection(Connection.Left) && toTile.HasConnection(Connection.Right);
            case Vector3Int v when v == Vector3Int.right:
                return fromTile.HasConnection(Connection.Right) && toTile.HasConnection(Connection.Left);
            default:
                return false;
        }
    }


    public bool HasConnection(Connection connection)
    {
        return (connections & connection) != 0;
    }
}


