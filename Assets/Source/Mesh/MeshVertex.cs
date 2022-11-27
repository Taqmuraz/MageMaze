using UnityEngine;

public sealed class MeshVertex : IMeshVertex
{
    Vector3 position;
    Vector2 uv;
    Vector3 normal;

    public MeshVertex(Vector3 position, Vector2 uv, Vector3 normal)
    {
        this.position = position;
        this.uv = uv;
        this.normal = normal;
    }

    public void Write(IMeshVertexBuffer buffer)
    {
        switch (buffer.Type)
        {
            case MeshVertexAttributeType.Position:
                buffer.Write(position);
                break;
            case MeshVertexAttributeType.Texcoord:
                buffer.Write(uv);
                break;
            case MeshVertexAttributeType.Normal:
                buffer.Write(normal);
                break;
        }
    }
}
