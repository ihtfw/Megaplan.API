using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading;
using Megaplan.API.Enums;
using Newtonsoft.Json;

namespace Megaplan.API.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    using Megaplan.API.Models;
    using Megaplan.API.Queries;
    using NUnit.Framework;
    using Task = System.Threading.Tasks.Task;

    public class MegaplanClientTestsSettings
    {
        public MegaplanClientTestsSettings()
        {
            Host = "megaplan.ru";
            Login = "pupkin@gmail.com";
            Password = "123456";
            ExistingEmployeeName = "Пупкин Василий";
            ExistingClientName = "ООО Рога и Копыта";
        }

        public string Host { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public string ExistingEmployeeName { get; set; }
        public string ExistingClientName { get; set; }

        public static MegaplanClientTestsSettings Load()
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Megaplan.API.Tests.Settings.json");
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
            if (!File.Exists(path))
            {
                var serialized = JsonConvert.SerializeObject(new MegaplanClientTestsSettings(), jsonSerializerSettings);
                File.WriteAllText(path, serialized);
                throw new FileNotFoundException(string.Format("No file with config {0}. Default file was created", path));
            }

            var settings = JsonConvert.DeserializeObject<MegaplanClientTestsSettings>(File.ReadAllText(path),
                jsonSerializerSettings);

            return settings;
        }
    }

    [TestFixture]
    public class MegaplanClientTests
    {
        private MegaplanClient client;
        private MegaplanClientTestsSettings settings;

        [SetUp]
        public void SetUp()
        {
            settings = MegaplanClientTestsSettings.Load();
            client = new MegaplanClient(settings.Host);
        }

        private async Task Authorize()
        {
            await client.Authorize(settings.Login, settings.Password);
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
        [Category("Manual")]
        public async void DownloadFile() 
        {
            await Authorize();

            var comments = await client.Comments(CommentsQueryParams.Task(1001702));
            var attach = comments.First().Attaches.First();
            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Cancel();
            IProgress<DownloadProgressArgs> progress = new Progress<DownloadProgressArgs>();
            await client.Download(attach.Url, $@"D:\{attach.Name}", cancellationTokenSource.Token, progress);
        }

        [Test]
        [Category("Manual")]
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
        [Category("Manual")]
        public async void AddTaskTest()
        {
            await Authorize();
            var task = await client.AddTask(AddTaskQueryParams.Simple("Тестовое название", "тестовоя суть задачи", 1000001));

            Assert.That(task, Is.Not.Null);
        }

        [Test]
        [Category("Manual")]
        public async void AddTaskToEmployeeTest()
        {
            await Authorize();

            var employee = (await client.Employees(EmployeesQueryParams.FilterByName(settings.ExistingEmployeeName))).Single();
            var task = await client.AddTask(AddTaskQueryParams.Simple("API Тестовое название", " API тестовя суть задачи", employee.Id));

            Assert.That(task, Is.Not.Null);
        }

        [Test]
        [Category("Manual")]
        public async void AddTaskToEmployeeFromClientTest()
        {
            await Authorize();

            var cl = (await client.Clients(ClientsQueryParams.WithFilter(settings.ExistingClientName))).Single();
            var employee = (await client.Employees(EmployeesQueryParams.FilterByName(settings.ExistingEmployeeName))).Single();
            var @params = AddTaskQueryParams.FromCustomer("API Тестовое название", " API тестовоя суть задачи", employee.Id, cl.Id);
            @params.Auditors = new List<int>(employee.Id);
            var task = await client.AddTask(@params);

            Assert.That(task, Is.Not.Null);
        }


        [Test]
        [Category("Manual")]
        public async void AddTaskTestWithAttachment()
        {
            await Authorize();
            var addTaskQueryParams = AddTaskQueryParams.Simple("Тестовое название", "тестовая суть задачи", 1000001);
            addTaskQueryParams.Attaches.Add(new Attachment{ Name = "1.txt", Content = Convert.ToBase64String(System.Text.Encoding.Default.GetBytes("содержимое файла 1"))});
            addTaskQueryParams.Attaches.Add(new Attachment{ Name = "2.txt", Content =  Convert.ToBase64String(System.Text.Encoding.Default.GetBytes("содержимое файла 2"))});
            var task = await client.AddTask(addTaskQueryParams);

            Assert.That(task, Is.Not.Null);
        }




        [Test]
        [Category("Manual")]
        public async void AddComentWithAttchment()
        {
            await Authorize();
//            var attachment = new Attachment{ Name = "231.txt", Content = Convert.ToBase64String(System.Text.Encoding.Default.GetBytes("содержимое файла 1")) };
            var addCommentQueryParams = AddCommentQueryParams.Task(1001615, "attach11").AttachFile(@"C:\Users\susloparovd.VBEST\Downloads\1436254575-17ad1ff5b5ff2f418a306e84022b5f15.jpg");
            var res = await client.AddComment(addCommentQueryParams);

            Assert.That(res, Is.Not.Null);
        }
        
        [Test]
        [Category("Manual")]
        public async void AvailibleTaskActionsTests()
        {
            await Authorize();
            var tasks = await client.Tasks();
            var task = tasks.First();
            var actions = await client.AvailibleTaskActions(task.Id);

            Assert.That(actions.Any(), Is.True);
        }
        
        [Test]
        [Category("Manual")]
        public async void TaskAction()
        {
            await Authorize();
            var tasks = await client.Tasks();
            var task = tasks.First();
            await client.TaskAction(task.Id, TaskActionType.AcceptTask);
            
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
            var employees = await client.Employees(EmployeesQueryParams.FilterByName(settings.ExistingEmployeeName));

            Assert.That(employees.Count, Is.EqualTo(1));
        }

        [Test]
        public async void EmployeeCardTest()
        {
            await Authorize();
            var employees = await client.Employees(EmployeesQueryParams.FilterByName(settings.ExistingEmployeeName));
            var card = await client.EmployeeCard(employees.First().Id);
            Assert.That(card, Is.Not.Null);
        }

        #endregion

        #region Clients

        [Test]
        public async void ClientsNoParamsTest()
        {
            await Authorize();
            var clients = await client.Clients();

            Assert.That(clients.Any(), Is.True);
        }

        [Test]
        public async void ClientCardTest()
        {
            await Authorize();
            var clients = await client.Clients(ClientsQueryParams.WithFilter(settings.ExistingClientName));
            var clientCard = await client.ClientCard(clients.First().Id);

            Assert.That(clientCard, Is.Not.Null);
        }

        [Test]
        public async void ClientsFilterTest()
        {
            await Authorize();
            var clients = await client.Clients(ClientsQueryParams.WithFilter(settings.ExistingClientName));

            Assert.That(clients.Count, Is.EqualTo(1));
        }

        #endregion

    }
}