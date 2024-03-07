namespace Composite
{
    /// <summary>
    /// Component
    /// </summary>
    public abstract class FileSystemItem
    {
        public string Name { get; set; }

        public abstract long GetSize();

        public FileSystemItem(string name)
        {
            Name = name;
        }
    }

    /// <summary>
    /// Leaf
    /// </summary>
    public class File : FileSystemItem
    {
        private long _size;
        public File(string name, long size) : base(name)
        {
            _size = size;
        }

        public override long GetSize()
        {
            return _size;
        }
    }

    /// <summary>
    /// Composite
    /// </summary>
    public class Directory : FileSystemItem
    {
        private List<FileSystemItem> _fileSystemItems = new List<FileSystemItem>();

        private long _size;
        public Directory(string name, long size) : base(name)
        {
            _size = size;
        }

        public void Add(FileSystemItem itemToAdd)
        {
            _fileSystemItems.Add(itemToAdd);
        }

        public void Remove(FileSystemItem itemToRemove)
        {
            _fileSystemItems.Remove(itemToRemove);
        }

        public override long GetSize()
        {
            var treeSize = _size;
            foreach (var fileSystemItem in _fileSystemItems)
            {
                var type = fileSystemItem.GetType();
                // Print information about a file system item, including its type, name, and size (if applicable).
                // For directories, the size is set to 0, and the function should not be called recursively.
                Console.WriteLine($"{type.FullName,-20} name: {fileSystemItem.Name,-55} size: {((type == typeof(Composite.Directory)) ? 0 : fileSystemItem.GetSize())}");
                treeSize += fileSystemItem.GetSize();

            }
            return treeSize;
        }
    }
}
