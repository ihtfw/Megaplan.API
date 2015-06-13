namespace Megaplan.API.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Megaplan.API.Models;
    using Megaplan.API.Queries;
    using NUnit.Framework;
    using Task = System.Threading.Tasks.Task;

    [TestFixture]
    public class MegaplanClientTests
    {
        private MegaplanClient client;

        [SetUp]
        public void SetUp()
        {
            client = new MegaplanClient("megaplan.ru");
        }

        private async Task Authorize()
        {
            await client.Authorize("validLogin", "validPass");
        }

        [Test]
        public async void AuthorizeWithWrongLoginPass()
        {
            await client.Authorize("login", "pass");

            Assert.That(client.IsAuthorized, Is.False);
        }

        [Test]
        public async void AuthorizeWithCorrectLoginPass()
        {
            await Authorize();

            Assert.That(client.IsAuthorized, Is.True);
        }

        [Test]
        public async void ServerTimeTest()
        {
            var time = await client.ServerTime();
        }

        #region Comments

        [Test]
        public async void CommentsTest()
        {
            await Authorize();
            var task = (await client.Tasks(TasksQueryParams.Responsible())).First();
            var comments = await client.Comments(CommentsQueryParams.Task(task.Id));

            Assert.That(comments.Any(), Is.True);
        }

        [Test]
        public async void AllCommentsTest()
        {
            await Authorize();
            var comments = await client.Comments(AllCommentsQueryParams.Actual());

            Assert.That(comments.Any(), Is.True);
        }

        [Test]
        public async void UnreadCommentsTest()
        {
            await Authorize();
            var comments = await client.UnreadComments(AllCommentsQueryParams.Actual());

            Assert.That(comments.Any(), Is.True);
        }

        [Test]
        public async void MarkCommentAsReadTest()
        {
            await Authorize();

            var comments = await client.UnreadComments(AllCommentsQueryParams.Actual());

            var unreadComment = comments.First();
            await client.MarkCommentAsRead(unreadComment.Id);

            var afterComments = await client.UnreadComments(AllCommentsQueryParams.Actual());

            Assert.AreEqual(comments.Count - 1, afterComments.Count);
        }

        [Test]
        public async void AddCommentTest()
        {
            await Authorize();

            var id = 1001537;
            var card = await client.Card(id);
            await client.AddComment(AddCommentQueryParams.Task(id, "New comment text"));
        }

        #endregion

        #region Tasks

        [Test]
        public async void TasksNoParamsTest()
        {
            await Authorize();
            var task = await client.Tasks();

            Assert.That(task.Any(), Is.True);
        }

        [Test]
        public async void TasksResponsibleTest()
        {
            await Authorize();
            var task = await client.Tasks(TasksQueryParams.Responsible());

            Assert.That(task.Any(), Is.True);
        }

        [Test]
        public async void AddTaskTest()
        {
            await Authorize();

//            var task = await client.AddTask(AddTaskQueryParams.Simple("Тестовое название", "тестовоя суть задачи", 1000025));
            var task = await client.AddTask(AddTaskQueryParams.Simple("Тестовое название", "тестовоя суть задачи", 1000001));

            Assert.That(task, Is.Not.Null);
        }

        [Test]
        public async void AddTaskToEmployeeTest()
        {
            await Authorize();

            var employee = (await client.Employees(EmployeesQueryParams.FilterByName("someone name"))).Single();
            var task = await client.AddTask(AddTaskQueryParams.Simple("API Тестовое название", " API тестовоя суть задачи", employee.Id));

            Assert.That(task, Is.Not.Null);
        }

        #endregion

        #region Employees

        [Test]
        public async void EmployeesNoParamsTest()
        {
            await Authorize();
            var employees = await client.Employees();

            Assert.That(employees.Any(), Is.True);
        }

        [Test]
        public async void EmployeesFilterByNameTest()
        {
            await Authorize();
            var employees = await client.Employees(EmployeesQueryParams.FilterByName("someone name"));

            Assert.That(employees.Count, Is.EqualTo(1));
        }

        #endregion
    }
}