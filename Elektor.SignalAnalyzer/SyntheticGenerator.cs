using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NCalc;

namespace Elektor.SignalAnalyzer
{
    public class SyntheticGenerator
    {
        public static double[] GenerateData(int n, double Fs, string formula, Hashtable parameters, out string message)
        {           
            message = null;
            double[] data = new double[n];
            try
            {            
                Expression e = new Expression(formula);
                e.Options = EvaluateOptions.IgnoreCase;
                e.EvaluateParameter += delegate (string name, ParameterArgs pargs)
                {
                    if (name.ToLowerInvariant() == "pi")
                        pargs.Result = Math.PI;
                };

                foreach (string param in parameters.Keys)
                {
                    e.Parameters.Add(param, double.Parse((string)parameters[param], System.Globalization.CultureInfo.CurrentUICulture.NumberFormat));
                }
                
                for (int i = 0; i < n; i++)
                {
                    e.Parameters["t"] = i/Fs;
                    data[i] = (double)e.Evaluate();
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return data;
        }


        public static HashSet<string> ExtractVariables(string formula, out string message)
        {
            message = null;
            HashSet<string> extractedParameters = null;
            try
            {
                var expression = Expression.Compile(formula, false);
                ParameterExtractionVisitor visitor = new ParameterExtractionVisitor();
                expression.Accept(visitor);
                extractedParameters = visitor.Parameters;                
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return extractedParameters;
        }

        //public static double[] GenerateData(int n, double Fs, int selection, double carrierfreq, double modfreq, double modlevel)
        //{
        //    double[] data = new double[n];
        //    for (int i = 0; i < n; i++)
        //    {
        //        switch (selection)
        //        {
        //            case 1:
        //                data[i] = (1 + (modlevel / 100) * Math.Cos(2 * Math.PI * modfreq * i / Fs)) * Math.Cos(2 * Math.PI * carrierfreq * i / Fs);      //AM signal
        //                break;
        //            case 2:
        //                data[i] = 4 / Math.PI * (Math.Cos(2 * Math.PI * carrierfreq * i / Fs) - .3333 * Math.Cos(2 * Math.PI * 3 * carrierfreq * i / Fs) + .2 * Math.Cos(2 * Math.PI * 5 * carrierfreq * i / Fs) - .1429 * Math.Cos(2 * Math.PI * 7 * carrierfreq * i / Fs) + .1111 * Math.Cos(2 * Math.PI * 9 * carrierfreq * i / Fs));  //square wave  sum i/n * cos(2*pi*n*f)  
        //                                                                                        //din[k] = Math.Sin(2 * Math.PI * carrierfreq * i / Fs);		//square wave
        //                                                                                        //    if (din[k] >= 0) din[k] = 1;
        //                                                                                        //    if (din[k] < 0) din[k] = -1;
        //                break;
        //            case 3:
        //                data[i] = Math.Sin(2 * Math.PI * carrierfreq * i / Fs + Math.PI * (modlevel / 100) * Math.Cos(2 * Math.PI * modfreq * i / Fs));      //PM signal
        //                                                                                                                                                     //din[k] = Math.Sin(2 * Math.PI * i / Fs * (carrierfreq + modlevel * Math.Sin(2 * Math.PI * modfreq * i / Fs)));		//FM signal
        //                break;
        //            case 4:
        //                data[i] = Math.Sin(2 * Math.PI * 1000 * i / Fs) + .5 * Math.Sin(2 * Math.PI * 2000 * i / Fs + 3 * Math.PI / 4);  //see "Understanding Digital Signal Processing", Lyons, p63
        //                break;
        //        }
        //    }
        //    return data;
        //}
    }
    
}
