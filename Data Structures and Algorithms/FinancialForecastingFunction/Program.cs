using System;

namespace FinancialForecasting
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter current value (e.g., 1000):");
            double currentValue = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Enter annual growth rate (as percentage, e.g., 10):");
            double growthRate = Convert.ToDouble(Console.ReadLine()) / 100;

            Console.WriteLine("Enter number of years to forecast:");
            int years = Convert.ToInt32(Console.ReadLine());

            double futureValue = PredictFutureValue(currentValue, growthRate, years);

            Console.WriteLine($"\nPredicted value after {years} years: {futureValue:F2}");
        }

        static double PredictFutureValue(double currentValue, double growthRate, int years)
        {
            if (years == 0)
                return currentValue;

            return PredictFutureValue(currentValue * (1 + growthRate), growthRate, years - 1);
        }
    }
}
