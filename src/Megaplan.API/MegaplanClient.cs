﻿namespace Megaplan.API
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    using Megaplan.API.Enums;
    using Megaplan.API.Exceptions;
    using Megaplan.API.Models;
    using Megaplan.API.Queries;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Linq;

    using Task = System.Threading.Tasks.Task;

    public class MegaplanClient : IMegaplanClient
    {
        private readonly string host;

        private string accessId;

        private string secretKey;

        public MegaplanClient(string host)
        {
            this.host = host;
        }
        
        public int? Timeout { get; set; }

        public bool IsAuthorized { get; private set; }

        #region Cards

        /// <summary>
        ///     Карточка задачи
        ///     https://help.megaplan.ru/API_task_card
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Card> Card(int id)
        {
            return MakeGetRequest<Card>("/BumsTaskApiV01/Task/card.api", "task", new
                                                                                 {
                                                                                     Id = id
                                                                                 });
        }

        #endregion

        #region Clients

        /// <summary>
        ///     Карточка клиента
        ///    https://help.megaplan.ru/API_contractor_card
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Client> ClientCard(int id)
        {
            return MakeGetRequest<Client>("/BumsCrmApiV01/Contractor/card.api", "contractor", new
            {
                Id = id
            });
        }

        /// <summary>
        ///     Список клиентов
        ///     https://help.megaplan.ru/API_contractor_list
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public Task<List<Client>> Clients(ClientsQueryParams queryParams = null)
        {
            return MakePostRequest<List<Client>>("/BumsCrmApiV01/Contractor/list.api", "clients", queryParams);
        }

        #endregion

        /// <summary>
        /// Серверное время
        /// https://help.megaplan.ru/API_system_datetime
        /// </summary>
        /// <returns></returns>
        public async Task<DateTime> ServerTime()
        {
            var response = await GetRequest("/BumsCommonApiV01/System/datetime.api", false).ConfigureAwait(false);

            var content = response.Content();

            var jData = ParseResponse(content);
            var datetime = (string)jData["datetime"];
            return DateTime.Parse(datetime);
        }

        /// <exception cref="AuthorizeException"></exception>
        public async Task Authorize(string login, string pass)
        {
            HttpWebResponse response;
            try
            {
                var passHash = Hashes.MD5(pass);
                response =
                    await
                        PostRequest(
                            string.Format("/BumsCommonApiV01/User/authorize.api?Login={0}&Password={1}", login, passHash),
                            false).ConfigureAwait(false);
            }
            catch (WebException e)
            {
                var httpResponse = e.Response as HttpWebResponse;
                if (httpResponse == null)
                {
                    throw new AuthorizeException("Ошибка при запросе", e);
                }

                switch (httpResponse.StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                    case HttpStatusCode.Forbidden:
                        return;
                }

                throw new AuthorizeException("Ошибка при запросе", e);
            }

            var content = response.Content();
            var jData = ParseResponse(content);

            accessId = (string)jData["AccessId"];
            secretKey = (string)jData["SecretKey"];
            UserId = (int)jData["UserId"];
            EmployeeId = (int)jData["EmployeeId"];

            IsAuthorized = true;
        }

        public int EmployeeId { get; private set; }

        public int UserId { get; private set; }
        
        private JToken ParseResponse(string response)
        {
            var jObject = JObject.Parse(response);
            if ((string)jObject["status"]["code"] != "ok")
            {
                throw new HttpRequestException((string)jObject["status"]["error"]);
            }

            if (jObject.Children().Any(c => c.Path == "data"))
            {
#if DEBUG
                Debug.WriteLine("data {0}", jObject["data"].ToString());
#endif
                return jObject["data"];
            }

            var lastToken = jObject.Last;
#if DEBUG
            Debug.WriteLine($"{lastToken.Path} {lastToken}");
#endif
            return lastToken;
        }

        #region Tasks

        /// <summary>
        ///     Создание задачи
        ///     https://help.megaplan.ru/API_task_create
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public Task<Models.Task> AddTask(AddTaskQueryParams queryParams)
        {
            return MakePostRequest<Models.Task>("/BumsTaskApiV01/Task/create.api", "task", queryParams);
        }

        /// <summary>
        ///     Список задач
        ///     https://help.megaplan.ru/API_task_list
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public Task<List<Models.Task>> Tasks(TasksQueryParams queryParams = null)
        {
            return MakeGetRequest<List<Models.Task>>("/BumsTaskApiV01/Task/list.api", "tasks", queryParams);
        }
        
        /// <summary>
        /// Действие над задачей
        /// https://help.megaplan.ru/API_task_action
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public Task TaskAction(int taskId, TaskActionType action)
        {
            return MakePostRequest("/BumsTaskApiV01/Task/action.api", new 
            {
                Id = taskId,
                Action = action
            });
        }

        /// <summary>
        /// Допустимые действия над задачей
        /// https://help.megaplan.ru/API_task_available_actions
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public Task<List<TaskActionType>> AvailibleTaskActions(int taskId)
        {
            return MakePostRequest<List<TaskActionType>>("/BumsTaskApiV01/Task/availableActions.api", "actions", new
            {
                Id = taskId
            });
        }

        #endregion

        #region Comments

        /// <summary>
        ///     Список комментариев по задаче/проекту
        ///     https://help.megaplan.ru/API_comment_list
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public Task<List<Comment>> Comments(CommentsQueryParams queryParams)
        {
            return MakeGetRequest<List<Comment>>("/BumsCommonApiV01/Comment/list.api", "comments", queryParams);
        }

        public async Task<List<Comment>> UnreadComments(AllCommentsQueryParams queryParams)
        {
            return (await Comments(queryParams).ConfigureAwait(false)).Where(c => c.IsUnread).ToList();
        }

        /// <summary>
        ///     Список всех комментариев по актуальным задачам и проектам
        ///     https://help.megaplan.ru/API_comment_all
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public Task<List<Comment>> Comments(AllCommentsQueryParams queryParams)
        {
            return MakeGetRequest<List<Comment>>("/BumsCommonApiV01/Comment/all.api", "comments", queryParams);
        }

        /// <summary>
        ///     Отметить комментарий как прочитанный
        ///     https://help.megaplan.ru/API_comment_mark_as_read
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task MarkCommentAsRead(int id)
        {
            return MakePostRequest("/BumsCommonApiV01/Comment/markAsRead.api", MarkCommentAsReadQueryParams.Create(id));
            //            var baseQuery = "/BumsCommonApiV01/Comment/markAsRead.api" +
            //                           new QueryBuider(MarkCommentAsReadQueryParams.Create(id)).Build();

            //            var response = await PostRequest(baseQuery, true);
        }

        /// <summary>
        ///     Создание комментария
        ///     https://help.megaplan.ru/API_comment_create
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public Task<Comment> AddComment(AddCommentQueryParams queryParams)
        {
            return MakePostRequest<Comment>("/BumsCommonApiV01/Comment/create.api", "comment", queryParams);
        }

        #endregion

        #region Employees

        /// <summary>
        ///     Список сотрудников
        ///     https://help.megaplan.ru/API_employee_list
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public Task<List<Employee>> Employees(EmployeesQueryParams queryParams = null)
        {
            return MakePostRequest<List<Employee>>("/BumsStaffApiV01/Employee/list.api", "employees", queryParams);
        }

        /// <summary>
        ///     Карточка сотрудника
        ///     https://help.megaplan.ru/API_employee_card
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public async Task<Employee> EmployeeCard(int id)
        {
            return await MakePostRequest<Employee>("/BumsStaffApiV01/Employee/card.api", "employee", new
                                                                                                     {
                                                                                                         Id = id
                                                                                                     });
        }

        #endregion

        #region Helpers

        StringEnumConverter StringEnumConverter = new StringEnumConverter();
        public async Task<T> MakeGetRequest<T>(string baseQuery, string jsonKey, object queryParams = null)
        {
            using(var response = await GetRequest(baseQuery + new QueryBuider(queryParams).Build(), true).ConfigureAwait(false))
            {
                var content = response.Content();

                var jData = ParseResponse(content);
                var data = jData[jsonKey].ToString();
                var tasks = JsonConvert.DeserializeObject<T>(data, StringEnumConverter);
                return tasks;
            }
        }

        public async Task<T> MakePostRequest<T>(string baseQuery, string jsonKey, object queryParams = null)
        {
            using (var response = await PostRequest(baseQuery, new QueryBuider(queryParams).BuildPostData(), true).ConfigureAwait(false))
            {
                var content = response.Content();

                string data = null;
                var jData = ParseResponse(content);
                if (jData is JProperty)
                {
                    foreach (var child in jData.Children())
                    {
                        if (child.Path == jsonKey)
                        {
                            data = child.ToString();
                        }
                    }
                    if (data == null)
                        throw new ArgumentException("Not found: " + jsonKey);
                }
                else
                {
                    data = jData[jsonKey].ToString();
                }
                
                var tasks = JsonConvert.DeserializeObject<T>(data, StringEnumConverter);
                return tasks;
            }
        }

        public async Task MakePostRequest(string baseQuery, object queryParams = null)
        {
            var response = await PostRequest(baseQuery, new QueryBuider(queryParams).BuildPostData(), true).ConfigureAwait(false);
            response.Dispose();
        }

        [DebuggerStepThrough]
        public async Task<HttpWebResponse> CreateRequest(string method, string subUrl, byte[] postData, bool signRequest)
        {
#if DEBUG
            Debug.WriteLine("{0}:\t{1} | {2}", method, subUrl, signRequest);
#endif
            Request request;
            if (signRequest)
            {
                request = new Request(method, host, subUrl, postData, accessId, secretKey);
            }
            else
            {
                request = new Request(method, host, subUrl, postData);
            }

            request.Timeout = Timeout;

            return await request.GetResponse().ConfigureAwait(false);
        }

#if !PCL
        public async Task Download(string url, string path, CancellationToken ct, IProgress<DownloadProgressArgs> progress)
        {
            try
            {
                using (var filestream = new FileStream(path, FileMode.OpenOrCreate))
                {
                    await Download(url, filestream, ct, progress);
                }
            }
            catch
            {
                File.Delete(path);
                throw;
            }
        }
#endif

        public async Task Download(string url, Stream streamToWrite, CancellationToken ct, IProgress<DownloadProgressArgs> progress)
        {
            var bufferSize = 2048;
            var bufferBytes = new byte[bufferSize];

            var respone = await GetRequest(url, true).ConfigureAwait(false);
            var totalSize = long.Parse(respone.Headers["Content-Length"]);
            using (var responseStream = respone.GetResponseStream())
            {
                while (true)
                {
                    ct.ThrowIfCancellationRequested();
                    var readSize = await responseStream.ReadAsync(bufferBytes, 0, bufferBytes.Length, ct).ConfigureAwait(false);
                    if (readSize == 0)
                    {
                        break;
                    }
                    // Cancle download file 
                    await streamToWrite.WriteAsync(bufferBytes, 0, readSize, ct).ConfigureAwait(false);
                    var args = new DownloadProgressArgs(streamToWrite.Position, totalSize);
                    progress.Report(args);
                }
            }
        }

        [DebuggerStepThrough]
        public Task<HttpWebResponse> GetRequest(string subUrl, bool signRequest)
        {
            return CreateRequest("GET", subUrl, null, signRequest);
        }

        [DebuggerStepThrough]
        public Task<HttpWebResponse> PostRequest(string subUrl, bool signRequest)
        {
            return CreateRequest("POST", subUrl, null, signRequest);
        }

        [DebuggerStepThrough]
        public Task<HttpWebResponse> PostRequest(string subUrl, byte[] postData, bool signRequest)
        {
            return CreateRequest("POST", subUrl, postData, signRequest);
        }

        [DebuggerStepThrough]
        public Task<HttpWebResponse> PutRequest(string subUrl, bool signRequest)
        {
            return CreateRequest("PUT", subUrl, null, signRequest);
        }

#endregion
    }
}