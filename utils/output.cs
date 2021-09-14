using System;
using System.Collections.Generic;
using System.IO;

namespace continuity.utils
{
    public static class output
    {
        public static void Printout(List<List<double>> rho, List<double> box, double ctime, string fileName)
        {
            double dx = box[0]/(rho.Count-1);

            using (StreamWriter writer = new StreamWriter(fileName,true))
            {
            
                if(rho[0].Count==1)
                    for(int i=0;i<rho.Count;i++)
                        writer.WriteLine($"{ctime}\t{i*dx-box[0]/2}\t{rho[i][0]}");
                else
                {
                    double dy = box[1]/(rho[0].Count-1);
                    for(int i=0;i<rho.Count;i++){
                        for(int j=0;j<rho[0].Count;j++)
                            writer.WriteLine($"{ctime}\t{i*dx-box[0]/2}\t{j*dy-box[1]/2}\t{rho[i][j]}");

                        writer.WriteLine("");
                    }
                }

                writer.WriteLine("");
            }
        }
    }
}