using System;
using UnityEngine;

public abstract class MazeGenerator : MonoBehaviour
{
    protected abstract IMazePattern CreatePattern();
    protected abstract IMazeBuilder CreateBuilder();

    void Start()
    {
        var pattern = CreatePattern();
        var builder = CreateBuilder();
        pattern.Apply(builder);
        builder.BuildMaze();
    }
}
