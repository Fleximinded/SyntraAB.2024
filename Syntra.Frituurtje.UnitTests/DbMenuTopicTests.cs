using Syntra.Frituurtje.Contracts.Models;
using Syntra.Frituurtje.Database.Repository;
using Syntra.Frituurtje.UnitTests.TestHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syntra.Frituurtje.UnitTests
{
    public class DbMenuTopicTests
    {
        [Theory]
        [MemberData(nameof(GetMenuTopicData))]
        public async Task Repo_GetAllTopics_ShouldGetAllTopics(object[] data)
        {
            // Arrange
            List<MenuTopic> topicList = data.Select(s => s as MenuTopic ?? new MenuTopic()).ToList();
            using var context = FrituurtjeTestDb.CreateContext();
            context.FillTopicData(topicList);
            var repo = new MenuRepository(context);
            // Act
            var topics = await repo.GetAllTopicsAsync(false,false);
            // Assert
            Assert.Equal(topics.Count(), topicList.Count);


            topics = topics.Take(topics.Count() / 2);

            foreach(var topic in topicList)
            {
                Assert.Contains(topics, t => t.Id == topic.Id && t.Title == topic.Title && t.Description == topic.Description);
            }

        }

        public static IEnumerable<object[]> GetMenuTopicData() => MenuTopicData.Generate();

    }
}
