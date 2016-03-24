using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elektor.SignalAnalyzer
{
    /// <summary>
    /// Acquire states
    /// </summary>
    public enum AcquireStates
    {
        Stopped,    // Stopped getting data
        Running,    // Keep on getting data
        Single,      // Wait for trigger then stop
        Immediate, // Get samples and stop
    }
}
