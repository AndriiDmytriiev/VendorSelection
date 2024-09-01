using System;

class Program
{
    static void Main()
    {
        // Step 1: Define Criteria
        string[] criteria = { "Cost", "Quality", "Delivery Time", "Service" };

        // Step 2: Pairwise Comparison Matrix (Saaty Scale)
        double[,] pairwiseMatrix = {
            { 1, 3, 5, 7 }, // Cost
            { 1.0/3.0, 1, 3, 5 }, // Quality
            { 1.0/5.0, 1.0/3.0, 1, 3 }, // Delivery Time
            { 1.0/7.0, 1.0/5.0, 1.0/3.0, 1 } // Service
        };

        // Step 3: Calculate Priority Vector (Normalized Eigenvector)
        double[] priorityVector = CalculatePriorityVector(pairwiseMatrix, criteria.Length);

        // Step 4: Display the priority vector
        Console.WriteLine("Priority Vector:");
        for (int i = 0; i < criteria.Length; i++)
        {
            Console.WriteLine($"{criteria[i]}: {priorityVector[i]:F4}");
        }

        // Step 5: Perform Vendor Evaluation based on weighted criteria
        // Example scores for three vendors on the criteria
        double[] vendor1Scores = { 0.7, 0.8, 0.6, 0.9 };
        double[] vendor2Scores = { 0.6, 0.9, 0.7, 0.8 };
        double[] vendor3Scores = { 0.8, 0.7, 0.9, 0.6 };

        double vendor1FinalScore = CalculateFinalScore(vendor1Scores, priorityVector);
        double vendor2FinalScore = CalculateFinalScore(vendor2Scores, priorityVector);
        double vendor3FinalScore = CalculateFinalScore(vendor3Scores, priorityVector);

        Console.WriteLine($"\nVendor 1 Final Score: {vendor1FinalScore:F4}");
        Console.WriteLine($"Vendor 2 Final Score: {vendor2FinalScore:F4}");
        Console.WriteLine($"Vendor 3 Final Score: {vendor3FinalScore:F4}");

        // Step 6: Determine the best vendor
        string bestVendor = DetermineBestVendor(vendor1FinalScore, vendor2FinalScore, vendor3FinalScore);
        Console.WriteLine($"\nThe best vendor is: {bestVendor}");
    }

    static double[] CalculatePriorityVector(double[,] matrix, int size)
    {
        double[] sumOfColumns = new double[size];
        double[] normalizedMatrixSum = new double[size];
        double[] priorityVector = new double[size];

        // Calculate the sum of each column
        for (int j = 0; j < size; j++)
        {
            for (int i = 0; i < size; i++)
            {
                sumOfColumns[j] += matrix[i, j];
            }
        }

        // Normalize the matrix
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                normalizedMatrixSum[i] += (matrix[i, j] / sumOfColumns[j]);
            }
            // Calculate the priority vector by averaging the rows of the normalized matrix
            priorityVector[i] = normalizedMatrixSum[i] / size;
        }

        return priorityVector;
    }

    static double CalculateFinalScore(double[] scores, double[] priorityVector)
    {
        double finalScore = 0.0;
        for (int i = 0; i < scores.Length; i++)
        {
            finalScore += scores[i] * priorityVector[i];
        }
        return finalScore;
    }

    static string DetermineBestVendor(double vendor1Score, double vendor2Score, double vendor3Score)
    {
        if (vendor1Score > vendor2Score && vendor1Score > vendor3Score)
        {
            return "Vendor 1";
        }
        else if (vendor2Score > vendor1Score && vendor2Score > vendor3Score)
        {
            return "Vendor 2";
        }
        else
        {
            return "Vendor 3";
        }
    }
}