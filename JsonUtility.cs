using System.Reflection;
using System.Text.Json;
using System.Text;

namespace AppliLeCrocodile
{
    internal static class JsonUtility
    {
        public static T DeserializeFromSave<T>(string relativePath)
        {
            string fileContent = ReadFileFromSave(relativePath);
            if (fileContent == null || fileContent == string.Empty)
                return default(T);

            T serializedObject = JsonSerializer.Deserialize<T>(fileContent);
            return serializedObject;
        }

        public static bool DeserializeFromAppData<T>(string relativePath, out T obj)
        {
            if(ReadFileFromAppData(relativePath, out string content))
            {
                obj = JsonSerializer.Deserialize<T>(content);
                return true;
            }
            obj = default(T);
            return false;
        }

        public static void SerializeToAppData<T>(T obj, string relativePath)
        {
            string jsonFile = JsonSerializer.Serialize(obj);
            WriteFileToAppData(jsonFile, relativePath);
        }

        public static string ReadFileFromSave(string relativePath)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceName = $"{ApplicationManager.applicationName}.{ApplicationManager.relativeSaveDirectory.Replace("/", ".")}.{relativePath.Replace("/", ".")}";

            using Stream? stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null)
            {
                Console.WriteLine($"The file : {relativePath} are not found");
                return string.Empty;
            }

            using StreamReader reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        public static bool ReadFileFromAppData(string relativePath, out string content)
        {
            string path = Path.Combine(FileSystem.AppDataDirectory, relativePath);
            if(File.Exists(path))
            {
                content = File.ReadAllText(path);
                return true;
            }
            content = string.Empty;
            return false;
        }

        public static void WriteFileToAppData(string content, string relativePath)
        {
            string filePath = Path.Combine(FileSystem.AppDataDirectory, relativePath);
            File.WriteAllText(filePath, content);
        }
    }
}
