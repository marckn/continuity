using System;
using System.Collections.Generic;
using System.IO;

namespace continuity
{
    class Program
    {
        static void Main(string[] args)
        {


            double alpha_rho = -1.0;
            double beta_u = 0.0;
            //var u_func = new utils.Function();




/*            var u_func = new utils.TwoDCross();
            u_func.center=(0,0);
            u_func.length=3;
            u_func.min_width= 0.8;
            u_func.min_slope= 10;
            u_func.slope_cross = 0.1;
*/
            
            var u_func = new utils.OneDDWell();
            u_func.a=0.1;
            u_func.b=2.0;
            u_func.c=0;

           /* using (StreamWriter writer = new StreamWriter("prova.dat",false))
            {
                for(int i=0;i<100;i++)
                {
                    var r = new List<double>{-5+i*0.1};
                    var vp = u_func.gradF(r);
                    var vs = u_func.lapF(r);
                    writer.WriteLine($"{-5+i*0.1}\t{u_func.F(r)}\t{vp[0]}\t{vs[0]}");
                }    
            }*/

            /*using (StreamWriter writer = new StreamWriter("prova.dat",false))
            {
                for(int i=0;i<100;i++)
                {
                    for(int j=0;j<100;j++)
                    {
                        var r = new List<double>{-5+i*0.1,-5+j*0.1};
                        var vp = u_func.gradF(r);
                        var vs = u_func.lapF(r);
                        
                        writer.WriteLine($"{-5+i*0.1}\t{-5+j*0.1}\t{u_func.F(r)}\t{vp[0]}\t{vp[1]}\t{vs[0]}\t{vs[1]}");
                    }
                    writer.WriteLine("");
                }    
            }*/

            var sig_func = new utils.Function();


            pde PDE = new pde(alpha_rho,beta_u,u_func, sig_func);

            
            List<double> box = new List<double>{10.0};
            List<int>    npoints = new List<int>{200};

            int Ntimes = 100;
            double htime = 0.001;

            List<double> center = new List<double>{0.0};
            List<double> sigmas = new List<double>{0.2};
            double amp = 0.5;
            var init_rho = utils.Initials.Gauss(box,npoints,center,sigmas,amp);

            solver solve = new solver(box,Ntimes,npoints,htime,PDE);
            solve.iskip=1;
            solve.RunIt(init_rho,"test.dat");
        }
    }


} 