﻿// This code is part of the SS-Scene library, released by Anh Pham (anhpt.csit@gmail.com).

using UnityEngine;
using UnityEditor;

namespace SS.IO
{
	public class File
	{
        public static string Copy(string sourceFileName, string targetRelativePath, bool replaceExistFile = true)
		{
            string targetFullPath = System.IO.Path.Combine(Application.dataPath, targetRelativePath);

            string directoryPath = System.IO.Path.GetDirectoryName(targetFullPath);
            string templatePath = SS.IO.Searcher.SearchFileInProject(sourceFileName);

            if (templatePath == null)
            {
                return null;
            }

			if (!System.IO.Directory.Exists(directoryPath))
			{
				System.IO.Directory.CreateDirectory(directoryPath);
			}

			if (System.IO.File.Exists(targetFullPath))
			{
				if (replaceExistFile)
				{
					System.IO.File.Delete(targetFullPath);
				}
			}

			if (!System.IO.File.Exists(targetFullPath))
			{
				UnityEditor.FileUtil.CopyFileOrDirectory(templatePath, targetFullPath);
			}

            return targetFullPath;
		}

        public static void ReplaceFileContent(string fullPath, string oldString, string newString)
        {
            string fileContents = System.IO.File.ReadAllText(fullPath);
            fileContents = fileContents.Replace(oldString, newString);
            System.IO.File.WriteAllText(fullPath, fileContents);
        }
	}
}
