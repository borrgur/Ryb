using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Supermarket
{
    class QueueHandler
    {
        Queue<int> customers = new Queue<int>();
        List<Cashier> cashiers = new List<Cashier>();
        Object syncObj = new object();
        double servingTimeMin;
        double servingTimeMax;

        volatile bool isStopped = false;
        Random rnd = new Random();


        public QueueHandler(int numOfCashiers, double servingMin, double servingMax)
        {
            for (int i = 0; i < numOfCashiers; ++i)
            {
                Cashier cashier = new Cashier(i+1);
                cashiers.Add(cashier);
            }

            servingTimeMin = servingMin;
            servingTimeMax = servingMax;

            Thread t = new Thread(ThreadFunc);
            t.Start();
        }

        public void Stop()
        {
            isStopped = true;
        }

        void ThreadFunc()
        {
            WaitHandle[] allEvents = cashiers.Select(x => x.IdleEvent).ToArray();
            while (!isStopped || customers.Count > 0)
            {
                int idleIndex = WaitHandle.WaitAny(allEvents); 
                if (idleIndex != WaitHandle.WaitTimeout)
                {
                    lock (syncObj)
                    {
                        if (customers.Count > 0)
                        {
                            int customerId = customers.Dequeue();
                            Cashier cashier = cashiers[idleIndex];
                            lock (cashier)
                            {
                                cashier.IdleEvent.Reset();
                                cashierServesCustomer(cashier, customerId);
                            }
                        }
                    }
                }
            }

            WaitHandle.WaitAll(allEvents);
            Console.WriteLine("End of work");
        }

        public void newCustomer(int id)
        {
            lock (syncObj)
            {
                customers.Enqueue(id);
                Console.WriteLine("Customer {0} arrived. His number in queue is {1}", id, customers.Count);
            }
        }

        void cashierServesCustomer(Cashier cashier, int customerId)
        {
            int servingTime = (int)((servingTimeMin + rnd.NextDouble() * (servingTimeMax - servingTimeMin)) * 1000);
            ThreadPool.QueueUserWorkItem(obj =>
            {
                cashier.serve(customerId, servingTime);
                cashier.IdleEvent.Set();
            });
        }
    }
}
