namespace Singleton
{
    public class Logger
    {

        // provides a thread-safe and efficient implementation of a singleton Logger class,
        private static readonly Lazy<Logger> _lazyLogger = new Lazy<Logger>(() => new Logger());

        public static Logger Instance => _lazyLogger.Value;

        protected Logger()
        {

        }

        public void Log(string message)
        {
            Console.WriteLine($"Message to log: {message}");
        }
    }

}
