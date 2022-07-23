// Author: John.tc.tsai
// Last update: 20220719

using System;

public static class DateUtils
{
    /// <summary>
    /// Get this week.
    /// </summary>
    /// <returns></returns>
    public static DateTime[] GetThisWeek()
    {
        DateTime[] thisWeek = new DateTime[7];
        thisWeek[0] = DateTime.Now.StartOfWeek(DayOfWeek.Sunday);
        thisWeek[1] = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
        thisWeek[2] = DateTime.Now.StartOfWeek(DayOfWeek.Tuesday);
        thisWeek[3] = DateTime.Now.StartOfWeek(DayOfWeek.Wednesday);
        thisWeek[4] = DateTime.Now.StartOfWeek(DayOfWeek.Thursday);
        thisWeek[5] = DateTime.Now.StartOfWeek(DayOfWeek.Friday);
        thisWeek[6] = DateTime.Now.StartOfWeek(DayOfWeek.Saturday);
        return thisWeek;
    }

    /// <summary>
    /// Gets the number of days in the given month.
    /// </summary>
    public static int GetDaysInMonth(int year, int month)
    {
        return DateTime.DaysInMonth(year, month);
    }

    /// <summary>
    /// For month.
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    public static string ConverInt2CHWeeks(int _day)
    {
        switch (_day)
        {
            default: return "error";
            case 1: return "一";
            case 2: return "二";
            case 3: return "三";
            case 4: return "四";
            case 5: return "五";
            case 6: return "六";
            case 7: return "七";
            case 8: return "八";
            case 9: return "九";
            case 10: return "十";
            case 11: return "十一";
            case 12: return "十二";
        }
    }

    public static string ConvertInt2CHDays(int _day)
    {
        switch (_day)
        {
            default: return "error";
            case 0: return "日";
            case 1: return "一";
            case 2: return "二";
            case 3: return "三";
            case 4: return "四";
            case 5: return "五";
            case 6: return "六";
        }
    }

    /// <summary>
    /// string(int) to String Number
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string Str2StrNumber(string str)
    {
        int.TryParse(str, out int result);
        if (result < 10)
            return $"0{str}";
        else
            return str;
    }




    static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
    {
        int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
        return dt.AddDays(-1 * diff).Date;
    }
}


