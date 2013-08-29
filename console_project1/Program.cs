using System;
using System.IO;
using System.Text;
using System.Collections;

namespace console_project1
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.Write ("What is the file path to search? ");
			string _path = Console.ReadLine ();
			Console.Write ("What files are you looking for? ");
			string pattern = Console.ReadLine ();
			Console.Write ("Where would you like to save the resultFile? ");
			string resultFile = Console.ReadLine ();
			Console.WriteLine ();
			FileFinder (_path, pattern, resultFile);
			Console.WriteLine ("!--Finished--!");
			Console.ReadLine ();

		}

		public static void FileFinder (string folderPath, string searchPattern, string resultFile) {
			ArrayList fileList = new ArrayList();
			try {
				if ((File.GetAttributes(folderPath) & FileAttributes.ReparsePoint) != FileAttributes.ReparsePoint) {
					foreach (string d in Directory.GetDirectories(folderPath)) {
						foreach (string f in Directory.GetFiles(d, searchPattern)){
							FileInfo fileInfo = new FileInfo(f);
							string info =  f + ", " + fileInfo.Length/1048576 + " MB";
							fileList.Add (info);

						}//recursive find files
						FileFinder (d, searchPattern, resultFile);
					}//recursive search folders
				}//Determine if reparse point
			} catch (Exception ex) {
				if (ex is PathTooLongException || ex is UnauthorizedAccessException) {
					//ignore folders that user does not have permission to access.
				}
			}
			string[] results = (string[])fileList.ToArray (typeof(string));
			foreach (string line in results) {
				if (!File.Exists (resultFile)) {
					File.WriteAllText (resultFile, line + Environment.NewLine);
				} else {
					File.AppendAllText (resultFile, line + Environment.NewLine);
				}
			}
//			if (!File.Exists (resultFile)) {
//				string[] results = (string[])fileList.ToArray (typeof(string));
//				File.WriteAllLines (resultFile, results);
//			}

		}//FileFinder()
	}//Main()
}//console_project1
