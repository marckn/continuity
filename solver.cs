using System;
using System.Collections.Generic;
using System.IO;
namespace continuity
{
    public class solver
    {
        public int iskip=1;
        public int ntimes;
        public List<int> ndims;
        public double htime;
        public List<double> hspace = new List<double>{};
        public List<double> box;
        
        pde PDE;


        public solver(List<double> _box, int _ntimes, List<int> _ndims, double _htime, pde pdeq)
        {
            ntimes=_ntimes;
            ndims=_ndims;
            htime=_htime;
            box = _box;
            PDE = pdeq;
            for(int i=0;i<box.Count;i++)
                hspace.Add(box[i]/(ndims[i]-1));

        }

        public List<List<double>> RunIt(List<List<double>> initial_rho, string outputfile)
        {
            using (StreamWriter writer = new StreamWriter(outputfile,false))
                writer.WriteLine("#PDE discretized solution");

            List<List<double>> crho=initial_rho;
            for(int i=0;i<ntimes;i++)
            {
                if(i%iskip == 0)
                    utils.output.Printout(crho,box,htime*i,outputfile);
                
                crho = nextTime(crho);
            }
            utils.output.Printout(crho,box,htime*ntimes,outputfile);
            return crho;
        }
        
        private List<List<double>> nextTime(List<List<double>> crho)
        {
            var nrho = new List<List<double>>(crho);

            if(ndims.Count==1)
            {
                for(int i=1;i<crho.Count-1;i++)
                {
                    double cx = -box[0]/2 + i*hspace[0];
                    
                    double ugrad = PDE.u.gradF(new List<double>{cx})[0];
                    double rhod1 = (crho[i+1][0]-crho[i-1][0])/(2*hspace[0]);
                    double rhod2 = (crho[i+1][0]-2*crho[i][0]+crho[i-1][0])/(hspace[0]*hspace[0]);
                    
                    
                    if(!PDE.is_compressible){
                        nrho[i][0] += htime*PDE.beta*rhod1*ugrad;
                        nrho[i][0] += -htime*PDE.alpha*rhod2;
                    }else{
                        double ulap = PDE.u.lapF(new List<double>{cx})[0];
                        nrho[i][0] += htime*crho[i][0]*(PDE.beta*ulap - PDE.alpha*rhod2);
                        nrho[i][0] += htime*PDE.beta*rhod1*ugrad;
                        nrho[i][0] += -htime*PDE.alpha*rhod1*rhod1;
                    }
                }


            }
            else
            {
                for(int i=1;i<crho.Count-1;i++)
                    for(int j=1;j<crho[0].Count-1;j++)
                
                    {
                        double cx = -box[0]/2 + i*hspace[0];
                        double cy = -box[1]/2 + j*hspace[1];
                    
                        List<double> ugrad = PDE.u.gradF(new List<double>{cx,cy});
                    
                        double rhod1x = (crho[i+1][j]-crho[i-1][j])/(2*hspace[0]);
                        double rhod1y = (crho[i][j+1]-crho[i][j-1])/(2*hspace[1]);
                    
                        double rhod2x = (crho[i+1][j]-2*crho[i][j]+crho[i-1][j])/(hspace[0]*hspace[0]);
                        double rhod2y = (crho[i][j+1]-2*crho[i][j]+crho[i][j-1])/(hspace[1]*hspace[1]);

                        
                        if(!PDE.is_compressible)
                        {
                            nrho[i][j] += htime*PDE.beta*(rhod1x*ugrad[0]+rhod1y*ugrad[1]);
                            nrho[i][j] += -htime*PDE.alpha*(rhod2x+rhod2y);
                        }
                        else{

                            double ulap = PDE.u.lapF(new List<double>{cx,cy})[0] + 
                                          PDE.u.lapF(new List<double>{cx,cy})[1];
                        
                            nrho[i][j] += htime*crho[i][j]*(PDE.beta*ulap - PDE.alpha*(rhod2x+rhod2y));
                            nrho[i][j] += htime*PDE.beta*(rhod1x*ugrad[0]+rhod1y*ugrad[1]);
                            nrho[i][j] += -htime*PDE.alpha*(rhod1x*rhod1x+rhod1y*rhod1y);
                        }

                }
                 
            }

            var rhoret = new List<List<double>>(crho);

            for(int i=0;i<crho.Count;i++)
                for(int j=0;j<crho[0].Count;j++)
                    rhoret[i][j] += 0.5*htime*(crho[i][j]+nrho[i][j]);
                
            return rhoret;

        }

    }
}