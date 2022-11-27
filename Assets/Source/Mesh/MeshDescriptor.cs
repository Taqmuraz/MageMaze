using System.Collections.Generic;
using UnityEngine;

public sealed class MeshDescriptor : IMeshDescriptor
{
    public IEnumerable<IMeshVertexAttribute> EnumerateAttributes()
    {
        yield return new MeshAttribyte(MeshVertexAttributeType.Position, typeof(Vector3));
        yield return new MeshAttribyte(MeshVertexAttributeType.Texcoord, typeof(Vector2));
        yield return new MeshAttribyte(MeshVertexAttributeType.Normal, typeof(Vector3));
    }
}