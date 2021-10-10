using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CRUD_BAL.Service
{
	public class RandomService
	{
		//private readonly Random _random = new Random();

		//// Generates a random number within a range.      
		//public int RandomNumber(int min, int max)
		//{
		//	return _random.Next(min, max);
		//}
		public static string RandomPassword(int cLength = 2, int lLength = 2, 
			int dLength = 4, int scLength = 2, 
			bool needCap = false, 
			bool needLow = false, 
			bool needSpecial = false)
		{
			Random ran = new Random();
			String random = "";

			String d = "0123456789";
			String c = "ABCDEFGHJKLMNPQRSTUVWXYZ";
			String l = "abcdefghijklmnopqrstuvwxyz";
			String sc = "!@#$%^&*~";

			
			for (int i = 0; i < dLength; i++)
			{
				int a = ran.Next(d.Length); //string.Lenght gets the size of string
				random = random + d.ElementAt(a);
			}
			if (needCap)
			{
				for (int i = 0; i < cLength; i++)
				{
					int a = ran.Next(c.Length); //string.Lenght gets the size of string
					random = random + c.ElementAt(a);
				}
			}
			if (needLow)
			{
				for (int i = 0; i < lLength; i++)
				{
					int a = ran.Next(l.Length); //string.Lenght gets the size of string
					random = random + l.ElementAt(a);
				}
			}
			if (needSpecial)
			{
				for (int i = 0; i < scLength; i++)
				{
					int a = ran.Next(sc.Length);
					random = random + sc.ElementAt(a);
				}
			}
			return random;
		}
	}
}
