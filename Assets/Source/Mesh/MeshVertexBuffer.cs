using System;

public sealed class MeshVertexBuffer : IMeshVertexBuffer
{
    Array array;
    int position;

    public MeshVertexBuffer(int size, IMeshVertexAttribute attribute)
    {
        Type = attribute.Type;
        array = Array.CreateInstance(attribute.NativeType, size);
    }

    public MeshVertexAttributeType Type { get; private set; }

    public void Write(object value)
    {
        array.SetValue(value, position++);
    }

    public Array ToArray()
    {
        return array;
    }
}