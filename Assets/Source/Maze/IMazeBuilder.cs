using UnityEngine;

public interface IMazeBuilder
{
    Vector2Int Size { get; }
    void BuildCell(MazeCellInfo cellInfo);
}
