using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace wcfserver
{
    class Program
    {

        static void Main(string[] args)
        {   
             try
            {
                ServiceHost svc = new ServiceHost(typeof(Service1));


                svc.Open();
                Console.WriteLine("START WCF");
                Console.ReadLine();

                svc.Close();
                Console.WriteLine("END WCF");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
