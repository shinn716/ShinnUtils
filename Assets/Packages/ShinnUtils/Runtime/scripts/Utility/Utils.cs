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
    #region Convert str, hex, int to something
    public class Converter
    {
        public static string UnicodeToString(string _source)
        {
            return new Regex(@"\\u([0-9A-F]{4})", 
                RegexOptions.IgnoreCase | RegexOptions.Compiled).Replace(_source, x => string.Empty + Convert.ToChar(Convert.ToUInt16(x.Result("$1"), 16)));
        }

        public static string StringToUnicode(string _source)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(_source);
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i += 2)
                stringBuilder.AppendFormat("\\u{0}{1}", bytes[i + 1].ToString("x").PadLeft(2, '0'), bytes[i].ToString("x").PadLeft(2, '0'));
            return stringBuilder.ToString();
        }

        /// <summary>
        ///  HexString to Bytes
        ///  string s = "4321000000000000000000000000000000000000"; 
        ///  byte[] bytes = Encoding.ASCII.GetBytes(s);
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static byte[] StringToBytes(string _source)
        {
            byte[] s_bytes = new byte[_source.Length / 2];
            for (int i = 0; i < _source.Length; i = i + 2)
                s_bytes[i / 2] = Convert.ToByte(_source.Substring(i, 2), 16);
            return s_bytes;
        }

        /// <summary>
        /// Show Bytes
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string BytesToString(byte[] _bytes)
        {
            string result = "";
            foreach (var b in _bytes)
                result += b.ToString("X2");
            return result;
        }

        /// <summary>
        /// Int 轉換成 String
        /// </summary>
        /// <param name="decValue"></param>
        /// <returns></returns>
        public static string IntToHex(int _decValue)
        {
            return string.Format("{0:x}", _decValue);
        }

        /// <summary>
        /// String 轉換成 int
        /// </summary>
        /// <param name="hexValue"></param>
        /// <returns></returns>
        public static int HexToInt(string _hexValue)
        {
            return (int)Convert.ToInt64(_hexValue, 16);
        }
    }
    #endregion

    #region Write And Read Data
    public class Txt
    {
        /// <summary>
        /// 寫入文字 (addDatatoFiles 重負寫入)
        /// </summary>
        /// <param name="data"></param>
        /// <param name="dataPath"></param>
        /// <param name="addDatatoFiles"></param>
        public static void StringOutput(string _content, string _outputurl, bool _addDatatoFiles = true)
        {
            using (StreamWriter outputFile = new StreamWriter(_outputurl, _addDatatoFiles))
                outputFile.WriteLine(_content);
        }

        /// <summary>
        /// 讀取文字檔, 各別行數
        /// </summary>
        /// <param name="dataPath"></param>
        /// <returns></returns>
        public static string[] ReadLines(string _url)
        {
            string[] lines = File.ReadAllLines(_url);
            return lines;
        }

        /// <summary>
        /// 讀取文字檔
        /// </summary>
        /// <param name="dataPath"></param>
        /// <returns></returns>
        public static string ReadTxt(string _url)
        {
            return File.ReadAllText(_url);
        }
    }
    #endregion

    #region Convert Image to right format
    public class Image
    {
        /// <summary>
        /// Texture to Texture2D
        /// </summary>
        /// <param name="texture"></param>
        /// <returns></returns>
        public static Texture2D TexToTex2D(Texture _texture)
        {
            return Texture2D.CreateExternalTexture(
            _texture.width,
            _texture.height,
            TextureFormat.RGB24,
            false, false,
            _texture.GetNativeTexturePtr());
        }

        /// <summary>
        /// Texture2D to sprite
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="PixelsPerUnit"></param>
        /// <param name="spriteType"></param>
        /// <returns></returns>
        public static Sprite Tex2DToSprite(Texture2D _texture, float _pixelsPerUnit = 100.0f, SpriteMeshType _spriteType = SpriteMeshType.Tight)
        {
            Sprite NewSprite;
            NewSprite = Sprite.Create(_texture,
                new Rect(0, 0, _texture.width, _texture.height),
                new Vector2(0, 0), _pixelsPerUnit, 0, _spriteType);
            return NewSprite;
        }

        /// <summary>
        /// Load texture2D from file
        /// </summary>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        public static Texture2D LoadTex2D(string _url)
        {
            Texture2D Tex2D;
            byte[] FileData;
            if (File.Exists(_url))
            {
                FileData = File.ReadAllBytes(_url);
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
        public static Sprite LoadSprite(string _url, float _pixelsPerUnit = 100.0f, SpriteMeshType _spriteType = SpriteMeshType.Tight)
        {
            Sprite NewSprite;
            Texture2D SpriteTexture = LoadTex2D(_url);
            NewSprite = Sprite.Create(SpriteTexture,
                new Rect(0, 0, SpriteTexture.width, SpriteTexture.height),
                new Vector2(0, 0), _pixelsPerUnit, 0, _spriteType);
            return NewSprite;
        }
    }
    #endregion

    public class Utils
    {
        /// <summary>
        /// Bitmask to int array
        /// </summary>
        /// <param name="mask"></param>
        /// <returns></returns>
        public static int[] Bitmask2Array(LayerMask _layoutMask)
        {
            var bitarray = new BitArray(new[] { _layoutMask.value });
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
        /// Get Ip address
        /// </summary>
        public static string[] GetLocalIP(System.Net.Sockets.AddressFamily _type = System.Net.Sockets.AddressFamily.InterNetwork)
        {
            var host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
            List<string> returnvalue = new List<string>();
            foreach (var i in host.AddressList)
                if (i.AddressFamily == _type)
                    if (i != null)
                        returnvalue.Add(i.ToString());
            return returnvalue.ToArray();
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        /// <summary>
        /// CopyComponent function, from https://answers.unity.com/questions/458207/copy-a-component-at-runtime.html
        /// </summary>
        public static Component CopyComponent(Component _original, GameObject _destination)
        {
            Type type = _original.GetType();
            Component copy = _destination.AddComponent(type);
            System.Reflection.FieldInfo[] fields = type.GetFields();
            foreach (System.Reflection.FieldInfo field in fields)
                field.SetValue(copy, field.GetValue(_original));
            return copy;
        }

        /// <summary>
        /// 找出 Array內, true 或 false的數量
        /// </summary>
        /// <param name="Boolean array"></param>
        /// <param name="flag(true or false)"></param>
        /// <returns></returns>
        public static int FindCountOfStateInBoolArray(bool[] _array, bool _trueOrfalse = true)
        {
            int value = 0;
            for (int i = 0; i < _array.Length; i++)
                if (_array[i].Equals(_trueOrfalse))
                    value++;
            return value;
        }

        #region CompareTwoArray
        /// <summary>
        /// 比較兩個Array 的數值, 是否相同
        /// </summary>
        /// <param name="array1"></param>
        /// <param name="array2"></param>
        /// <returns></returns>
        public static bool Compare2Array<T, S>(T[] arrayA, S[] arrayB)
        {
            if (arrayA.Length != arrayB.Length) 
                return false;
            for (int i = 0; i < arrayA.Length; i++)
                if (!arrayA[i].Equals(arrayB[i])) 
                    return false;
            return true;
        }

        public enum SignType
        {
            GREATER,
            LESS
        }
        /// <summary>
        /// Array(int, float) 內數值大於某一數
        /// </summary>
        /// <param name="array"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static bool Compare2ArrayValue(float[] _array, int _limit, SignType _signType = SignType.GREATER)
        {
            bool result = false;
            if (_signType.Equals(SignType.GREATER))
                result = _array.All(_value => _value > _limit) ? true : false;
            else
                result = _array.All(_value => _value < _limit) ? true : false;
            return result;
        }
        public static bool Compare2ArrayValue(int[] _array, int _limit, SignType _signType = SignType.GREATER)
        {
            bool result = false;
            if (_signType.Equals(SignType.GREATER))
                result = _array.All(_value => _value > _limit) ? true : false;
            else
                result = _array.All(_value => _value < _limit) ? true : false;
            return result;
        }
        #endregion

        /// <summary>
        /// String to quaternion, likes log
        /// </summary>
        /// <param name="sQuaternion"></param>
        /// <returns></returns>
        public static Quaternion Str2Quaternion(string _sQuaternion)
        {
            // Remove the parentheses
            if (_sQuaternion.StartsWith("(") && _sQuaternion.EndsWith(")"))
                _sQuaternion = _sQuaternion.Substring(1, _sQuaternion.Length - 2);

            // split the items
            string[] sArray = _sQuaternion.Split(',');

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
        public static float GetDegree(float _value)
        {
            _value %= 360;
            _value = _value > 180 ? _value - 360 : _value;
            return _value;
        }

        /// <summary>
        /// Find inactive object
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static GameObject FindInactiveObject(string _name)
        {
            Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
            for (int i = 0; i < objs.Length; i++)
                if (objs[i].hideFlags == HideFlags.None)
                    if (objs[i].name == _name)
                        return objs[i].gameObject;
            return null;
        }
        
        /// <summary>
        /// 產生一組 UUID
        /// </summary>
        /// <returns></returns>
        public static string CreateUUID()
        {
            return Guid.NewGuid().ToString("N");
        }
                
        /// <summary>
        /// 取得距離最近的物件
        /// </summary>
        /// <param name="objs"></param>
        /// <param name="currentPos"></param>
        /// <returns></returns>
        public static Transform GetClosestObject(Transform[] _objs, Vector3 _currentPos)
        {
            Transform tMin = null;
            float minDist = Mathf.Infinity;
            foreach (Transform t in _objs)
            {
                float dist = Vector3.Distance(t.position, _currentPos);
                if (dist < minDist)
                {
                    tMin = t;
                    minDist = dist;
                }
            }
            return tMin;
        }
    }
}
