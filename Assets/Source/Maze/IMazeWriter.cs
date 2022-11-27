using UnityEngine;

public interface IMazeWriter
{
    Vector2Int Size { get; }
    void WriteCell(MazeCellInfo cellInfo);
}
