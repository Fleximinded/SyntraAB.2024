using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syntra.Frituurtje.Contracts.Models;

namespace Syntra.Frituurtje.UnitTests.TestHelpers
{
    public class MenuTopicData
    {
        Random _rand = new Random();
        public IEnumerable<object[]> GenerateRandomDbTopics()
        {
            var count = _rand.Next(4, 30);
            var topics = new object[count];
            for(int i = 0; i < topics.Length; i++)
            {
                topics[i] = new MenuTopic() { Title = $"Title-{_rand.Next(1,short.MaxValue)}", Description = Guid.NewGuid().ToString() };
            }
            yield return new object[] { topics };
        }
        public static IEnumerable<object[]> Generate() => new MenuTopicData().GenerateRandomDbTopics();
    }
}
