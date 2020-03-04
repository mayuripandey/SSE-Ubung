// Course Material "Protokolle Verteilte Systeme"
// (c) 2008 by Professur Verteilte und Selbstorganisierende Rechnersysteme, TUC

using System;
using System.Threading;

namespace Vsr.Teaching.Pvs.Sample
{
    /// <summary>
    /// A sample program that demonstrates the use of threads.
    /// </summary>
    class ThreadExample
    {
        static void Method()        
        {
            for (int i = 0; i < 10; i++)
            {
                Console.Write("b");
                // wait for 0.1s
                Thread.Sleep(100);
            }
        }

        /// <summary>
        /// The main program routine.
        /// </summary>        
        static void Main(string[] args)
        {
            // start a parallel thread
            Thread t = new Thread(Method);
            t.Start();

            for (int i = 0; i < 10; i++)
            {
                Console.Write("a");
                // wait for 0.1s
                Thread.Sleep(100);
            }

            // wait for thread to end
            t.Join();
            Console.ReadLine();
        }        
      
    }
}
