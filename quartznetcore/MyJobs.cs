using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quartznetcore
{
    internal class MyJobs : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            // Según el job que se vaya a ejecutar
            switch (context.JobDetail.Key.ToString())
            {
                case "app.arranqueApp":
                    Console.WriteLine(string.Format("[{0}]: Aplicación iniciada!", DateTime.Now));
                    break;

                case "app.cada24h":
                    Console.WriteLine(string.Format("[{0}]: Hora de comer!", DateTime.Now));
                    break;

                case "app.cada10s":
                    Console.WriteLine(string.Format("[{0}]: La app esta UP!.", DateTime.Now));
                    break;
            }
        }
    }
}