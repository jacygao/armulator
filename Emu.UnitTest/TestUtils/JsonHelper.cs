using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Emu.UnitTest.TestUtils
{
    internal class JsonHelper
    {
        /// <summary>
        /// Converts a JSON string to an object of type T.
        /// </summary>
        /// <typeparam name="T">The type of the object to convert to.</typeparam>
        /// <param name="jsonString">The JSON string to convert.</param>
        /// <returns>An object of type T.</returns>
        public static T DeserializeJson<T>(string jsonString)
        {
            if (string.IsNullOrEmpty(jsonString))
            {
                throw new ArgumentException("JSON string cannot be null or empty.", nameof(jsonString));
            }

            return JsonSerializer.Deserialize<T>(jsonString);
        }
    }
}
