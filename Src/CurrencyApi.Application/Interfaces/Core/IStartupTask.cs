namespace CurrencyApi.Application.Interfaces.Core
{
    /// <summary>
    /// Interface which should be implemented by tasks run on startup
    /// </summary>
    public interface IStartupTask
    {
        /// <summary>
        /// Gets order of this startup task implementation
        /// </summary>
        int Order { get; }

        /// <summary>
        /// Executes a task
        /// </summary>
        void Execute();
    }
}
