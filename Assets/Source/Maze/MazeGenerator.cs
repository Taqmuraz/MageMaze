using UnityEngine;

public sealed class MazeGenerator : MonoBehaviour
{

}
public sealed class StandardMazeBuilder : IMazeBuilder
{
    public StandardMazeBuilder(Vector2Int size)
    {
        Size = size;
    }

    public Vector2Int Size { get; private set; }

    public void BuildCell(MazeCellInfo cellInfo)
    {
    }
}
