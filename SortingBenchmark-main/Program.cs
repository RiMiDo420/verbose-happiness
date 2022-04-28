using BenchmarkDotNet.Running;
using System.Collections.Generic;
using System.Linq;

namespace SortingBenchmark
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var summary = BenchmarkRunner.Run<Benchmarks>();
		}
		public static int[] QuickSort(int[] data2)
		{
			int pivot;
			List<int> smaller = new List<int>();
			List<int> bigger = new List<int>();

			if (data2.Length <= 1)
			{
				return data2;
			}
			if (data2.Length == 2)
			{
				if (data2[0] > data2[1])
				{
					int temp = data2[1];
					data2[1] = data2[0];
					data2[0] = temp;
					return data2;
				}
				return data2;
			}

			pivot = data2[0];
			for (int i = 1; i < data2.Length; i++)
			{
				if (data2[i] < pivot)
				{
					smaller.Add(data2[i]);
				}
				else if (data2[i] > pivot)
				{
					bigger.Add(data2[i]);
				}
			}
			smaller = QuickSort(smaller.ToArray()).ToList();
			smaller.Add(pivot);
			smaller.AddRange(QuickSort(bigger.ToArray()).ToList());
			return smaller.ToArray();
		}
	}

}