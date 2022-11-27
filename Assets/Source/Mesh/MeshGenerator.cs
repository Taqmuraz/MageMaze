public static class MeshGenerator
{
    public static void GenerateMesh(IMeshDescriptor descriptor, IMeshWriter writer, IMeshBuilder builder)
    {
        MeshStream stream = new MeshStream();
        writer.Write(stream);
        stream.Build(descriptor, builder);
    }
}
