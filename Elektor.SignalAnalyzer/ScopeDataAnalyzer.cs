using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elektor.SignalAnalyzer
{
    public class ScopeDataAnalyzer
    {

        // Conversion
        const double AmpGain = 3.902439;  //Nominal value; AmpGain = 4k/(1k + 50//50); 
        const double DcBias = 1.470732;   //Nominal value; bias=(1+4000/1025)*Vref(=.3v)
        

        #region Public Methods

        public ScopeData ScopeDataFromSamples(double[] samples)
        {
            ScopeData scopeData = new ScopeData();

            // Calculate DC term
            scopeData.DCterm = CalculateDcTerm(samples);    //calculates signal average (DC term)

            // Calculate DC term in volts
            scopeData.DCVoltage = ((DcBias - scopeData.DCterm) / AmpGain); // in volts
            double[] voltages = new double[samples.Length];
            CalculateVoltages(Coupling == CouplingModes.AC ? scopeData.DCterm : DcBias, samples, AmpGain, voltages);  //subtracts DC, scales for ADC bits, puts data in real part of din[]            
            scopeData.SampleInterval = 1 / (float)SamplesPerSecond;
            scopeData.SampleCount = samples.Length;            
            scopeData.Voltages = voltages;
            scopeData.TriggerSamples = DetermineTriggerPoints(voltages);

            // Calculate base frequency
            if (scopeData.TriggerSamples.Count > 2)                            
                scopeData.BaseFrequency = 1 / ((scopeData.TriggerSamples[scopeData.TriggerSamples.Count - 1] - scopeData.TriggerSamples[0]) / (double)(scopeData.TriggerSamples.Count - 1) * (double)scopeData.SampleInterval);            
            else
                scopeData.BaseFrequency = null;

            scopeData.Triggered = scopeData.TriggerSamples.Any();
            if (scopeData.Triggered && scopeData.TriggerSamples.Count > 1)
                scopeData.Statistics.Calculate(voltages, scopeData.TriggerSamples.First(), TriggerMode != TriggerModes.Off); // Calculate statistics
            else
                scopeData.Statistics.Calculate(voltages, null, TriggerMode != TriggerModes.Off); // Calculate statistics
            return scopeData;
        }

        #endregion


        #region Public Properties

        public int SamplesPerSecond
        {
            get;
            set;
        }

        public double TriggerLevel
        {
            get;
            set;
        }

        public TriggerSlopes TriggerSlope
        {
            get;
            set;
        }


        public TriggerModes TriggerMode
        {
            get;
            set;
        }         


        public CouplingModes Coupling
        {
            get;
            set;
        }

        #endregion


        #region Private Methods

        /// <summary>
        /// Get a list of sample numbers on which the trigger matches
        /// </summary>
        /// <param name="voltages"></param>
        /// <returns></returns>
        private List<int> DetermineTriggerPoints(double[] voltages)
        {
            List<int> triggerSamples = new List<int>();
            if (TriggerMode != TriggerModes.Off)
            {
                if (TriggerSlope == TriggerSlopes.RisingEdge)
                {
                    for (int i = 0; i < voltages.Length; i++)
                    {
                        if (i > 0 && voltages[i - 1] < TriggerLevel && voltages[i] >= TriggerLevel)
                        {
                            triggerSamples.Add(i);
                        }
                    }
                }
                else if (TriggerSlope == TriggerSlopes.FallingEdge)
                {
                    for (int i = 0; i < voltages.Length; i++)
                    {
                        if (i > 0 && voltages[i - 1] > TriggerLevel && voltages[i] <= TriggerLevel)
                        {
                            triggerSamples.Add(i);
                        }
                    }
                }
            }
            return triggerSamples;
        }

        #endregion


        #region Private Functions

        /// <summary>
        /// Calculates the DC term
        /// </summary>
        /// <param name="samples">Samples to calculate from</param>
        /// <returns>DC term</returns>
        static double CalculateDcTerm(double[] samples)
        {
            double avg = samples.Average();
            return avg;
        }

        /// <summary>
        /// Calculate the voltages of the samples
        /// </summary>
        /// <param name="dcTerm">Dc term</param>
        /// <param name="samples">Samples to calculate from</param>
        /// <param name="ampGain">Amplifier gain</param>
        /// <param name="voltages">voltages output</param>
        static void CalculateVoltages(double dcTerm, double[] samples, double ampGain, double[] voltages)
        {
            for (int j = 0; j < samples.Length; j++)
            {
                voltages[j] = -(-dcTerm + samples[j]) / ampGain;                
            }            
        }

        #endregion

    }
}
