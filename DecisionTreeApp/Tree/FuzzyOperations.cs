using System;

namespace DecisionTreeApp.Tree
{
    public interface IFuzzyOperations
    {
        double SNorm(double a, double b);

        double PNorm(double a, double b);
    }

    class FirstFuzzyFunction : IFuzzyOperations
    {
        public double PNorm(double a, double b)
        {
            return Math.Min(a, b);
        }

        public double SNorm(double a, double b)
        {
            return Math.Max(a, b);
        }
    }

    class SecondFuzzyFunction : IFuzzyOperations
    {
        public double PNorm(double a, double b)
        {
            return a * b;
        }

        public double SNorm(double a, double b)
        {
            return Math.Min(a + b, 1);
        }
    }
}
