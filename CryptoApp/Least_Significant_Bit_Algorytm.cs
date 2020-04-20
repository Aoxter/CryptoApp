using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;

namespace CryptoApp
{
	class Least_Significant_Bit_Algorytm
	{
		private byte[] chars;
		private string message = "";
		private int r_bits;
		private int g_bits;
		private int b_bits;
		private int pixel_bits;
		private int message_size;
		private string path_in;
		private string path_out;
		public Least_Significant_Bit_Algorytm(int option, int r_bits, int g_bits, int b_bits, string path_in, 
			string path_out, int message_size = 0, string message = "")
		{
			this.r_bits = r_bits;
			this.g_bits = g_bits;
			this.b_bits = b_bits;
			pixel_bits = 8;
			this.path_in = path_in;
			this.path_out = path_out;
			this.message_size = message_size;
			if (option == 1) encryption(message);
			else if (option == 2) decryption();
		}
		private void encryption(string m)
		{
			message = m;
			chars = new ASCIIEncoding().GetBytes(message);
			Console.WriteLine("Message length: " + chars.Length);
			Console.WriteLine();
			Bitmap image = new Bitmap(path_in);
			int w = 0;
			int h = 0;
			if (chars.Length > (image.Width * image.Height)) throw new Exception("Message size bigger than space for encryption");
			foreach (byte b in chars)
			{
				byte R = image.GetPixel(w, h).R;
				byte G = image.GetPixel(w, h).G;
				byte B = image.GetPixel(w, h).B;
				int tempR = R;
				int tempG = G;
				int tempB = B;
				int r_id = r_bits - 1;
				int g_id = g_bits - 1;
				int b_id = b_bits - 1;
				int ch_id = 7;
				bool bit;
				while (r_id >= 0)
				{
					bit = (b & (1 << ch_id)) != 0;
					if (bit) tempR |= 1 << r_id;
					else tempR &= ~(1 << r_id);
					ch_id--;
					r_id--;
				}
				while (g_id >= 0)
				{
					bit = (b & (1 << ch_id)) != 0;
					if (bit) tempG |= 1 << g_id;
					else tempG &= ~(1 << g_id);
					ch_id--;
					g_id--;
				}
				while (b_id >= 0)
				{
					bit = (b & (1 << ch_id)) != 0;
					if (bit) tempB |= 1 << b_id;
					else tempB &= ~(1 << b_id);
					ch_id--;
					b_id--;
				}
				R = (byte)tempR;
				G = (byte)tempG;
				B = (byte)tempB;
				image.SetPixel(w, h, Color.FromArgb(255, R, G, B));
				var test = image.GetPixel(w, h);
				if (w < (image.Width - 1)) w++;
				else
				{
					w = 0;
					h++;
				}
			}
			image.Save(path_out);
		}
		private void decryption()
		{
			Bitmap image = new Bitmap(path_in);
			chars = new byte[message_size];
			int w = 0;
			int h = 0;
			int pix_id = 0;
			if (message_size > (image.Width * image.Height)) throw new Exception("Message size bigger than space for encryption");
			while (pix_id < message_size)
			{
				int resoult = 0;
				byte R = image.GetPixel(w, h).R;
				byte G = image.GetPixel(w, h).G;
				byte B = image.GetPixel(w, h).B;
				int r_id = r_bits - 1;
				int g_id = g_bits - 1;
				int b_id = b_bits - 1;
				int ch_id = 7;
				bool bit;
				while (r_id >= 0)
				{
					bit = (R & (1 << r_id)) != 0;
					if (bit) resoult |= 1 << ch_id;
					else resoult &= ~(1 << ch_id);
					ch_id--;
					r_id--;
				}
				while (g_id >= 0)
				{
					bit = (G & (1 << g_id)) != 0;
					if (bit) resoult |= 1 << ch_id;
					else resoult &= ~(1 << ch_id);
					ch_id--;
					g_id--;
				}
				while (b_id >= 0)
				{
					bit = (B & (1 << b_id)) != 0;
					if (bit) resoult |= 1 << ch_id;
					else resoult &= ~(1 << ch_id);
					ch_id--;
					b_id--;
				}
				chars[pix_id] = (byte)resoult;
				if (w < (image.Width - 1)) w++;
				else
				{
					w = 0;
					h++;
				}
				pix_id++;
			}
			message = new ASCIIEncoding().GetString(chars);
			//STOP
			try
			{   // Open the text file using a stream reader.
				using (StreamWriter sw = new StreamWriter(path_out))
				{
					// Read the stream to a string, and write the string to the console.
					sw.WriteLine(message);
				}
			}
			catch (IOException e)
			{
				Console.WriteLine("The file could not be read:");
				Console.WriteLine(e.Message);
			}
		}
	}
}
