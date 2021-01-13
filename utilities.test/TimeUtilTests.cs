using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace utilities.test
{
   [TestClass]
   public class TimeUtilTests
   {
      [TestMethod]
      public void RoundUp_Minutes()
      {
         int granularity_min = 2;
         var startTime = DateTime.Now;
         Console.WriteLine(startTime);

         foreach (var i in Enumerable.Range(-37, 37))
         {
            var testTime = startTime.AddMinutes(i);
            var roundedTime = testTime.RoundUp(TimeSpan.FromMinutes(granularity_min));
            Console.WriteLine(testTime + " -> " + roundedTime);
            Assert.AreEqual(0, roundedTime.Second);
         }
      }

      [TestMethod]
      public void RoundUp_100ms()
      {
         var startTime = DateTime.Now;
         foreach (var i in Enumerable.Range(-37, 37))
         {
            var testTime = startTime.AddMilliseconds(i * 55);
            var roundedTime = testTime.RoundUp(TimeSpan.FromMilliseconds(100));
            Console.WriteLine($"{testTime:o}  ->  {roundedTime:o}");
            Assert.AreEqual(0, roundedTime.Millisecond % 100);
         }
      }

      [TestMethod]
      public void RoundUp_ExactHour_NoRounding()
      {
         var timenow = new DateTime(2021, 1, 8, 14, 0, 0, DateTimeKind.Local);
         var roundedTime = timenow.RoundUp(TimeSpan.FromHours(1));
         Assert.AreEqual(14, roundedTime.Hour);

         Assert.AreEqual(15, timenow.AddTicks(1).RoundUp(TimeSpan.FromHours(1)).Hour);
      }

      [TestMethod]
      public void TimeToNextFullHour()
      {
         var startTime = DateTime.Now;
         Console.WriteLine(startTime);

         int fullHour = 13;

         foreach (var i in Enumerable.Range(-37, 37))
         {
            var testTime = startTime.AddHours(i);
            //Console.WriteLine(testTime + " -> " + MyClass.RoundUp(testTime, TimeSpan.FromHours(24)));
            var timeToNextFullHour = TimeUtil.TimeToNextFullHour(fullHour, testTime);
            Console.WriteLine($"{testTime} -> Time to next {fullHour}:  {timeToNextFullHour}");
            Assert.IsTrue(timeToNextFullHour > TimeSpan.Zero);
            Assert.IsTrue(timeToNextFullHour < TimeSpan.FromHours(24));
         }
      }

      [TestMethod]
      public void TimeToNextHHMM()
      {
         var startTime = DateTime.Now;
         Console.WriteLine(startTime);

         int hour = 20;
         int min = 10;

         foreach (var i in Enumerable.Range(-37, 37))
         {
            var testTime = startTime.AddHours(i);
            //Console.WriteLine(testTime + " -> " + MyClass.RoundUp(testTime, TimeSpan.FromHours(24)));
            var timeToHHMM = TimeUtil.TimeToNextHHMM(hour, min, testTime);
            Console.WriteLine($"{testTime} -> Time to {hour}{min}:  {timeToHHMM}");
            Assert.IsTrue(timeToHHMM > TimeSpan.Zero);
            Assert.IsTrue(timeToHHMM < TimeSpan.FromHours(24));
         }
      }

      [TestMethod]
      public void TimeToNextHHMM_OnlySecondsDiffer_ReturnsCorrectSeconds()
      {
         int hour = 2;
         int min = 3;
         int second = 1;

         var epoch = DateTime.Today;
         epoch += TimeSpan.FromHours(hour);
         epoch += TimeSpan.FromMinutes(min);
         epoch -= TimeSpan.FromSeconds(second);

         var delta = TimeUtil.TimeToNextHHMM(hour, min, epoch);
         Console.WriteLine($"{epoch} -> Time to {hour}:{min} = {delta}");
         Assert.IsTrue(delta.Equals(TimeSpan.FromSeconds(second)));
      }

      [TestMethod]
      public void TimeToNextHHMM_SameTime_Returns24h()
      {
         int hour = 2;
         int min = 3;

         var epoch = DateTime.Today;
         epoch += TimeSpan.FromHours(hour);
         epoch += TimeSpan.FromMinutes(min);

         var delta = TimeUtil.TimeToNextHHMM(hour, min, epoch);
         Console.WriteLine($"{epoch} -> Time to {hour}:{min} = {delta}");
         Assert.IsTrue(delta.Equals(TimeSpan.FromHours(24)));
      }

      [TestMethod]
      public void Trim()
      {
         var now = DateTime.Now;

         Assert.IsTrue(now.Trim(TimeSpan.TicksPerSecond).Millisecond == 0);
         Assert.IsTrue(now.Trim(TimeSpan.TicksPerMinute).Second == 0);
         Assert.IsTrue(now.Trim(TimeSpan.TicksPerHour).Minute == 0);
         Assert.IsTrue(now.Trim(TimeSpan.TicksPerDay).Hour == 0);
         Assert.IsTrue(now.Trim(TimeSpan.TicksPerDay).Minute == 0);
         Assert.IsTrue(now.Trim(TimeSpan.TicksPerDay).Second == 0);
         Assert.IsTrue(now.Trim(TimeSpan.TicksPerDay).Millisecond == 0);
      }

      [TestMethod]
      public void Trim_ExactHour_NoTrimming()
      {
         var timenow = new DateTime(2021, 1, 8, 14, 0, 0, DateTimeKind.Local);
         var trimmedTime = timenow.Trim(TimeSpan.TicksPerHour);
         Assert.AreEqual(14, trimmedTime.Hour);

         Assert.AreEqual(13, timenow.AddTicks(-1).Trim(TimeSpan.TicksPerHour).Hour);
      }
   }
}
