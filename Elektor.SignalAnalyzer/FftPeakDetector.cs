using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Elektor.SignalAnalyzer
{
    /// <summary>
    /// Ported FFT Peakdetector from matlab
    /// Work in progress!
    /// </summary>
    public class FftPeakDetector
    {
        /// <summary>
        /// Find up to npeaks interpolated peaks in data.
        /// </summary>
        /// <returns></returns>
        public static PeakDetectionData FindPeaks(double[] data, int? npeaks = null, double? minwidth = null, double? maxwidth = null, double? minpeak = null)
        {
            if (!minpeak.HasValue)
                minpeak = data.Min();

            if (!maxwidth.HasValue)
                maxwidth = 0;

            if (!minwidth.HasValue)
                minwidth = 1;

            if (!npeaks.HasValue)
                npeaks = data.Length;

            PeakDetectionData result = new PeakDetectionData
            {
                PeakAmps = new double[npeaks.Value],
                PeakLocs = new double[npeaks.Value],
                PeakWidths = new double[npeaks.Value]
            };

            var nrej = 0;
            var ipeak = 0;

            while (ipeak < npeaks)
            {
                
            }
            /*
                        

pdebug=debug;
ipeak=0;
while ipeak<npeaks
 [ploc, pamp, pcurv] = maxr(data);
 if (pamp==minpeak), warning('findpeaks:min amp reached');
        break;
 end
 plocq = round(ploc);
 ulim = min(len,plocq+1);
 camp = pamp;
 %
 % Follow peak down to determine its width
 %
 drange = max(data) - minpeak; % data dynamic range
 tol = drange * 0.01;
 dmin = camp;
 while ((ulim<len) & (data(ulim)<=dmin+tol)),
   camp = data(ulim);
   ulim = ulim + 1;
   if (camp<dmin), dmin=camp; end
 end;
 ulim = ulim - 1;
 lamp = camp;

 llim = max(1,plocq-1);
 camp = pamp;
 dmin = camp;
 while ((llim>1) & (data(llim)<=dmin+tol)),
   camp = data(llim);
   llim = llim - 1;
   if (camp<dmin), dmin=camp; end
 end;
 llim = llim + 1;
 uamp = camp;
 %
 % Remove the peak
 %
 data(llim:ulim) = min(lamp,uamp) * ones(1,ulim-llim+1);
 %
 % Reject peaks which are too narrow (indicated by zero loc and amp)
 %
 pwid = ulim - llim + 1;
 if ~(pwid < minwidth),
   ipeak = ipeak + 1;
   peaklocs(ipeak) = ploc;
   peakamps(ipeak) = pamp;
   peakwidths(ipeak) = - 1/pcurv;
   nrej = 0;
   if pdebug>1
      peaksarr(plocq) = pamp;
      maxloc = min(len,2*round(max(peaklocs)));
      ttl = sprintf(...
        'Peak %d = %0.2f at %0.2f, width %d',ipeak,pamp,ploc,pwid);
      if x == -1
        pdebug = 0;
      end
   end
 else
   nrej = nrej + 1;
   if (nrej >= 10),
     warning('*** findpeaks: giving up (10 rejected peaks in a row)');
     break;
   end;
 end;
end;
if (ipeak<npeaks),
 warning(sprintf(...
   '*** peaks.m: only %d peaks found instead of %d',ipeak,npeaks));
 peakamps = peakamps(1:ipeak);
 peaklocs = peaklocs(1:ipeak);
 peakwidths = peakwidths(1:ipeak);
end;
resid = data;
            */
            return result;
        }



        private MaxrResult Maxr(double[] a)
        {
            /*
            function [xi,yi,hc] = maxr(a)
            %MAXR   Find interpolated maximizer(s) and max value(s)
            %       for (each column of) a.
            %
            %               [xi,yi,hc] = maxr(a)
            %
            %  Calls max() followed by qint() for quadratic interpolation.
            %
               [m,n] = size(a);
               if m==1, a=a'; t=m; m=n; n=t; end;
               [y,x] = max(a);
               xi=x;    % vector of maximizer locations, one per col of a
               yi=y;    % vector of maximum values, one per column of a
               if nargout>2, hc = zeros(1,n); end
               for j=1:n,   % loop over columns
                 if x(j)>1, % only support interior maxima
                   if x(j)<m,
                     [xdelta,yij,cj] = qint(a(x(j)-1,j),y(j),a(x(j)+1,j));
                     xi(j) = x(j) + xdelta;
                     if nargout>2, hc(j) = cj; end
                     if (nargout>1), yi(j) = yij; end
                   end;
                 end;
               end;
            */

            return null; // for now
        }


        private void Qint()
        {
            /*
            function [p,y,a] = qint(ym1,y0,yp1)
            %QINT   Quadratic interpolation of 3 uniformly spaced samples
            %
            %               [p,y,a] = qint(ym1,y0,yp1)
            %
            %       returns extremum-location p, height y, and half-curvature a
            %       of a parabolic fit through three points.
            %       The parabola is given by y(x) = a*(x-p)^2+b,
            %       where y(-1)=ym1, y(0)=y0, y(1)=yp1.

               p = (yp1 - ym1)/(2*(2*y0 - yp1 - ym1));
               if nargout>1
                 y = y0 - 0.25*(ym1-yp1)*p;
               end;
               if nargout>2
                 a = 0.5*(ym1 - 2*y0 + yp1);
               end;

            */
        }
    }

    //https://www.dsprelated.com/freebooks/sasp/Matlab_listing_qint_m.html

    public class PeakDetectionData
    {
        public double[] PeakAmps { get; set; }
        public double[] PeakLocs { get; set; }
        public double[] PeakWidths { get; set; }
        public double[] ResId { get; set; }        
    }

    public class MaxrResult
    {
        public double[] xi { get; set; }
        public double[] yi { get; set; }
        public double[] hc { get; set; }        
    }
}
