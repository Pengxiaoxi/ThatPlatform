using System;
using System.Collections.Generic;
using System.Text;

namespace ThatPlatform.Tools.UtilLibrary
{
    /// <summary>
    /// DateTimeHelper
    /// </summary>
    public static class DateTimeHelper
    {
        private static long GetNowTimeStamp()
        {
            return GetTimeStamp(DateTime.Now);
        }

        private static long GetUtcNowTimeStamp()
        {
            return GetTimeStamp(DateTime.UtcNow);
        }

        private static long GetTimeStamp(DateTime dateTime)
        {
            return new DateTimeOffset(dateTime).ToUnixTimeSeconds();

        }
    }
}