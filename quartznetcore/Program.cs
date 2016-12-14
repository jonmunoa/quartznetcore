using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace quartznetcore
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                // Creamos el demonio
                IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();

                // Iniciamos el demonio
                scheduler.Start();

                // Definimos un trabajo y lo asociamos a la clase MyJobs
                IJobDetail appStartJob = JobBuilder.Create<MyJobs>()
                    .WithIdentity("arranqueApp", "app")
                    .Build();

                // Lanzamos el job cada vez que arranquemos la aplicación
                ITrigger appStartTrigger = TriggerBuilder.Create()
                    .WithIdentity("arranqueApp", "app")
                    .StartNow()
                    .Build();

                // Asociamos el job y el trigger al demonio
                scheduler.ScheduleJob(appStartJob, appStartTrigger);

                // Definimos un trabajo y lo asociamos a la clase MyJobs
                IJobDetail every24hJob = JobBuilder.Create<MyJobs>()
                    .WithIdentity("cada24h", "app")
                    .Build();

                // Lanzamos el job todos los días a las 14:00:00
                ITrigger every24hTrigger = TriggerBuilder.Create()
                    .WithIdentity("cada24h", "app")
                    .StartAt(DateBuilder.DateOf(14, 00, 0)) //14:00:00
                    .WithSimpleSchedule(s => s
                        .WithIntervalInHours(24)
                        .RepeatForever())
                    .Build();

                // Asociamos el job y el trigger al demonio
                scheduler.ScheduleJob(every24hJob, every24hTrigger);

                // Definimos un trabajo y lo asociamos a la clase MyJobs
                IJobDetail every10sJob = JobBuilder.Create<MyJobs>()
                    .WithIdentity("cada10s", "app")
                    .Build();

                // Lanzamos el job cada 10 segundos
                ITrigger every10sTrigger = TriggerBuilder.Create()
                    .WithIdentity("cada10s", "app")
                    .StartNow()
                    .WithSimpleSchedule(s => s
                        .WithIntervalInSeconds(10)
                        .RepeatForever())
                    .Build();

                // Asociamos el job y el trigger al demonio
                scheduler.ScheduleJob(every10sJob, every10sTrigger);
            }
            catch (SchedulerException se)
            {
                Console.WriteLine(se);
            }
        }
    }
}