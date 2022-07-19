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
    public static string ConverInt2Characters(int i)
    {
        if (i.Equals(1))
            return "一";
        else if (i.Equals(2))
            return "二";
        else if (i.Equals(3))
            return "三";
        else if (i.Equals(4))
            return "四";
        else if (i.Equals(5))
            return "五";
        else if (i.Equals(6))
            return "六";
        else if (i.Equals(7))
            return "七";
        else if (i.Equals(8))
            return "八";
        else if (i.Equals(9))
            return "九";
        else if (i.Equals(10))
            return "十";
        else if (i.Equals(11))
            return "十一";
        else if (i.Equals(12))
            return "十二";
        else
            return "Error";
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


