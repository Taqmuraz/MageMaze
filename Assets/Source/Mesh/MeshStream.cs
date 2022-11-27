using System.Collections.Generic;
using System.Linq;

public sealed class MeshStream : IMeshStream
{
    List<int> indices = new List<int>();
    List<IMeshVertex> vertices = new List<IMeshVertex>();
    int lastIndex;

    public void PushIndex()
    {
        lastIndex = vertices.Count;
    }

    public void WriteIndices(int[] indices)
    {
        this.indices.AddRange(indices.Select(i => i + lastIndex));
    }

    public void WriteVertices(IEnumerable<IMeshVertex> vertices)
    {
        this.vertices.AddRange(vertices);
    }

    public void Build(IMeshDescriptor descriptor, IMeshBuilder builder)
    {
        var buffers = descriptor.EnumerateAttributes().Select(a => new MeshVertexBuffer(vertices.Count, a)).ToArray();
        foreach (var buffer in buffers)
        {
            foreach (var vertex in vertices)
            {
                vertex.Write(buffer);
            }
        }

        builder.Build(descriptor, indices.ToArray(), buffers);
    }
}
