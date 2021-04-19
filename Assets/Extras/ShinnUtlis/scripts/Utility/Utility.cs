using UnityEngine;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Shinn
{
    #region Write And Read Data
    public class TxtTools
    {
        /// <summary>
        /// 寫入文字 (addDatatoFiles 重負寫入)
        /// </summary>
        /// <param name="data"></param>
        /// <param name="dataPath"></param>
        /// <param name="addDatatoFiles"></param>
        public static void WriteToTxt(string data, string dataPath, bool addDatatoFiles = true)
        {
            using (StreamWriter outputFile = new StreamWriter(dataPath, addDatatoFiles))
                outputFile.WriteLine(data);
        }

        /// <summary>
        /// 讀取文字檔, 各別行數
        /// </summary>
        /// <param name="dataPath"></param>
        /// <returns></returns>
        public static string[] ReadLines(string dataPath)
        {
            string[] lines = File.ReadAllLines(dataPath);
            return lines;
        }

        /// <summary>
        /// 讀取文字檔
        /// </summary>
        /// <param name="dataPath"></param>
        /// <returns></returns>
        public static string ReadText(string dataPath)
        {
            string allstr = File.ReadAllText(dataPath);
            return allstr;
        }
    }
    #endregion
    
    public class ConvertTools
    {
        public static string Unicode2String(string source)
        {
            return new Regex(@"\\u([0-9A-F]{4})", RegexOptions.IgnoreCase | RegexOptions.Compiled).Replace(
                         source, x => string.Empty + Convert.ToChar(Convert.ToUInt16(x.Result("$1"), 16)));
        }

        public static string String2Unicode(string source)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(source);
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i += 2)
                stringBuilder.AppendFormat("\\u{0}{1}", bytes[i + 1].ToString("x").PadLeft(2, '0'), bytes[i].ToString("x").PadLeft(2, '0'));

            return stringBuilder.ToString();
        }

        /// <summary>
        ///  HexString to Bytes
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static byte[] StringToBytes(string s)
        {
            //string s = "4321000000000000000000000000000000000000";
            //byte[] bytes = Encoding.ASCII.GetBytes(s);

            byte[] s_bytes = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i = i + 2)
            {
                //每2位16進位數字轉換為一個10進位整數
                s_bytes[i / 2] = Convert.ToByte(s.Substring(i, 2), 16);
            }
            return s_bytes;
        }

        /// <summary>
        /// Show Bytes
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string BytesToString(byte[] bytes)
        {
            string result = "";

            foreach (var b in bytes)
                result += b.ToString("X2");

            return result;
        }

        /// <summary>
        /// Int 轉換成 String
        /// </summary>
        /// <param name="decValue"></param>
        /// <returns></returns>
        public static string ConvertInt2Hex(int decValue)
        {
            return string.Format("{0:x}", decValue);
        }

        /// <summary>
        /// String 轉換成 int
        /// </summary>
        /// <param name="hexValue"></param>
        /// <returns></returns>
        public static int ConvertHex2Int(string hexValue)
        {
            return (int)Convert.ToInt64(hexValue, 16);
        }
    }

    public class Utility
    {
        /// <summary>
        /// Texture to Texture2D
        /// </summary>
        /// <param name="texture"></param>
        /// <returns></returns>
        public static Texture2D Texture2Tex2D(Texture texture)
        {
            return Texture2D.CreateExternalTexture(
            texture.width,
            texture.height,
            TextureFormat.RGB24,
            false, false,
            texture.GetNativeTexturePtr());
        }

        /// <summary>
        /// Texture2D to sprite
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="PixelsPerUnit"></param>
        /// <param name="spriteType"></param>
        /// <returns></returns>
        public static Sprite Tex2D2Sprite(Texture2D texture, float PixelsPerUnit = 100.0f, SpriteMeshType spriteType = SpriteMeshType.Tight)
        {
            Sprite NewSprite;
            NewSprite = Sprite.Create(texture, 
                new Rect(0, 0, texture.width, texture.height), 
                new Vector2(0, 0), PixelsPerUnit, 0, spriteType);
            return NewSprite;
        }

        /// <summary>
        /// Load texture2D from file
        /// </summary>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        public static Texture2D LoadTex2D(string FilePath)
        {
            Texture2D Tex2D;
            byte[] FileData;

            if (File.Exists(FilePath))
            {
                FileData = File.ReadAllBytes(FilePath);
                Tex2D = new Texture2D(2, 2);             // Create new "empty" texture
                if (Tex2D.LoadImage(FileData))           // Load the imagedata into the texture (size is set automatically)
                    return Tex2D;                        // If data = readable -> return texture
            }
            return null;                                 // Return null if load failed
        }

        /// <summary>
        /// Load sprite from file
        /// </summary>
        /// <param name="FilePath"></param>
        /// <param name="PixelsPerUnit"></param>
        /// <param name="spriteType"></param>
        /// <returns></returns>
        public static Sprite LoadSprite(string FilePath, float PixelsPerUnit = 100.0f, SpriteMeshType spriteType = SpriteMeshType.Tight)
        {
            Sprite NewSprite;
            Texture2D SpriteTexture = LoadTex2D(FilePath);
            NewSprite = Sprite.Create(SpriteTexture, 
                new Rect(0, 0, SpriteTexture.width, SpriteTexture.height), 
                new Vector2(0, 0), PixelsPerUnit, 0, spriteType);
            return NewSprite;
        }


        /// <summary>
        /// Bitmask to int array
        /// </summary>
        /// <param name="mask"></param>
        /// <returns></returns>
        public static int[] Bitmask2Array(LayerMask layoutMask)
        {
            var bitarray = new BitArray(new[] { layoutMask.value });
            var att = bitarray.Cast<bool>().ToArray();
            List<int> returnArray = new List<int>();

            for (int i = 0; i < att.Length; i++)
                if (att[i])
                    returnArray.Add(i);

            return returnArray.ToArray();
        }
        
        /// <summary>
        /// Mapping 
        /// </summary>
        public static float Map(float v, float a, float b, float x, float y)
        {
            return (v == a) ? x : (v - a) * (y - x) / (b - a) + x;
        }

        /// <summary>
        /// 不重覆亂數 (int) 0 ~ length
        /// </summary>
        public static int[] NonrepetitiveRandom(int total)
        {
            int[] sequence = new int[total];
            int[] output = new int[total];

            for (int i = 0; i < total; i++)
                sequence[i] = i;


            int end = total - 1;
            for (int i = 0; i < total; i++)
            {
                int num = UnityEngine.Random.Range(0, end + 1);
                output[i] = sequence[num];
                sequence[num] = sequence[end];
                end--;
            }
            return output;
        }

        /// <summary>
        /// String to float
        /// </summary>
        public static float StringToFloat(string stringValue)
        {
            float.TryParse(stringValue, out float result);
            return result;
        }

        /// <summary>
        /// Get Ip address
        /// </summary>
        public static string GetLocalIPAddress()
        {
            var host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
            foreach (var ip in host.AddressList)
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    return ip.ToString();

            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        /// <summary>
        /// CopyComponent function, from https://answers.unity.com/questions/458207/copy-a-component-at-runtime.html
        /// </summary>
        public static Component CopyComponent(Component original, GameObject destination)
        {
            Type type = original.GetType();
            Component copy = destination.AddComponent(type);
            // Copied fields can be restricted with BindingFlags
            System.Reflection.FieldInfo[] fields = type.GetFields();
            foreach (System.Reflection.FieldInfo field in fields)
                field.SetValue(copy, field.GetValue(original));

            return copy;
        }

        /// <summary>
        /// 找出 Array內, true 或 false的數量
        /// </summary>
        /// <param name="Boolean array"></param>
        /// <param name="flag(true or false)"></param>
        /// <returns></returns>
        public static int FindCountOfStateInBoolArray(bool[] array, bool trurorfalse)
        {
            int value = 0;
            for (int i = 0; i < array.Length; i++)
                if (array[i].Equals(trurorfalse))
                    value++;

            return value;
        }

        #region CompareTwoArray
        /// <summary>
        /// 比較兩個Array(int, float, boolean, string) 的數值, 是否相同
        /// </summary>
        /// <param name="array1"></param>
        /// <param name="array2"></param>
        /// <returns></returns>
        public static bool CompareTwoArray(float[] array1, float[] array2)
        {
            bool result = array1.SequenceEqual(array2) ? true : false;
            return result;
        }
        public static bool CompareTwoArray(int[] array1, int[] array2)
        {
            bool result = array1.SequenceEqual(array2) ? true : false;
            return result;
        }
        public static bool CompareTwoArray(string[] array1, string[] array2)
        {
            bool result = array1.SequenceEqual(array2) ? true : false;
            return result;
        }
        public static bool CompareTwoArray(bool[] array1, bool[] array2)
        {
            bool result = array1.SequenceEqual(array2) ? true : false;
            return result;
        }
        #endregion

        #region ArrayValueGreaterThanValue, ArrayValueLessThanValue
        /// <summary>
        /// Array(int, float) 內數值大於某一數
        /// </summary>
        /// <param name="array"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static bool ArrayValueGreaterThanValue(int[] array, int limit)
        {
            bool result = array.All(_value => _value > limit) ? true : false;
            return result;
        }
        public static bool ArrayValueGreaterThanValue(float[] array, float limit)
        {
            bool result = array.All(_value => _value > limit) ? true : false;
            return result;
        }
        public static bool ArrayValueLessThanValue(int[] array, int limit)
        {
            bool result = array.All(_value => _value < limit) ? true : false;
            return result;
        }
        public static bool ArrayValueLessThanValue(float[] array, float limit)
        {
            bool result = array.All(_value => _value < limit) ? true : false;
            return result;
        }
        #endregion

        /// <summary>
        /// String to quaternion, likes log
        /// </summary>
        /// <param name="sQuaternion"></param>
        /// <returns></returns>
        public static Quaternion StringToQuaternion(string sQuaternion)
        {
            // Remove the parentheses
            if (sQuaternion.StartsWith("(") && sQuaternion.EndsWith(")"))
                sQuaternion = sQuaternion.Substring(1, sQuaternion.Length - 2);

            // split the items
            string[] sArray = sQuaternion.Split(',');

            // store as a Vector3
            Quaternion result = new Quaternion(
                float.Parse(sArray[0]),
                float.Parse(sArray[1]),
                float.Parse(sArray[2]),
                float.Parse(sArray[3]));
            return result;
        }

        /// <summary>
        /// Value from -180 to 180.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static float GetDegree(float value)
        {
            value %= 360;
            value = value > 180 ? value - 360 : value;
            return value;
        }

        /// <summary>
        /// Find inactive object
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static GameObject FindInActiveObjectByName(string name)
        {
            Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
            for (int i = 0; i < objs.Length; i++)
            {
                if (objs[i].hideFlags == HideFlags.None)
                {
                    if (objs[i].name == name)
                    {
                        return objs[i].gameObject;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Check MAC address
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>  
        public static bool CheckMac(string input)
        {
            input = input.Replace(" ", "").Replace(":", "").Replace("-", "");
            Regex r = new Regex("^(?:[0-9a-fA-F]{2}:){5}[0-9a-fA-F]{2}|(?:[0-9a-fA-F]{2}-){5}[0-9a-fA-F]{2}|(?:[0-9a-fA-F]{2}){5}[0-9a-fA-F]{2}$");
            if (r.IsMatch(input))
                return true;
            else
                return false;
        }

        /// <summary>
        /// 產生一組 UUID
        /// </summary>
        /// <returns></returns>
        public static string CreateUUID()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
