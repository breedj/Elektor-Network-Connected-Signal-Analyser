using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elektor.SignalAnalyzer
{
    /// <summary>
    /// Event arguments used for scope events
    /// </summary>
    public class ScopeEventArgs : EventArgs
    {
        public ScopeEventArgs(ScopeData scopeData)
        {
            _scopeData = scopeData;
        }
        private ScopeData _scopeData;

        public ScopeData ScopeData
        {
            get { return _scopeData; }
            set { _scopeData = value; }
        }
    }
    
}
