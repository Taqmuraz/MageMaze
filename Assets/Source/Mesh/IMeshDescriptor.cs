using System.Collections.Generic;

public interface IMeshDescriptor
{
    IEnumerable<IMeshVertexAttribute> EnumerateAttributes();
}
