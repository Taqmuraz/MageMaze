using System;

public interface IMeshVertexAttribute
{
    MeshVertexAttributeType Type { get; }
    Type NativeType { get; }
}
