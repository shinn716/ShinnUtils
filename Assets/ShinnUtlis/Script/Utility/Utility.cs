namespace Shinn
{
    public class Utility
    {

        public static float Map(float v, float a, float b, float x, float y)
        {
            return (v == a) ? x : (v - a) * (y - x) / (b - a) + x;
        }
        
        
        public static int[] NonrepetitiveRandom(int total)
        {
            int[] sequence = new int[total];
            int[] output = new int[total];

            for (int i = 0; i < total; i++)
            {
                sequence[i] = i;
            }

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
        
        //String to float
        public static float GetFloat(string stringValue, float defaultValue)
	    {
		    float result = defaultValue;
		    float.TryParse(stringValue, out result);
		    return result;
	    }
        

    }

}
