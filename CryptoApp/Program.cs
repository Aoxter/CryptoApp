using System;
using System.IO;

namespace CryptoApp
{
	class Program
	{
		static void Main(string[] args)
		{
			int option;
			do
			{
				Console.WriteLine("Choose what you want to do");
				Console.WriteLine("0 - exit");
				Console.WriteLine("1 - Diffi-Hellmans session's keys generator");
				Console.WriteLine("2 - Steganography - Least Significant Bit Algorithm");
				option = int.Parse(Console.ReadLine());
				switch (option)
				{
					case 1:
						{
							Console.WriteLine("Remember that you need to run two instances of Diffi-Hellmans");
							Console.WriteLine("Press q to cancel or any other key to continue");
							if (Console.ReadKey().Key == ConsoleKey.Q) break;
							else { }
							Console.WriteLine("\nGive prime number N");
							int n = int.Parse(Console.ReadLine());
							Console.WriteLine("Give primitive root modulo N in range (1,N)");
							int g = int.Parse(Console.ReadLine());
							Diffi_Hellman dh = new Diffi_Hellman(n, g);
							int sk = dh.getSessionKey();
							int pk = dh.getPrivateKey();
							Console.WriteLine("Private key: " + pk);
							Console.WriteLine("Session key: " + sk);
							break;
						}
					case 2:
						{
							string message = "";
							String path_in = "";
							String path_out = "";
							int pixel_bits;
							int r_bits;
							int g_bits;
							int b_bits;
							int message_size = 0;
							Console.WriteLine("Choose option 1 - message encryption 2 - message decryption");
							int option_2 = int.Parse(Console.ReadLine());
							if (option_2 == 1)
							{
								Console.WriteLine("Choose message source 1 - console 2 - file");
								int option_3 = int.Parse(Console.ReadLine());
								if (option_3 == 1)
								{
									Console.WriteLine("Give message to encryption");
									message = Console.ReadLine();
								}
								else if (option_3 == 2)
								{
									Console.WriteLine("Give path to file with message (txt format)");
									String path_to_file = Console.ReadLine();
									try
									{   // Open the text file using a stream reader.
										using (StreamReader sr = new StreamReader(path_to_file))
										{
											message = sr.ReadToEnd();
										}
									}
									catch (IOException e)
									{
										Console.WriteLine("The file could not be read:");
										Console.WriteLine(e.Message);
									}
								}
								Console.WriteLine("Give path to file with image (bmp format)");
								path_in = Console.ReadLine();
								Console.WriteLine("Give path to output file (bmp format)");
								path_out = Console.ReadLine();
								message_size = message.Length;
							}
							else if (option == 2)
							{
								Console.WriteLine("Give size of message (amount of characters)");
								message_size = int.Parse(Console.ReadLine());
								Console.WriteLine("Give path to source file (bmp format)");
								path_in = Console.ReadLine();
								Console.WriteLine("Give path to save message (txt format)");
								path_out = Console.ReadLine();
							}
							do
							{
								Console.WriteLine("Enter in order how many bits of red, green and blue channels is using (sum must be equal 8)");
								r_bits = int.Parse(Console.ReadLine());
								g_bits = int.Parse(Console.ReadLine());
								b_bits = int.Parse(Console.ReadLine());
								pixel_bits = r_bits + g_bits + b_bits;
								if (pixel_bits < 8) Console.WriteLine("Not enaugh bits: " + pixel_bits);
								else if (pixel_bits > 8) Console.WriteLine("Too many bits: " + pixel_bits);
							} while (pixel_bits != 8);
							if(option_2 == 1)
							{
								Least_Significant_Bit_Algorytm lsba = new Least_Significant_Bit_Algorytm(option_2,
									r_bits, g_bits, b_bits, path_in, path_out, message_size, message);
							}
							else if (option_2 == 2)
							{
								Least_Significant_Bit_Algorytm lsba = new Least_Significant_Bit_Algorytm(option_2,
									r_bits, g_bits, b_bits, path_in, path_out, message_size);
							}
							break;
						}
				}
			} while (option != 0);
			
		}
	}
}
