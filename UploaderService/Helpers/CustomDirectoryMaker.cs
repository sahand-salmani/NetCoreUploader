using System.IO;
using System.Linq;

namespace UploaderService.Helpers
{
    public class DirectoryFactory
    {
        public class CustomDirectory
        {
            private readonly string _path = string.Empty;
            public CustomDirectory(params string[] directories)
            {
                if (directories.Any())
                {
                    _path = directories[0];
                }

                for (var index = 1; index < directories.Length; index++)
                {
                    if (directories[index].Contains("/"))
                    {
                        continue;
                    }
                    _path = Path.Combine(_path, directories[index]);
                }

            }
            public CustomDirectory(string fullPath)
            {
                _path = fullPath;
            }
            public static implicit operator string(CustomDirectory customDirectory)
            {
                return customDirectory._path;
            }
            public static implicit operator CustomDirectory(string text)
            {
                return new CustomDirectory(text);
            }
        }
        public static CustomDirectory MakeDirectory(params string[] directories) => new(directories);
        public static CustomDirectory FullDirectory(string path) => new(path);
    }

}
