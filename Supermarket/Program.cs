using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Configuration;

namespace Supermarket
{
    class Program
    {
        static int curCustomer = 0;
        static QueueHandler qh;

        static void Main(string[] args)
        {
            var appSettings = ConfigurationManager.AppSettings;
            int cashiers;
            bool success = int.TryParse(appSettings.Get("numberOfCashiers"), out cashiers);
            if (!success || cashiers <= 0) throw new Exception("Wrong cashiers number");

            double customerRate;
            success = double.TryParse(appSettings.Get("customerRate"), out customerRate);
            if (!success || customerRate <= 0) throw new Exception("Wrong customer rate");

            double servingTimeMin;
            success = double.TryParse(appSettings.Get("servingTimeMin"), out servingTimeMin);
            if (!success || servingTimeMin <= 0) throw new Exception("Wrong min serving time");

            double servingTimeMax;
            success = double.TryParse(appSettings.Get("servingTimeMax"), out servingTimeMax);
            if (!success || servingTimeMax < servingTimeMin) throw new Exception("Wrong max serving time");

            qh = new QueueHandler(cashiers, servingTimeMin, servingTimeMax);

            Timer timer = new Timer(customerRate*1000);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();

            Console.ReadLine();
            timer.Stop();
            Console.WriteLine("Door is closed to entry");
            qh.Stop();
            Console.ReadLine();
        }

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            ++curCustomer;
            qh.newCustomer(curCustomer);
        }
    }
}
