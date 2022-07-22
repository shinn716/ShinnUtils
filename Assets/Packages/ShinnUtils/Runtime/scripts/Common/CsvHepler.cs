// Author: john.tc
// Date: 20220722

using UnityEngine;
using System.Collections.Generic;

namespace Shinn.Common
{
    public static class CsvHelper
    {
        public static List<string[]> ReadCSV(string _content, bool _showLog = false)
        {
            string[] row = _content.Split("\n"[0]);
            List<string[]> returnList = new List<string[]>();
            for(int i=0; i< row.Length; i++)
            {
                string[] column = row[i].Trim().Split(","[0]);
                List<string> strarr = new List<string>();
                for(int j=0; j< column.Length; j++)
                    strarr.Add(column[j]);
                returnList.Add(strarr.ToArray());
            }

            if (_showLog)
            {
                int col = 0;
                foreach (var i in returnList)
                {
                    col = i.Length;
                    foreach (var j in i)
                    {
                        Debug.Log(j);
                    }
                    Debug.Log("====");
                }
                Debug.Log($"CSV size: {row.Length}x{col}");
            }
            return returnList;
        }
    }
}
