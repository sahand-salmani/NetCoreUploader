using System;
using System.IO;
using System.Linq;

namespace UploaderService.Helpers
{
    public interface ICreateDirectoryHandler
    {
        bool CreateDirectory(string path);
        bool CheckOrCreateDirectory(string path);
    }
    public interface IDirectoryExistenceHandler
    {
        bool DirectoryExists(string path);
    }
    public interface IModifyDirectoryHandler
    {
        bool RenameDestinationFolder(string path, string name);
        bool RenameDirectory(string path, string newPath);
    }
    public interface IRemoveDirectoryHandler
    {
        bool RemoveDirectory(string path);
        bool RemoveAllFiles(string path);
        bool RemoveAllSubDirectories(string path);
    }
    public interface IDestinationFolderChanger
    {
        string ChangeFolderName(string path, string name);
    }

    public interface IDirectoryHandler : ICreateDirectoryHandler, IDirectoryExistenceHandler, IModifyDirectoryHandler, IRemoveDirectoryHandler, IDestinationFolderChanger
    {
    }
    public class DirectoryHandler : IDirectoryHandler
    {

        public bool RenameDestinationFolder(string path, string name)
        {
            if (!Directory.Exists(path) || string.IsNullOrEmpty(name))
            {
                return false;
            }

            var newPath = ChangeFolderName(path, name);

            Directory.Move(path, newPath);

            return Directory.Exists(newPath);
        }

        public bool RenameDirectory(string path, string newPath)
        {
            if (!Directory.Exists(path) || string.IsNullOrEmpty(newPath))
            {
                return false;
            }

            if (Directory.Exists(newPath))
            {
                return false;
            }

            Directory.Move(path, newPath);

            return Directory.Exists(newPath);

        }
        public bool DirectoryExists(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }

            return Directory.Exists(path);
        }

        public bool CreateDirectory(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }

            var _ = Directory.CreateDirectory(path);

            return Directory.Exists(path);
        }

        public bool CheckOrCreateDirectory(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }
            if (!Directory.Exists(path))
            {
                return CreateDirectory(path);
            }

            return DirectoryExists(path);
        }

        public string CombineDirectory(string first, params string[] others)
        {
            if (string.IsNullOrEmpty(first))
            {
                throw new ArgumentNullException();
            }

            var path = first;
            foreach (var other in others)
            {
                path = Path.Combine(path, other);
            }

            return path;
        }

        public bool RemoveDirectory(string path)
        {
            Directory.Delete(path);
            return !Directory.Exists(path);
        }

        public bool RemoveAllFiles(string path)
        {
            Directory.Delete(path);
            Directory.CreateDirectory(path);
            return Directory.Exists(path);
        }

        public bool RemoveAllSubDirectories(string path)
        {
            DirectoryInfo directory = new DirectoryInfo(path);

            foreach (FileInfo file in directory.EnumerateFiles())
            {
                file.Delete();
            }

            foreach (DirectoryInfo directoryInfo in directory.GetDirectories())
            {
                directoryInfo.Delete();
            }

            return !directory.GetFiles().Any() && !directory.GetDirectories().Any();
        }

        public string ChangeFolderName(string path, string name)
        {
            if (string.IsNullOrEmpty(path) || string.IsNullOrEmpty(name))
            {
                return null;
            }

            var splitPath = path.Split('\\');
            if (!splitPath.Any())
            {
                return null;
            }

            splitPath[^1] = name;

            return string.Join('\\', splitPath);
        }

    }
}
