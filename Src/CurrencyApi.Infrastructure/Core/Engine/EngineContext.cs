using System.Runtime.CompilerServices;
using CurrencyApi.Application.Interfaces.Core;

namespace CurrencyApi.Infrastructure.Core.Engine
{
    /// <summary>
    /// Provides access to the singleton instance of the App engine.
    /// </summary>
    public class EngineContext
    {
        #region Properties

        /// <summary>
        /// Gets the singleton App engine used to access App services.
        /// </summary>
        public static IEngine Current
        {
            get
            {
                if (Singleton<IEngine>.Instance == null)
                {
                    Create();
                }

                return Singleton<IEngine>.Instance!;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Create a static instance of the application engine.
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static IEngine Create()
        {
            //create AppEngine as engine
            return Singleton<IEngine>.Instance ?? (Singleton<IEngine>.Instance = new AppEngine());
        }

        /// <summary>
        /// Sets the static engine instance to the supplied engine. Use this method to supply your own engine implementation.
        /// </summary>
        /// <param name="engine">The engine to use.</param>
        /// <remarks>Only use this method if you know what you're doing.</remarks>
        public static void Replace(IEngine engine)
        {
            Singleton<IEngine>.Instance = engine;
        }

        #endregion
    }
}
