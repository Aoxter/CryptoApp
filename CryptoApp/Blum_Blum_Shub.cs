using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Collections;
using System.Diagnostics;

namespace CryptoApp
{
	class Blum_Blum_Shub
	{
		private Random random = new Random();
		private ulong N;
		private ulong x;
		private Stopwatch stopwatch = new Stopwatch();
		private BitArray key;
		bool monobitResult;
		bool runsTest;
		bool longRunTest;
		bool pokerTest;

		public Blum_Blum_Shub()
		{
			//stopwatch.Start();
			Random random;
			ulong[] numbers = GenerateBlumNumber();
			N = numbers[0];
			x = numbers[1];
			key = GenerateKey(20000);
			displayStatictics();
		}
		private ulong[] GenerateBlumNumber()
		{
			ulong p = GeneratePrimeNumber(3, 4);
			ulong q = GeneratePrimeNumber(3, 4);
			ulong[] temp = new ulong[2];
			temp[0] = p * q;
			temp[1] = GenerateRelativePrime(p, q);
			return temp;
		}

		private ulong GeneratePrimeNumber()
		{
			bool guard = false;
			int prime = 0;
			while (!guard)
			{
				prime = random.Next(20000, 40000);
				guard = true;
				for (int i = 2; i < (int)Math.Sqrt(prime) + 1; i++)
				{
					if (prime % i == 0) guard = false;
				}
			}
			return (ulong)prime;
		}

		private ulong GeneratePrimeNumber(int a, int b)
		{
			bool guard = false;
			int prime = 0;
			while (!guard)
			{
				prime = random.Next(1001, 10000);
				guard = true;
				for (int i = 2; i < (int)Math.Sqrt(prime) + 1; i++)
				{
					if (prime % i == 0) guard = false;
				}
				if ((prime - a) % b != 0) guard = false;
			}
			return (ulong)prime;
		}

		private ulong GenerateRelativePrime(ulong p, ulong q)
		{
			ulong x = GeneratePrimeNumber();
			while ((x == p) || (x == q)) x = GeneratePrimeNumber();
			return x;
		}

		private BitArray GenerateKey(int amount)
		{
			stopwatch.Start();
			ulong temp = x;
			ulong generator;
			BitArray bits = new BitArray(amount);
			BitArray temp_bits;
			byte[] bytes;
			for (int i = 0; i < amount; i++)
			{
				generator = (temp * temp) % N;
				bytes = BitConverter.GetBytes(generator);
				temp_bits = new BitArray(bytes);
				bits.Set(i, temp_bits.Get(0));
				temp = generator;
			}
			stopwatch.Stop();
			return bits;
		}

		private bool MonobitTest(BitArray bits)
		{
			Console.WriteLine();
			Console.WriteLine("Monobit test");
			Console.WriteLine();
			bool pass = false;
			int temp = 0;
			foreach (bool b in bits)
			{
				if (b) temp++;
			}
			if ((temp > 9725) && (temp < 10275)) pass = true;
			Console.WriteLine("Amount of bits of value 1: " + temp);
			return pass;
		}

		private bool RunsTest(BitArray bits)
		{
			Console.WriteLine();
			Console.WriteLine("Runs test");
			Console.WriteLine();
			bool pass = false;
			bool first = true;
			bool last = true;
			int current_length = 0;
			int length0_1 = 0;
			int length0_2 = 0;
			int length0_3 = 0;
			int length0_4 = 0;
			int length0_5 = 0;
			int length0_6_more = 0;
			int length1_1 = 0;
			int length1_2 = 0;
			int length1_3 = 0;
			int length1_4 = 0;
			int length1_5 = 0;
			int length1_6_more = 0;
			foreach (bool b in bits)
			{
				if (first)
				{
					first = false;
					current_length++;
					last = b;
				}
				else
				{
					if (last == b) current_length++;
					else
					{
						switch (current_length)
						{
							case 1:
								{
									if (last) length1_1++;
									else length0_1++;
									break;
								}
							case 2:
								{
									if (last) length1_2++;
									else length0_2++;
									break;
								}
							case 3:
								{
									if (last) length1_3++;
									else length0_3++;
									break;
								}
							case 4:
								{
									if (last) length1_4++;
									else length0_4++;
									break;
								}
							case 5:
								{
									if (last) length1_5++;
									else length0_5++;
									break;
								}
							default:
								{
									if (last) length1_6_more++;
									else length0_6_more++;
									break;
								}
						}
						current_length = 1;
						last = b;
					}
				}
			}
			Console.WriteLine("Series of 0 of lengths 1: " + length0_1);
			Console.WriteLine("Series of 1 of lengths 1: " + length1_1);
			Console.WriteLine("Series of 0 of lengths 2: " + length0_2);
			Console.WriteLine("Series of 1 of lengths 2: " + length1_2);
			Console.WriteLine("Series of 0 of lengths 3: " + length0_3);
			Console.WriteLine("Series of 1 of lengths 3: " + length1_3);
			Console.WriteLine("Series of 0 of lengths 4: " + length0_4);
			Console.WriteLine("Series of 1 of lengths 4: " + length1_4);
			Console.WriteLine("Series of 0 of lengths 5: " + length0_5);
			Console.WriteLine("Series of 1 of lengths 5: " + length1_5);
			Console.WriteLine("Series of 0 of lengths 6 and more: " + length0_6_more);
			Console.WriteLine("Series of 1 of lengths 6 and more: " + length1_6_more);
			if ((length0_1 >= 2315) && (length0_1 <= 2685) && (length0_2 >= 1114) && (length0_2 <= 1386) && (length0_3 >= 527) && (length0_3 <= 723) && (length0_4 >= 240) && (length0_4 <= 384) && (length0_5 >= 103) && (length0_5 <= 209) && (length0_6_more >= 103) && (length0_6_more <= 209) && (length1_1 >= 2315) && (length1_1 <= 2685) && (length1_2 >= 1114) && (length1_2 <= 1386) && (length1_3 >= 527) && (length1_3 <= 723) && (length1_4 >= 240) && (length1_4 <= 384) && (length1_5 >= 103) && (length1_5 <= 209) && (length1_6_more >= 103) && (length1_6_more <= 209)) pass = true;
			return pass;
		}

		private bool LongRunTest(BitArray bits)
		{
			Console.WriteLine();
			Console.WriteLine("Long run test");
			Console.WriteLine();
			bool pass = true;
			bool first = true;
			bool last = true;
			int current_length = 0;
			foreach (bool b in bits)
			{
				if (first)
				{
					first = false;
					current_length++;
					last = b;
				}
				else
				{
					if (last == b) current_length++;
					else
					{
						if (current_length >= 26)
						{
							pass = false;
							break;
						}
						current_length = 1;
						last = b;
					}
				}
			}
			return pass;
		}

		static bool PokerTest(BitArray bits)
		{
			Console.WriteLine();
			Console.WriteLine("Poker test");
			Console.WriteLine();
			bool pass = false;
			int value = 0;
			int index = 0;
			bool[] group = new bool[4];
			int amount_0 = 0;
			int amount_1 = 0;
			int amount_2 = 0;
			int amount_3 = 0;
			int amount_4 = 0;
			int amount_5 = 0;
			int amount_6 = 0;
			int amount_7 = 0;
			int amount_8 = 0;
			int amount_9 = 0;
			int amount_10 = 0;
			int amount_11 = 0;
			int amount_12 = 0;
			int amount_13 = 0;
			int amount_14 = 0;
			int amount_15 = 0;
			foreach (bool b in bits)
			{
				if (index == 4)
				{
					index = 0;
					if (group[0]) value += 8;
					if (group[1]) value += 4;
					if (group[2]) value += 2;
					if (group[3]) value += 1;
					switch (value)
					{
						case 0: amount_0++; break;
						case 1: amount_1++; break;
						case 2: amount_2++; break;
						case 3: amount_3++; break;
						case 4: amount_4++; break;
						case 5: amount_5++; break;
						case 6: amount_6++; break;
						case 7: amount_7++; break;
						case 8: amount_8++; break;
						case 9: amount_9++; break;
						case 10: amount_10++; break;
						case 11: amount_11++; break;
						case 12: amount_12++; break;
						case 13: amount_13++; break;
						case 14: amount_14++; break;
						case 15: amount_15++; break;
					}
					value = 0;
				}
				group[index] = b;
				index++;
			}
			double x = (double)16 / 5000 * ((amount_0 * amount_0) + (amount_1 * amount_1) + (amount_2 * amount_2) + (amount_3 * amount_3) + (amount_4 * amount_4) + (amount_5 * amount_5) + (amount_6 * amount_6) + (amount_7 * amount_7) + (amount_8 * amount_8) + (amount_9 * amount_9) + (amount_10 * amount_10) + (amount_11 * amount_11) + (amount_12 * amount_12) + (amount_13 * amount_13) + (amount_14 * amount_14) + (amount_15 * amount_15)) - 5000;
			if ((x > 2.16) && (x < 46.17)) pass = true;
			Console.WriteLine("X = " + x);
			return pass;
		}

		private void displayStatictics()
		{
			Console.WriteLine();
			Console.WriteLine("RESULTS:");
			Console.WriteLine();
			Console.WriteLine("--------------------");
			Console.WriteLine();
			Console.WriteLine("Generate of key took: " + stopwatch.ElapsedMilliseconds + "ms");
			Console.WriteLine();
			Console.WriteLine("Prime number N: " + N.ToString() + " relatively prime number x to number N: " + x.ToString());
			Console.WriteLine();
			Console.WriteLine("--------------------");
			Console.WriteLine();
			Console.WriteLine("Press any key to run tests");
			Console.ReadKey();
			Console.WriteLine();
			Console.WriteLine("--------------------");
			monobitResult = MonobitTest(key);
			Console.WriteLine();
			if (monobitResult) Console.WriteLine("Test passed");
			else Console.WriteLine("Test failed");
			Console.WriteLine();
			Console.WriteLine("--------------------");
			runsTest = RunsTest(key);
			Console.WriteLine();
			if (runsTest) Console.WriteLine("Test passed");
			else Console.WriteLine("Test failed");
			Console.WriteLine();
			Console.WriteLine("--------------------");
			longRunTest = LongRunTest(key);
			if (longRunTest) Console.WriteLine("Test passed");
			else Console.WriteLine("Test failed");
			Console.WriteLine();
			Console.WriteLine("--------------------");
			pokerTest = PokerTest(key);
			Console.WriteLine();
			if (pokerTest) Console.WriteLine("Test passed");
			else Console.WriteLine("Test failed");
			Console.WriteLine();
			Console.WriteLine("--------------------");
			Console.WriteLine();
			Console.WriteLine("Press any key to display generated key");
			Console.ReadKey();
			Console.WriteLine();
			Console.WriteLine("--------------------");
			Console.WriteLine();
			Console.WriteLine("Generated key:");
			Console.WriteLine();
			foreach (bool bit in key)
			{
				if (bit) Console.Write(1);
				else Console.Write(0);
			}
			Console.WriteLine();
			Console.WriteLine();
		}
	}
}

