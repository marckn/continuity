using System;
using System.Collections.Generic;

namespace continuity.utils
{
    public class Function
    {
        public virtual double F(List<double> x){ return 0;}
        public virtual List<double> gradF(List<double> x){ return new List<double>{0,0};}
        public virtual List<double> lapF(List<double> x){ return new List<double>{0,0};}
    }


    public class OneDDWell : Function
    {
        public double a,b,c;
        
        public override double F(List<double> x)
        {
            return a*Math.Pow(x[0],4) - b*Math.Pow(x[0],2) + c*x[0];
        }

        public override List<double> gradF(List<double> x)
        {
            return new List<double>{4*a*Math.Pow(x[0],3)-2*b*x[0]+c};
        }
        
        public override List<double> lapF(List<double> x)
        {
            return new List<double>{12*a*Math.Pow(x[0],2)-2*b};
        }
        
    }


    public class TwoDCross : Function
    {
        
        public (double,double) center;
        public double length;
        public double min_width;
        public double slope_cross;
        public double min_slope;

        public override double F(List<double> x)
        {
            double retval = slope_cross*Math.Pow(x[0]*x[1],2);
            List<(double,double)> lobes = new List<(double,double)>{(0,length),(0,-length),(length,0),(-length,0)};

            foreach (var center in lobes)
                retval += -min_slope*Math.Exp(-(Math.Pow(center.Item1-x[0],2)+Math.Pow(center.Item2-x[1],2))/(2*min_width*min_width));
            

            return retval;
        }

        public override List<double> gradF(List<double> x)
        {
            List<double> retval = new List<double>{0,0};

            retval[0] = 2*slope_cross*x[0]*Math.Pow(x[1],2);
            retval[1] = 2*slope_cross*x[1]*Math.Pow(x[0],2);

            List<(double,double)> lobes = new List<(double,double)>{(0,length),(0,-length),(length,0),(-length,0)};

            foreach (var center in lobes)
            {
                double fc = -min_slope*Math.Exp(-(Math.Pow(center.Item1-x[0],2)+Math.Pow(center.Item2-x[1],2))/(2*min_width*min_width));
                retval[0] += fc*(center.Item1 - x[0])/(min_width*min_width);
                retval[1] += fc*(center.Item2 - x[1])/(min_width*min_width);
            }

            return retval;            
        }

        public override List<double> lapF(List<double> x)
        {
            List<double> retval = new List<double>{0,0};

            retval[0] = 2*slope_cross*x[1]*x[1];
            retval[1] = 2*slope_cross*x[0]*x[0];

            List<(double,double)> lobes = new List<(double,double)>{(0,length),(0,-length),(length,0),(-length,0)};

            foreach (var center in lobes)
            {
                double fc = -min_slope*Math.Exp(-(Math.Pow(center.Item1-x[0],2)+Math.Pow(center.Item2-x[1],2))/(2*min_width*min_width));
                retval[0] += fc*Math.Pow(center.Item1 - x[0],2)/Math.Pow(min_width,4) -fc/(min_width*min_width);
                retval[1] += fc*Math.Pow(center.Item2 - x[1],2)/Math.Pow(min_width,4) - fc/(min_width*min_width);
            }

            return retval;            
            
        }
    }
}