namespace UploaderService.Helpers
{
    public interface IExtensionHandler
    {
        string FindExtension(string fileName);
        string GetFileNameWithoutExtension(string fileName);
    }

    public class ExtensionHandler : IExtensionHandler
    {
        public string FindExtension(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return null;
            }

            var splitString = fileName.Split('.');
            var ext = splitString.Length < 2 ? null : splitString[^1];

            return ext;
        }

        public string GetFileNameWithoutExtension(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return null;
            }

            var extension = FindExtension(fileName);

            return fileName.Remove((fileName.Length - 1) - (extension.Length), extension.Length + 1);
        }
    }
}
