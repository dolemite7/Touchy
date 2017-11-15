/*
 * Created by SharpDevelop.
 * User: BryanW
 * Date: 10/26/2017
 * Time: 12:33 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Diagnostics;

namespace Touchy
{
	class Program
	{

		static string helpstring = "Touchy - Usage: \n\n" +
			"touchy.exe [sourcedatechar][targetdatechar] <filemask>\n\n" +
			"Sourcedatechar: Datetime to touch files with\n" +
			"Targetdatechar: File Datetime property to touch\n" +
			"Filemask: Optional - default *.*, wildcards accepted\n\n" +
			"SourceDatechar codes: c - File Creation Time\n" +
			"                      m - File Last Modified Time\n" +
			"                      a - File Last Accessed Time\n" +
            "                      n - Now\n\n" +
			"TargetDatechar codes: c - File Creation Time\n" +
			"                      m - File Last Modified Time\n" +
			"                      a - File Last Accessed Time\n" +
			"                      t - Test (do not touch any file date)\n\n";

        public static void Main(string[] args)
		{
			if (args.Length < 1) {
				Console.WriteLine(helpstring);
				System.Environment.Exit(1);
			}

			if (args[0].Length < 2)
			{
				Console.WriteLine(helpstring);
				System.Environment.Exit(1);
			}

			string inargs = args[0].Replace("/", "");

			DateTime dTemp = DateTime.Now;

			DirectoryInfo di = new DirectoryInfo(@".");

			string filemask = (args.Length == 2) ? args[1] : "*.*";
			Console.WriteLine("Touchy - Processing " + filemask + "...");

			foreach (var fi in di.EnumerateFiles(filemask))
			{

				if (fi.Name == "Touchy.exe") { break; }
				switch (inargs.Substring(0, 1))
				{            //foreach (var fi in di.EnumerateFiles())
					case "c":
						dTemp = File.GetCreationTime(fi.FullName);
						break;
					case "m":
						dTemp = File.GetLastWriteTime(fi.FullName);
						break;
					case "a":
						dTemp = File.GetLastAccessTime(fi.FullName);
						break;
					case "n":
						dTemp = DateTime.Now;
						break;
					default:
						Console.WriteLine("Error 1: Invalid parameters.");
						PressAnyKey();
						break;
				}

				switch (inargs.Substring(1, 1))
				{
					case "c":
						File.SetCreationTime(fi.FullName, dTemp);
						break;
					case "m":
						File.SetLastWriteTime(fi.FullName, dTemp);
						break;
					case "a":
						File.SetLastAccessTime(fi.FullName, dTemp);
						break;
					case "t":
						break;
					default:
						Console.WriteLine("Error 2: Invalid parameters.");
						PressAnyKey();
						break;
				}
				Console.WriteLine("Touched: " + fi.Name + ((inargs.Substring(1, 1) == "t") ? " testing, skipped..." : "") );

			}
			Console.WriteLine("Touchy complete.");
			PressAnyKey();		
			
		}

		public static void PressAnyKey()
		{
#if DEBUG
			if (Debugger.IsAttached)
			{
				Console.Write("Press any key to continue...");
				Console.ReadKey(true);
			}
#endif
			System.Environment.Exit(1);
		}

	}
}