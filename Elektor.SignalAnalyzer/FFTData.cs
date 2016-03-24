using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elektor.SignalAnalyzer
{
    public class FFTData
    {
        public FFTData(double[] freqDomain, double[] vrms, int samplesPerSecond)
        {
            FreqDomain = freqDomain;
            Vrms = vrms;
            SamplesPerSecond = samplesPerSecond;                      
        }

        /// <summary>
        /// Contains the frequency domain
        /// </summary>
        public double[] FreqDomain { get; set; }

        /// <summary>
        /// Contains square root of magnitude, scaled
        /// </summary>
        public double[] Vrms { get; set; }

        /// <summary>
        /// Samples per second used
        /// </summary>
        public int SamplesPerSecond { get; set; }

        
        /// <summary>
        /// The calculated base frequency, based on magnitude
        /// </summary>
        public double BaseFrequency
        {
            get
            {
                double highest = Double.MinValue;
                double frequency = 0;
                int indexNumber = 0;
                for (int i = 0; i < Vrms.Length; i++)
                {
                    if (Vrms[i] > highest)
                    {
                        frequency = i * ResolutionBandWith;
                        indexNumber = i;
                        highest = Vrms[i];
                    }
                }

                // You can estimate the actual frequency of a discrete frequency component to a greater resolution than the f given by the FFT 
                // by performing a weighted average of the frequencies around a detected peak in the power spectrum.
                // Source: http://www.ni.com/white-paper/4278/en/
                if (indexNumber > 3)
                {
                    // Calculate weighted average of 6 points.
                    double x = 0, y = 0;
                    for (int i = indexNumber - 3; i <= indexNumber + 3; i++)
                    {
                        x += Vrms[i] * i * ResolutionBandWith;
                        y += Vrms[i];
                    }
                    frequency = x / y;
                }

                return frequency;
            }
        }

        /// <summary>
        /// The resolution bandwith
        /// </summary>
        public double ResolutionBandWith
        {
            get { return SamplesPerSecond / (FreqDomain.Length * 2.0); }
        }                
    }
}
