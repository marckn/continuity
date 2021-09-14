using System;
using System.Collections.Generic;

namespace continuity.utils
{
    public static class Initials
    {
        public static List<List<double>> Gauss(List<double> box, List<int> npoints, 
                                            List<double> center, List<double> sigma, double amp)
        {
            List<List<double>> retval = new List<List<double>>{};

            List<double> dx = new List<double>{};
            for(int i=0;i<box.Count;i++)
                dx.Add(box[i]/(npoints[i]-1));
            
            if(box.Count == 1)
            {
                for(int i=0;i<npoints[0];i++)
                {
                    double cx = -box[0]/2 + i*dx[0];
                    double val = amp*Math.Exp(-Math.Pow(cx-center[0],2)/(2*sigma[0]*sigma[0]));
                    if(val<1E-6)
                        val=0;

                    retval.Add(new List<double>{val});
                }
            }
            else
            {
                for(int i=0;i<npoints[0];i++)
                {
                    var rln = new List<double>{};
                    for(int j=0;j<npoints[1];j++)
                    {
                        double cx = -box[0]/2 + i*dx[0];
                        double cy = -box[1]/2 + j*dx[1];
                        
                        double val = amp*Math.Exp(-Math.Pow(cx-center[0],2)/(2*sigma[0]*sigma[0]))*
                                        Math.Exp(-Math.Pow(cy-center[1],2)/(2*sigma[1]*sigma[1]));
                        if(val<1E-3)
                            val=0;
                        
                        rln.Add(val);
                    }
                    retval.Add(rln);
                }
            }

            return retval;
        }        
    }
}