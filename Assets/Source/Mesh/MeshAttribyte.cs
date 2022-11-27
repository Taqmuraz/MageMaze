using System;

public sealed class MeshAttribyte : IMeshVertexAttribute
{
    public MeshAttribyte(MeshVertexAttributeType type, Type nativeType)
    {
        Type = type;
        NativeType = nativeType;
    }

    public MeshVertexAttributeType Type { get; private set; }
    public Type NativeType { get; private set; }
}
