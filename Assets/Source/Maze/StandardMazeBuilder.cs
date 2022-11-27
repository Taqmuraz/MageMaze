using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class StandardMazeBuilder : IMazeBuilder, IMeshWriter
{
    GameObject gameObject;
    MazeCellInfo[] cells;
    int pos;

    public StandardMazeBuilder(GameObject gameObject, Vector2Int size)
    {
        this.gameObject = gameObject;
        this.Size = size;
        cells = new MazeCellInfo[size.x * size.y];
    }

    public Vector2Int Size { get; private set; }

    void IMazeWriter.WriteCell(MazeCellInfo cellInfo)
    {
        cells[pos++] = cellInfo;
    }

    void IMazeBuilder.BuildMaze()
    {
        Mesh mesh = new UnityMeshBuilder().Build(this);
        gameObject.AddComponent<MeshFilter>().mesh = mesh;
        gameObject.AddComponent<MeshRenderer>().material = new Material(Shader.Find("Diffuse"));
    }

    struct WallOffset
    {
        public Vector3 position;
        public Quaternion rotation;

        public WallOffset(Vector3 position, Quaternion rotation)
        {
            this.position = position;
            this.rotation = rotation;
        }
    }

    void IMeshWriter.Write(IMeshStream stream)
    {
        float wallDepth = 0.1f;
        float wallEnd = 1f - wallDepth;

        Vector3[] wallVertices = new Vector3[]
        {
            new Vector3(wallEnd, 0f, wallDepth),
            new Vector3(wallEnd, 1f, wallDepth),
            new Vector3(wallDepth, 1f, wallDepth),
            new Vector3(wallDepth, 0f, wallDepth),

            new Vector3(wallEnd, 0f, 0f),
            new Vector3(wallEnd, 1f, 0f),
            new Vector3(wallDepth, 1f, 0f),
            new Vector3(wallDepth, 0f, 0f),
        };
        Vector2[] wallUvs = new Vector2[]
        {
            new Vector2(0f, 0f),
            new Vector2(1f, 0f),
            new Vector2(1f, 1f),
            new Vector2(0f, 1f),

            new Vector2(0f, 0f),
            new Vector2(1f, 0f),
            new Vector2(1f, 1f),
            new Vector2(0f, 1f),
        };
        Vector3[] wallNormals = new Vector3[]
        {
            Vector3.forward,
            Vector3.forward,
            Vector3.forward,
            Vector3.forward,

            Vector3.forward,
            Vector3.forward,
            Vector3.forward,
            Vector3.forward,
        };
        int[] wallIndices = new int[]
        {
            0, 1, 2,
            2, 3, 0,
            5, 1, 0,
            0, 4, 5,
            3, 2, 6,
            6, 7, 3,
            1, 5, 6,
            6, 2, 1
        };
        Dictionary<MazeWallMask, WallOffset> wallOffsets = new Dictionary<MazeWallMask, WallOffset>();
        wallOffsets[MazeWallMask.Left] = new WallOffset(new Vector3(0f, 0f, 1f), Quaternion.AngleAxis(90f, Vector3.up));
        wallOffsets[MazeWallMask.Right] = new WallOffset(new Vector3(1f, 0f, 0f), Quaternion.AngleAxis(-90f, Vector3.up));
        wallOffsets[MazeWallMask.Up] = new WallOffset(new Vector3(1f, 0f, 1f), Quaternion.AngleAxis(180f, Vector3.up));
        wallOffsets[MazeWallMask.Down] = new WallOffset(new Vector3(0f, 0f, 0f), Quaternion.identity);

        IMeshVertex[] wallFloor = new IMeshVertex[]
        {
            new MeshVertex(new Vector3(0f, 0f, 0f), new Vector2(0f, 0f), Vector3.up),
            new MeshVertex(new Vector3(Size.x, 0f, 0f), new Vector2(Size.x, 0f), Vector3.up),
            new MeshVertex(new Vector3(Size.x, 0f, Size.y), new Vector2(Size.x, Size.y), Vector3.up),
            new MeshVertex(new Vector3(0f, 0f, Size.y), new Vector2(0f, Size.y), Vector3.up),
        };
        stream.PushIndex();
        stream.WriteIndices(new int[] { 0, 3, 2, 2, 1, 0 });
        stream.WriteVertices(wallFloor);

        foreach (var cell in cells)
        {
            for (int i = 0; i < 5; i++)
            {
                var wall = (MazeWallMask)(1 << i);
                if ((cell.WallMask & wall) != MazeWallMask.None)
                {
                    var offset = wallOffsets[wall];
                    stream.PushIndex();
                    stream.WriteIndices(wallIndices);
                    stream.WriteVertices(Enumerable.Range(0, wallVertices.Length).Select(v => (IMeshVertex)new MeshVertex
                        (
                            (offset.rotation * wallVertices[v]) + offset.position + new Vector3(cell.Position.x, 0f, cell.Position.y),
                            wallUvs[v],
                            offset.rotation * wallNormals[v]
                        )));
                }
            }
        }
    }
}
