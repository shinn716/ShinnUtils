// Author : Shinn
// Date : 20190913
// Reference : https://sushanta1991.blogspot.com/2015/02/how-to-write-data-to-csv-file-in-unity.html
// 

using UnityEngine;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Shinn.Common
{

    public class CsvTools
    {
        public bool ClearCsvAtWrite { get; set; }

        private string[] csvContent;
        private List<string[]> rowData = new List<string[]>();
        private string saveName;
        
        public CsvTools(string[] content, string name)
        {
            csvContent = content;
            saveName = name;
        }


        public void WriteToCsv()
        {
            if (ClearCsvAtWrite)
                rowData.Clear();

            rowData.Add(csvContent);

            string[][] output = new string[rowData.Count][];

            for (int i = 0; i < output.Length; i++)
            {
                output[i] = rowData[i];
            }

            int length = output.GetLength(0);
            string delimiter = ",";

            StringBuilder sb = new StringBuilder();

            for (int index = 0; index < length; index++)
                sb.AppendLine(string.Join(delimiter, output[index]));


            string filePath = GetPath();

            StreamWriter outStream = File.CreateText(filePath);
            outStream.WriteLine(sb);
            outStream.Close();
        }

        public void Dispose()
        {
            rowData.Clear();
        }

        // Following method is used to retrive the relative path as device platform
        private string GetPath()
        {
#if UNITY_EDITOR
            return Application.streamingAssetsPath + "/csv/" + "saveName.csv";
// #elif UNITY_ANDROID
//         return Application.persistentDataPath+"Saved_data.csv";
// #elif UNITY_IPHONE
//         return Application.persistentDataPath+"/"+"Saved_data.csv";
// #else
//         return Application.dataPath +"/"+"Saved_data.csv";
#endif
        }
    }

}
