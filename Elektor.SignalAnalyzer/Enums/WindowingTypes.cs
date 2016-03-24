using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elektor.SignalAnalyzer
{
    public enum WindowingTypes
    {
        Square,
        Parzen,
        Welch,
        Hanning,
        Hamming,
        Blackman,
        Steeper30db
    }
}
