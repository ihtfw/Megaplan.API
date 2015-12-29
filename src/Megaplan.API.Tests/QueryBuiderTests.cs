namespace Megaplan.API.Tests
{
    using System.Collections.Generic;

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



        [Test]
        public void ArrayTest()
        {
            var addTaskQueryParams = AddTaskQueryParams.Simple("name", "statement", 1);
            addTaskQueryParams.Attaches.Add(new Attachment{ Name = "file1", Content = "content1"});
            addTaskQueryParams.Attaches.Add(new Attachment{ Name = "file2", Content = "content2"});


            var sut = new QueryBuider(addTaskQueryParams);

            var actual = sut.Build();
            Assert.AreEqual("?Model[Name]=name&Model[Responsible]=1&Model[IsGroup]=0&Model[Statement]=statement&Model[Attaches][Add][0][Name]=file1&Model[Attaches][Add][0][Content]=content1&Model[Attaches][Add][1][Name]=file2&Model[Attaches][Add][1][Content]=content2", actual);
        }


        [Test]
        public void IntArrayTest()
        {
            var addTaskQueryParams = AddTaskQueryParams.Simple("Name", "Statement", 1);
            addTaskQueryParams.Auditors = new List<int>
                                          {
                                              1, 2, 3
                                          };

            var sut = new QueryBuider(addTaskQueryParams);

            var actual = sut.Build();
            
            Assert.AreEqual("?Model[Name]=Name&Model[Responsible]=1&Model[Auditors]=1%2C2%2C3&Model[IsGroup]=0&Model[Statement]=Statement", actual);
        }

    }
}