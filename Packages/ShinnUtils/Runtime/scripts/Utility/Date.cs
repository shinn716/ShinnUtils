// Author: John.tc.tsai
// Last update: 20220719

using System;

namespace Shinn
{
    public static class Date
    {
        /// <summary>
        /// Get this week.
        /// </summary>
        /// <returns></returns>
        public static DateTime[] GetThisWeek()
        {
            return DateTime.Now.ThisWeek();
        }

        /// <summary>
        /// Gets the number of days in the given month.
        /// </summary>
        public static int GetDaysInMonth(int _year, int _month)
        {
            return DateTime.DaysInMonth(_year, _month);
        }

        /// <summary>
        /// ConverInt2Months
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static string ConverInt2Months(int _day)
        {
            switch (_day)
            {
                default:
                    return "error";
                case 0:
                    return "一";
                case 1:
                    return "二";
                case 2:
                    return "三";
                case 3:
                    return "四";
                case 4:
                    return "五";
                case 5:
                    return "六";
                case 6:
                    return "七";
                case 7:
                    return "八";
                case 8:
                    return "九";
                case 9:
                    return "十";
                case 10:
                    return "十一";
                case 11:
                    return "十二";
            }
        }

        /// <summary>
        /// ConvertInt2Weeks
        /// </summary>
        /// <param name="_day"></param>
        /// <returns></returns>
        public static string ConvertInt2Weeks(int _day)
        {
            switch (_day)
            {
                default:
                    return "error";
                case 0:
                    return "日";
                case 1:
                    return "一";
                case 2:
                    return "二";
                case 3:
                    return "三";
                case 4:
                    return "四";
                case 5:
                    return "五";
                case 6:
                    return "六";
            }
        }

        /// <summary>
        /// string(int) to String Number
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Str2StrNum(string _str)
        {
            int.TryParse(_str, out int result);
            return result < 10 ? $"0{_str}" : _str;
        }


        static DateTime[] ThisWeek(this DateTime dt)
        {
            DateTime[] weeks = new DateTime[7];
            for (int i = 0; i < weeks.Length; i++)
                weeks[i] = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + i);
            return weeks;
        }
    }
}
