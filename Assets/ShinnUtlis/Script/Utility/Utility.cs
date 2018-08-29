namespace Shinn
{
    public class Utility
    {

        public static float Map(float v, float a, float b, float x, float y)
        {
            return (v == a) ? x : (v - a) * (y - x) / (b - a) + x;
        }

    }

}