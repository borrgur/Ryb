using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Supermarket
{
    class Cashier
    {
        public ManualResetEvent IdleEvent = new ManualResetEvent(true);
        public int Id { get; private set; }

        private object syncObj = new object();
        public Cashier(int id)
        {
            Id = id;
        }

        public void serve(int customerId, int servingTime)
        {
            Console.WriteLine("Cashier {0} starts serving customer {1}", Id, customerId);
            Thread.Sleep(servingTime);
            Console.WriteLine("Cashier {0} finished serving customer {1}", Id, customerId);
        }

    }
}
