// Author : Shinn
// Date : 20190918
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
        private List<string[]> rowData = new List<string[]>();

        public bool ClearCsvAtWrite { get; set; }

        public string ReadCsv(string path)
        {
            string text = "";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    // Read the stream to a string, and write the string to the console.
                    text = sr.ReadToEnd();
                    sr.Close();
                }
            }
            catch (System.NullReferenceException e)
            {
                Debug.Log("資料格式錯誤");
            }
            catch (System.Exception e)
            {
                Debug.Log("The file could not be read:");
            }
            return text;
        }

        public double[] ReadCsvDouble(string path)
        {
           List<double> result = new List<double>();
            string text = "";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    text = sr.ReadToEnd();
                    sr.Close();
                }
            }
            catch (System.NullReferenceException e)
            {
                Debug.Log("資料格式錯誤");
            }
            catch (System.Exception e)
            {
                Debug.Log("The file could not be read:");
            }

            string[] dataStr = text.Trim().Split('\n');
            for (int i=0; i< dataStr.Length; i++)
            {
                string[] rawQ = dataStr[0].Split(',');
                result.Add(double.Parse(rawQ[0]));
                result.Add(double.Parse(rawQ[1]));
                result.Add(double.Parse(rawQ[2]));
                result.Add(double.Parse(rawQ[3]));
            }
            return result.ToArray();
        }

        public void WriteToCsv(string[] csvContent, string saveName)
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
            string delimiter = "";

            StringBuilder sb = new StringBuilder();
            for (int index = 0; index < length; index++)
            {
                sb.AppendLine(string.Join(delimiter, output[index]));
            }

            string filePath = GetPath(saveName);

            StreamWriter outStream = File.CreateText(filePath);
            outStream.WriteLine(sb);
            outStream.Close();
        }

        public void Dispose()
        {
            rowData.Clear();
        }

        // Following method is used to retrive the relative path as device platform
        private string GetPath(string name)
        {
#if UNITY_EDITOR
            return Application.streamingAssetsPath + "/csv/" + name + ".csv";
#elif UNITY_ANDROID
        return Application.persistentDataPath+"Saved_data.csv";
#elif UNITY_IPHONE
        return Application.persistentDataPath+"/"+"Saved_data.csv";
#else
        return Application.dataPath +"/"+"Saved_data.csv";
#endif
        }
    }

}
