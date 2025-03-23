namespace DotNetApiLogging
{
    /// <summary>
    /// A scratch directory, a temporary scrach space that gets cleaned up when the object is disposed
    /// </summary>
    public class ScratchDirectory : IDisposable
    {
        /// <summary>
        /// Flag that indicates when the object has been disposed
        /// </summary>
        private bool disposedValue;

        /// <summary>
        /// Creates a new scratch directory
        /// </summary>
        /// <param name="rootDirectory">The root path of the scratch directory, a unique subdiretory will
        /// be createed below the root directory for the scratch files</param>
        public ScratchDirectory(string rootDirectory)
        {
            // the actual scratch directory
            Directory = Path.Combine(rootDirectory, Guid.NewGuid().ToString());
            if (!System.IO.Directory.Exists(Directory))
            {
                System.IO.Directory.CreateDirectory(Directory);
            }
        }

        /// <summary>
        /// Disposes the scratch directory
        /// </summary>
        ~ScratchDirectory()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }

        /// <summary>
        /// The root directory under which a scratch sub-directory will be created below
        /// </summary>
        public string RootDirectory { get; private set; } = string.Empty;

        /// <summary>
        /// The scatch directory
        /// </summary>
        public string Directory { get; private set; } = string.Empty;

        /// <summary>
        /// Disposes the scratch directory
        /// </summary>
        /// <param name="disposing">Whether the object is disposing</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }
                if (System.IO.Directory.Exists(Directory))
                {
                    System.IO.Directory.Delete(Directory, true);
                }
                disposedValue = true;
            }
        }

        /// <summary>
        /// Method to dispose the object
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
