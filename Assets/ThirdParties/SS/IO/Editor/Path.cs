// This code is part of the SS-Scene library, released by Anh Pham (anhpt.csit@gmail.com).

using UnityEngine;
using System.Collections;

namespace SS.IO
{
	public class Path
	{
		public static string GetRelativePath(string absolutePath)
		{
			string projectPath = Application.dataPath.Substring(0, Application.dataPath.Length - "Assets".Length);
			return absolutePath.Replace(projectPath, string.Empty);
		}

        public static string GetAbsolutePath(string relativePath)
        {
            return System.IO.Path.Combine(Application.dataPath, relativePath);
        }

        public static string GetRelativePathWithAssets(string relativePath)
        {
            return System.IO.Path.Combine("Assets", relativePath);
        }
	}
}
