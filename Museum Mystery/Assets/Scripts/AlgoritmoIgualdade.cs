
using UnityEngine;
using System;
using System.Linq;

public class AlgoritmoIgualdade : MonoBehaviour
{
    public static double CalculateSimilarity(string source, string target)
{
    double result = 0;
    if ((source == null) || (target == null)) return result = 0;
    if ((source.Length == 0) || (target.Length == 0)) return result = 0;
    if (source == target) return result = 100;

    int stepsToSame = EditDistance(source, target);
    result = (1.0 - ((double)stepsToSame / (double)Math.Max(source.Length, target.Length)));
        Console.WriteLine("porcentagem de similaridade: "+result*100+"%");
        return result*100; //falta multiplicar por 100
}


    public static int EditDistance(string original, string modified)
    {
        int len_orig = original.Length;
        int len_diff = modified.Length;

        var matrix = new int[len_orig + 1, len_diff + 1];
        for (int i = 0; i <= len_orig; i++)
            matrix[i, 0] = i;
        for (int j = 0; j <= len_diff; j++)
            matrix[0, j] = j;

        for (int i = 1; i <= len_orig; i++)
        {
            for (int j = 1; j <= len_diff; j++)
            {
                int cost = modified[j - 1] == original[i - 1] ? 0 : 1;
                var vals = new int[] {
                matrix[i - 1, j] + 1,
                matrix[i, j - 1] + 1,
                matrix[i - 1, j - 1] + cost
            };
                matrix[i, j] = vals.Min();
                if (i > 1 && j > 1 && original[i - 1] == modified[j - 2] && original[i - 2] == modified[j - 1])
                    matrix[i, j] = Math.Min(matrix[i, j], matrix[i - 2, j - 2] + cost);
            }
        }
        Console.WriteLine("qtd diferenças da resposta: "+matrix[len_orig, len_diff]);
        return matrix[len_orig, len_diff];

    }
  
}