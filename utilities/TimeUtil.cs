using System;

namespace utilities
{
   public static class TimeUtil
   {
      /// <summary>
      /// Round up the datetime using the provided resolution.
      /// NOTE! Does not round up, if the time's resolution already is rounded to the desired resolution.
      /// </summary>
      /// <param name="datetime">time to round up</param>
      /// <param name="resolution">desired resolution, e.g. TimeSpan.FromSeconds(1)</param>
      /// <returns></returns>
      public static DateTime RoundUp(this DateTime datetime, TimeSpan resolution)
      {
         return new DateTime((datetime.Ticks + resolution.Ticks - 1) / resolution.Ticks * resolution.Ticks, datetime.Kind);
      }

      /// <summary>
      /// Trim (RoundDown) a datetime by reducing accuracy to the desired level.
      /// </summary>
      /// <param name="datetime">this DateTime (Trim is an extension method)</param>
      /// <param name="ticks">Level of accurary required, e.g. TimeSpan.TicksPerSecond</param>
      /// <returns></returns>
      public static DateTime Trim(this DateTime datetime, long ticks)
      {
         return new DateTime(datetime.Ticks - (datetime.Ticks % ticks), datetime.Kind);
      }

      public static TimeSpan TimeToNextFullHour(int hour, DateTime? epoch = null)
      {
         var startTime = epoch ?? DateTime.Now;

         var delta_after_hour = startTime.TimeOfDay - TimeSpan.FromHours(hour);
         if (delta_after_hour < TimeSpan.Zero) delta_after_hour += TimeSpan.FromDays(1);
         return TimeSpan.FromDays(1) - delta_after_hour;
      }

      public static TimeSpan TimeToNextHHMM(int hour, int min, DateTime? epoch = null)
      {
         var startTime = epoch ?? DateTime.Now;

         var delta_after_hour = startTime.TimeOfDay - TimeSpan.FromHours(hour) - TimeSpan.FromMinutes(min);
         if (delta_after_hour < TimeSpan.Zero) delta_after_hour += TimeSpan.FromDays(1);
         return TimeSpan.FromDays(1) - delta_after_hour;
      }
   }
}
