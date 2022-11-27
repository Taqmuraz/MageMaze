using System.Collections.Generic;

public interface IMeshStream
{
    void PushIndex();
    void WriteIndices(int[] indices);
    void WriteVertices(IEnumerable<IMeshVertex> vertices);
}