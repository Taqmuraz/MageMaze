using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class EllerMazePattern : IMazePattern
{
    float wallChance;

    public EllerMazePattern(float wallChance)
    {
        this.wallChance = wallChance;
    }

    public void Apply(IMazeWriter writer)
    {
        Vector2Int size = writer.Size;
        MazeWallMask[,] cells = new MazeWallMask[size.x, size.y];
        int[] indices = Enumerable.Range(0, size.x).ToArray();
        int index = 0;

        for (int y = 0; y < size.y; y++)
        {
            for (int x = 0; x < size.x; x++)
            {
                indices[x] = y == 0 ? index++ : ((cells[x, y] & MazeWallMask.Down) != MazeWallMask.None ? index++ : indices[x]);

                if (y == 0)
                {
                    cells[x, y] |= MazeWallMask.Down;
                }
            }

            for (int x = 0; x < size.x; x++)
            {
                if (x > 0)
                {
                    if (Random.value < wallChance)
                    {
                        cells[x, y] |= MazeWallMask.Left;
                        cells[x - 1, y] |= MazeWallMask.Right;
                    }
                    else
                    {
                        indices[x] = indices[x - 1];
                    }
                    if (x == size.x - 1) cells[x, y] |= MazeWallMask.Right;
                }
                else
                {
                    cells[x, y] |= MazeWallMask.Left;
                }
            }

            if (y < size.y - 1)
            {
                Dictionary<int, List<int>> groups = new Dictionary<int, List<int>>();
                int lastIndex = -1;

                for (int x = 0; x < size.x; x++)
                {
                    if (indices[x] == lastIndex)
                    {
                        groups[indices[x]].Add(x);
                    }
                    else
                    {
                        groups.Add(indices[x], new List<int>() { x });
                    }
                    lastIndex = indices[x];
                }

                foreach (var group in groups.Values)
                {
                    int r = group[Random.Range(0, group.Count)];
                    foreach (var g in group)
                    {
                        if (g == r) continue;

                        cells[g, y] |= MazeWallMask.Up;
                        cells[g, y + 1] |= MazeWallMask.Down;
                    }
                }
            }
            else
            {
                for (int x = 0; x < size.x; x++) cells[x, y] |= MazeWallMask.Up;
            }
        }

        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                writer.WriteCell(new MazeCellInfo(new Vector2Int(x, y), cells[x, y]));
            }
        }
    }
}