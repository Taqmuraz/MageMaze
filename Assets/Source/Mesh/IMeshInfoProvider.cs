using System.Collections.Generic;

public interface IMeshInfoProvider
{
    IMeshDescriptor Descriptor { get; }
    IEnumerable<IMeshElement> EnumerateElements();
}
