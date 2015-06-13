namespace Megaplan.API.Tests
{
    using Megaplan.API.Enums;
    using Megaplan.API.Queries;

    using NUnit.Framework;

    [TestFixture]
    public class QueryBuiderTests
    {
        [Test]
        public void EmptyTest()
        {
            var sut = new QueryBuider(new TasksQueryParams());

            Assert.AreEqual("", sut.Build());
        }

        [Test]
        public void OnlyActualTest()
        {
            var sut = new QueryBuider(new TasksQueryParams
                                           {
                                               OnlyActual = true
                                           });

            Assert.AreEqual("?OnlyActual=true", sut.Build());
        }

        [Test]
        public void LimitTest()
        {
            var sut = new QueryBuider(new TasksQueryParams
                                           {
                                               Limit = 33
                                           });

            Assert.AreEqual("?Limit=33", sut.Build());
        }

        [Test]
        public void SortByTest()
        {
            var sut = new QueryBuider(new TasksQueryParams
                                           {
                                               SortBy = SortByType.Completed
                                           });

            Assert.AreEqual("?SortBy=completed", sut.Build());
        }

        [Test]
        public void SortOrderTest()
        {
            var sut = new QueryBuider(new TasksQueryParams
                                           {
                                               SortOrder = SortOrderType.Asc
                                           });

            Assert.AreEqual("?SortOrder=asc", sut.Build());
        }

        [Test]
        public void JsonPropertyTest()
        {
            var sut = new QueryBuider(new AddCommentQueryParams()
                                           {
                                               SubjectType = SubjectType.Task,
                                               SubjectId = 1,
                                               Text = "test"
                                           });

            Assert.AreEqual("?SubjectType=task&SubjectId=1&Model[Text]=test", sut.Build());
        }

        [Test]
        public void MultipleParametersTest()
        {

            var sut = new QueryBuider(new TasksQueryParams
            {
                SortBy = SortByType.Completed,
                SortOrder = SortOrderType.Asc
            });

            Assert.AreEqual("?SortBy=completed&SortOrder=asc", sut.Build());
        }
    }
}