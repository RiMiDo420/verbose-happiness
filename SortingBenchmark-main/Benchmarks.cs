using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet;
using BenchmarkDotNet.Attributes;

namespace SortingBenchmark
{
	public class Benchmarks
    {
		[Benchmark(Baseline = true)]
		public int[] BoubleSort()
		{
			int temp;
			for (int j = 0; j <= data.Length - 2; j++)
			{
				for (int i = 0; i <= data.Length - 2; i++)
				{
					if (data[i] > data[i + 1])
					{
						temp = data[i + 1];
						data[i + 1] = data[i];
						data[i] = temp;
					}
				}
			}
			return data;
		}

		[Benchmark]
		public int[] BoubleSort_YourOptimization()
		{
			// TODO
			int temp;
			bool isSorted = true;
			for (int j = 0; j <= data.Length - 2; j++)
			{
				for (int i = 0; i <= data.Length - (j + 2); i++)
				{
					if (data[i] > data[i + 1])
					{
						temp = data[i + 1];
						data[i + 1] = data[i];
						data[i] = temp;
						isSorted = false;
					}
				}
                if (isSorted)
                {
					return data;
                }
			}
			return data;
		}

		[Benchmark]
		public int[] YourImplementation()
		{
			int pivot;
			List<int> smaller = new List<int>();
			List<int> bigger = new List<int>();
			if(data.Length <= 1)
            {
				return data;
            }
			if(data.Length == 2)
            {
				if (data[0] > data[1])
                {
					int temp = data[1];
					data[1] = data[0];
					data[0] = temp;
					return data;
                }
				return data;
            }

			pivot = data[0];
			for (int i = 1; i < data.Length; i++)
			{
				if (data[i] < pivot)
				{
					smaller.Add(data[i]);
				}
				else if (data[i] >= pivot)
				{
					bigger.Add(data[i]);
				} 
			}
			smaller = QuickSort(smaller.ToArray()).ToList();
			smaller.Add(pivot);
			smaller.AddRange(QuickSort(bigger.ToArray()).ToList());
			return smaller.ToArray();

		}

		public int[] QuickSort(int[] data2)
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
				else if (data2[i] >= pivot)
				{
					bigger.Add(data2[i]);
				}
			}
			smaller = QuickSort(smaller.ToArray()).ToList();
			smaller.Add(pivot);
			smaller.AddRange(QuickSort(bigger.ToArray()).ToList());
			return smaller.ToArray();
		}

		[Params(3_000, 10_000)]
		public int N;

		private int[] originalData;
		private int[] data;

		[GlobalSetup]
		public void GlobalSetup()
		{
			originalData = new int[N];
			for (int i = 0; i < N; i++)
			{
				originalData[i] = Random.Shared.Next(N);
			}
		}

		[IterationSetup]
		public void IterationSetup()
		{
			data = (int[])originalData.Clone();
		}
	}
}
