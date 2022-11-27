using UnityEngine;

public struct MazeCellInfo
{
    public MazeCellInfo(Vector2Int position, MazeWallMask wallMask)
    {
        Position = position;
        WallMask = wallMask;
    }

    public Vector2Int Position { get; private set; }
    public MazeWallMask WallMask { get; private set; }
}