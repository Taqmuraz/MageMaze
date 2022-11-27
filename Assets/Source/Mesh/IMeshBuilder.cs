using System.Collections.Generic;

public interface IMeshBuilder
{
    void Build(IMeshDescriptor descriptor, int[] indices, IEnumerable<IMeshVertexBuffer> buffers);
}
