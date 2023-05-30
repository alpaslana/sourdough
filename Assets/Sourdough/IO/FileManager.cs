using System.IO;
using UnityEngine;

namespace Sourdough.IO
{
    public static class FileManager
    {
        public static string FileRootPath => Application.persistentDataPath;

        public static string OpenTextFile(string fileName)
        {
            string fullPath = Path.Combine(FileRootPath + "/" + fileName);
            string fileData = File.ReadAllText(fullPath);

            return fileData;
        }

        public static bool CheckFile(string fileName)
        {
            return File.Exists(Path.Combine(FileRootPath + "/" + fileName));
        }

        public static void SaveTextFile(string fileName, string jsonStringData)
        {
            string fullPath = Path.Combine(FileRootPath + "/" + fileName);
            File.WriteAllText(fullPath, jsonStringData);
        }

        public static void DeleteFile(string fileName)
        {
            if (!CheckFile(fileName)) return;
            File.Delete(Path.Combine(FileRootPath + "/" + fileName));
        }
    }
}