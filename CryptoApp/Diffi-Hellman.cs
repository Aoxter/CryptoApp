using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoApp
{
	class Diffi_Hellman
	{
		private int n;
		private int g;
		private int private_key;
		private int session_key;
		private int value;
		private int second_side_value;
		public Diffi_Hellman(int n, int g)
		{
			Random generator = new Random();
			this.n = n;
			this.g = g;
			private_key = generator.Next(1000, 9999);
			value = power_modulo_fast(g, private_key, n);
			Console.WriteLine("Value for second side: " + value);
			Console.WriteLine("Give value from second side");
			second_side_value = int.Parse(Console.ReadLine());
			session_key = power_modulo_fast(second_side_value, private_key, n);
		}
		public int getSessionKey()
		{
			return session_key;
		}
		public int getPrivateKey()
		{
			return private_key;
		}
		private int power_modulo_fast(int base_value, int exponent, int modulo)
		{
			int i;
			long result = 1;
			long x = base_value % modulo;
			for (i = 1; i <= exponent; i <<= 1)
			{
				x %= modulo;
				if ((exponent & i) != 0)
				{
					result *= x;
					result %= modulo;
				}
				x *= x;
			}
			return (int)result;
		}
	}
}
