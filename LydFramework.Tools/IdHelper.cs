using IdGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Tools
{
    public static class IdHelper
    {
        public static long GetId()
        {
            var generator = new IdGenerator(123);
            return generator.CreateId();
        }

        private static readonly long BaseDateTicks = new DateTime(1900, 1, 1).Ticks;

        public static Guid GenerateSequentialGuid()
        {
            byte[] guidBytes = Guid.NewGuid().ToByteArray();
            DateTime now = DateTime.UtcNow;
            byte[] timestamp = BitConverter.GetBytes(now.Ticks - BaseDateTicks);
            Array.Reverse(timestamp);

            Array.Copy(timestamp, timestamp.Length - 6, guidBytes, guidBytes.Length - 6, 6);

            return new Guid(guidBytes);
        }
    }
}
