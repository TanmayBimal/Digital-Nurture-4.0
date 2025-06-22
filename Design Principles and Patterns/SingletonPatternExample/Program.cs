using System;

namespace SingletonPatternExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Logger logger1 = Logger.GetInstance();
            logger1.Log("Logging first message...");

            Logger logger2 = Logger.GetInstance();
            logger2.Log("Logging second message...");

            if (object.ReferenceEquals(logger1, logger2))
            {
                Console.WriteLine("✅ Both logger instances are the same (Singleton works!)");
            }
            else
            {
                Console.WriteLine("❌ Logger instances are different (Singleton failed!)");
            }
        }
    }
}
