public static class MeshGenerator
{
    public static void GenerateMesh(IMeshInfoProvider infoProvider, IMeshBuilder builder)
    {
        MeshStream stream = new MeshStream();
        foreach (var element in infoProvider.EnumerateElements())
        {
            element.Write(stream);
        }
        stream.Build(infoProvider.Descriptor, builder);
    }
}