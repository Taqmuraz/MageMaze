using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnityMeshBuilder : IMeshBuilder
{
    Mesh mesh;

    void IMeshBuilder.Build(IMeshDescriptor descriptor, int[] indices, IEnumerable<IMeshVertexBuffer> buffers)
    {
        var bufferMap = buffers.ToDictionary(b => b.Type);
        mesh.subMeshCount = 1;

        mesh.vertices = (Vector3[])bufferMap[MeshVertexAttributeType.Position].ToArray();
        mesh.uv = (Vector2[])bufferMap[MeshVertexAttributeType.Texcoord].ToArray();
        mesh.normals = (Vector3[])bufferMap[MeshVertexAttributeType.Normal].ToArray();
        mesh.triangles = indices;

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();
    }
    public Mesh Build(IMeshWriter writer)
    {
        mesh = new Mesh();
        MeshGenerator.GenerateMesh(new MeshDescriptor(), writer, this);
        return mesh;
    }
}