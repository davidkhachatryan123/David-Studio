using Storage.Options;

namespace Storage
{
    public class StorageSetup
    {
        public static void CreateDirIfNotExists(string path)
        {
            if (!Path.Exists(path))
                Directory.CreateDirectory(path);
        }
    }
}

