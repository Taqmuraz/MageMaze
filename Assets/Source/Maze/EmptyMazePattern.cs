using UnityEngine;

public sealed class EmptyMazePattern : IMazePattern
{
    public void Apply(IMazeWriter writer)
    {
        for (int x = 0; x < writer.Size.x; x++)
        {
            for (int y = 0; y < writer.Size.y; y++)
            {
                MazeWallMask mask = MazeWallMask.None;
                if (x == 0) mask |= MazeWallMask.Left;
                if (x == writer.Size.x - 1) mask |= MazeWallMask.Right;
                if (y == 0) mask |= MazeWallMask.Down;
                if (y == writer.Size.y - 1) mask |= MazeWallMask.Up;

                writer.WriteCell(new MazeCellInfo(new Vector2Int(x, y), mask));
            }
        }
    }
}
