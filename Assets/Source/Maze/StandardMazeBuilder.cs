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
        Size = size;
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
        Vector3[] wallVertices = new Vector3[]
        {
            new Vector3(0f, 0f, 0f),
            new Vector3(1f, 0f, 0f),
            new Vector3(1f, 1f, 0f),
            new Vector3(0f, 1f, 0f),
        };
        Vector2[] wallUvs = new Vector2[]
        {
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
        };
        int[] wallIndices = new int[]
        {
            0, 1, 2,
            2, 3, 0
        };
        Dictionary<MazeWallMask, WallOffset> wallOffsets = new Dictionary<MazeWallMask, WallOffset>();
        wallOffsets[MazeWallMask.Left] = new WallOffset(new Vector3(0f, 0f, 1f), Quaternion.AngleAxis(90f, Vector3.up));
        wallOffsets[MazeWallMask.Right] = new WallOffset(new Vector3(1f, 0f, 0f), Quaternion.AngleAxis(-90f, Vector3.up));
        wallOffsets[MazeWallMask.Up] = new WallOffset(new Vector3(1f, 0f, 1f), Quaternion.AngleAxis(180f, Vector3.up));
        wallOffsets[MazeWallMask.Down] = new WallOffset(new Vector3(0f, 0f, 0f), Quaternion.identity);
        WallOffset floor = new WallOffset(new Vector3(0f, 0f, 1f), Quaternion.AngleAxis(-90f, Vector3.right));

        foreach (var cell in cells)
        {
            for (int i = 0; i < 5; i++)
            {
                WallOffset offset;
                if (i == 4)
                {
                    offset = floor;
                }
                else
                {
                    var wall = (MazeWallMask)(1 << i);
                    if ((cell.WallMask & wall) != MazeWallMask.None)
                    {
                        offset = wallOffsets[wall];
                    }
                    else continue;
                }
                stream.PushIndex();
                stream.WriteIndices(wallIndices);
                stream.WriteVertices(Enumerable.Range(0, 4).Select(v => (IMeshVertex)new MeshVertex
                    (
                        (offset.rotation * wallVertices[v]) + offset.position + new Vector3(cell.Position.x, 0f, cell.Position.y),
                        wallUvs[v],
                        offset.rotation * wallNormals[v]
                    )));
            }
        }
    }
}
