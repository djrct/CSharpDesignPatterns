using System;
using System.Collections.Generic;
using System.IO;

namespace Composite
{
    public class Program
    {
        static void Main()
        {
            FileSystemExplorer.ExploreCDrive();
        }
    }

    public static class FileSystemExplorer
    {
        public static void ExploreCDrive()
        {
            string cDrivePath =
                @"C:\Users\David.Rossi\OneDrive - Numotion\Apps\VSTestProjects\CSharpDesignPatterns\CSharpDesignPatterns\Starter files\Composite\obj";   // Path.GetPathRoot(Environment.CurrentDirectory);
            var cDrive = CreateCDriveComposite(cDrivePath);
            var cDriveSize = cDrive.GetSize();
            Console.WriteLine($"Total size of {cDrivePath}: \r\n {cDriveSize} bytes");

            Console.ReadKey();
        }

        private static Directory CreateCDriveComposite(string rootPath)
        {
            var cDrive = new Composite.Directory(rootPath, 0);

            var rootDirectories = System.IO.Directory.GetDirectories(rootPath);
            var rootFiles = System.IO.Directory.GetFiles(rootPath);

            foreach (var file in rootFiles)
            {
                var fileInfo = new System.IO.FileInfo(file);
                cDrive.Add(new Composite.File(fileInfo.Name, fileInfo.Length));
            }

            foreach (var directory in rootDirectories)
            {
                var directoryInfo = new System.IO.DirectoryInfo(directory);
                var subDirectory = CreateDirectoryStructure(directoryInfo);
                cDrive.Add(subDirectory);
            }

            return cDrive;
        }

        private static Composite.Directory CreateDirectoryStructure(System.IO.DirectoryInfo directoryInfo)
        {
            var directory = new Composite.Directory(directoryInfo.Name, 0);

            var files = directoryInfo.GetFiles();
            foreach (var file in files)
            {
                directory.Add(new Composite.File(file.Name, file.Length));
            }

            var subDirectories = directoryInfo.GetDirectories();
            foreach (var subDir in subDirectories)
            {
                var subDirectory = CreateDirectoryStructure(subDir);
                directory.Add(subDirectory);
            }

            return directory;
        }
    }

    // Rest of your Composite pattern classes (FileSystemItem, File, Directory) go here...
}
