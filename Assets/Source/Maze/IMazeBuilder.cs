using UnityEngine;

public interface IMazeBuilder : IMazeWriter
{
    void BuildMaze();
}
public interface IMazeWriter
{
    Vector2Int Size { get; }
    void WriteCell(MazeCellInfo cellInfo);
}
