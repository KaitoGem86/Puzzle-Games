// This code is part of the SS-Scene library, released by Anh Pham (anhpt.csit@gmail.com).

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SS.IO
{
    public class Searcher
    {
        private static List<FileInfo> SearchFile(DirectoryInfo dir, string fileName)
        {
            List<FileInfo> foundItems = dir.GetFiles(fileName).ToList();
            DirectoryInfo[] dis = dir.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                foundItems.AddRange(SearchFile(di, fileName));
            }

            return foundItems;
        }

        public static string SearchFileInProject(string fileName)
        {
            DirectoryInfo di = new DirectoryInfo(Application.dataPath);
            List<FileInfo> fis = SearchFile(di, fileName);

            if (fis.Count >= 1)
            {
                return fis[0].FullName;
            }

            return null;
        }
    }
}
