using UnityEngine;

public sealed class StandardMazeGenerator : MazeGenerator
{
    protected override IMazeBuilder CreateBuilder()
    {
        return new StandardMazeBuilder(gameObject, new Vector2Int(16, 16));
    }
    protected override IMazePattern CreatePattern()
    {
        return new EllerMazePattern(0.5f);
    }
}