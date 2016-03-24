namespace Elektor.SignalAnalyzer
{
    public class SamplingSettings
    {
        /// <summary>
        /// Sample frequency
        /// </summary>
        public int Fs { get; set; }
        public int M { get; set; }
        public byte N1 { get; set; }
        public byte N2 { get; set; }

        /// <summary>
        /// Calculated cpu clock frequency
        /// </summary>
        public int Fcpu { get; set; }

        /// <summary>
        /// Set ADCS. If null then auto
        /// </summary>
        public byte? ADCS { get; set; }
    }
}