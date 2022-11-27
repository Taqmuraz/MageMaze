using System;

public interface IMeshVertexBuffer
{
    MeshVertexAttributeType Type { get; }
    void Write(object value);
    Array ToArray();
}
