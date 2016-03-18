using System;
using System.Threading;

namespace Traffic_Light_Challenge
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create the thread object. This does not start the thread.
            GameEngine gameEngine = new GameEngine();
            Thread gameEngineThread = new Thread(gameEngine.Start);
            View view = new View(gameEngine);
            Thread viewThread = new Thread(view.Start);

            // Start the worker thread.
            gameEngineThread.Start();
            Console.WriteLine("main thread: Starting gameEngine thread...");
            viewThread.Start();
            Console.WriteLine("main thread: Starting view thread...");

            // Loop until worker thread activates. 
            while ((!gameEngineThread.IsAlive) || (!viewThread.IsAlive)) ;

            Console.ReadLine();
            // Request that the worker thread stop itself:
            gameEngine.RequestStop();
            view.RequestStop();

            // Use the Join method to block the current thread  
            // until the object's thread terminates.
            viewThread.Join();
            Console.WriteLine("main thread: view thread has terminated.");
            gameEngineThread.Join();
            Console.WriteLine("main thread: gameEngine thread has terminated.");
            Console.WriteLine("main thread terminates.");
        }
    }
}
