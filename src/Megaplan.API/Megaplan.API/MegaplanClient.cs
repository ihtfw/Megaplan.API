using System;
using System.Collections.Generic;
using System.Linq;

namespace Megaplan.API
{
    using System.Diagnostics;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Megaplan.API.Exceptions;
    using Megaplan.API.Models;
    using Megaplan.API.Queries;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    using Task = System.Threading.Tasks.Task;

    public class MegaplanClient
    {
        private string accessId;

        private string secretKey;

        private readonly string host;

        public MegaplanClient(string host)
        {
            this.host = host;
        }

        public bool IsAuthorized { get; private set; }

        #region Cards

        /// <summary>
        /// Карточка сотрудника
        /// https://help.megaplan.ru/API_employee_card
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Card> Card(int id)
        {
            var response = await GetRequest(string.Format("/BumsTaskApiV01/Task/card.api?Id={0}", id), true);
            var content = response.Content();

            var jData = ParseResponse(content);
            var data = jData["task"].ToString();
            var card = JsonConvert.DeserializeObject<Card>(data);

            return card;
        }

        #endregion

        #region Tasks

        /// <summary>
        /// https://help.megaplan.ru/API_task_create
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public async Task<Models.Task> AddTask(AddTaskQueryParams queryParams)
        {
            var baseQuery = "/BumsTaskApiV01/Task/create.api";
            var response = await PostRequest(baseQuery, new QueryBuider(queryParams).BuildPostData(), true);
            var content = response.Content();

            var jData = ParseResponse(content);
            var data = jData["task"].ToString();
            var task = JsonConvert.DeserializeObject<Models.Task>(data);
            return task;
        }

        /// <summary>
        /// Список задач
        /// https://help.megaplan.ru/API_task_list
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public async Task<List<Models.Task>> Tasks(TasksQueryParams queryParams = null)
        {
            var baseQuery = "/BumsTaskApiV01/Task/list.api";
            if (queryParams != null)
            {
                baseQuery += new QueryBuider(queryParams).Build();
            }

            var response = await GetRequest(baseQuery, true);

            var content = response.Content();

            var jData = ParseResponse(content);
            var data = jData["tasks"].ToString();
            var tasks = JsonConvert.DeserializeObject<List<Models.Task>>(data);

            return tasks;
        }

        #endregion

        #region Comments

        /// <summary>
        /// Список комментариев по задаче/проекту
        ///https://help.megaplan.ru/API_comment_list
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public async Task<List<Comment>> Comments(CommentsQueryParams queryParams)
        {
            var baseQuery = "/BumsCommonApiV01/Comment/list.api" + new QueryBuider(queryParams).Build();

            var response = await GetRequest(baseQuery, true);

            var content = response.Content();

            var jData = ParseResponse(content);
            var data = jData["comments"].ToString();
            var comments = JsonConvert.DeserializeObject<List<Comment>>(data);

            return comments;
        }

        public async Task<List<Comment>> UnreadComments(AllCommentsQueryParams queryParams)
        {
            return (await Comments(queryParams)).Where(c => c.IsUnread).ToList();
        }

        /// <summary>
        /// Список всех комментариев по актуальным задачам и проектам
        /// https://help.megaplan.ru/API_comment_all
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public async Task<List<Comment>> Comments(AllCommentsQueryParams queryParams)
        {
            var baseQuery = "/BumsCommonApiV01/Comment/all.api" + new QueryBuider(queryParams).Build();

            var response = await GetRequest(baseQuery, true);

            var content = response.Content();

            var jData = ParseResponse(content);
            var data = jData["comments"].ToString();
            var comments = JsonConvert.DeserializeObject<List<Comment>>(data);

            return comments.Distinct().ToList();
        }

        /// <summary>
        /// Отметить комментарий как прочитанный
        /// https://help.megaplan.ru/API_comment_mark_as_read
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task MarkCommentAsRead(int id)
        {
            var baseQuery = "/BumsCommonApiV01/Comment/markAsRead.api" +
                            new QueryBuider(MarkCommentAsReadQueryParams.Create(id)).Build();

            var response = await PostRequest(baseQuery, true);
        }

        /// <summary>
        /// Создание комментария
        /// https://help.megaplan.ru/API_comment_create
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public async Task<Comment> AddComment(AddCommentQueryParams queryParams)
        {
            var baseQuery = "/BumsCommonApiV01/Comment/create.api";
            var response = await PostRequest(baseQuery, new QueryBuider(queryParams).BuildPostData(), true);
            var content = response.Content();

            var jData = ParseResponse(content);
            var data = jData["comment"].ToString();
            var comment = JsonConvert.DeserializeObject<Comment>(data);
            return comment;
        }

        #endregion

        #region Employees

        /// <summary>
        /// Список сотрудников
        /// https://help.megaplan.ru/API_employee_list
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public async Task<List<Employee>> Employees(EmployeesQueryParams queryParams = null)
        {
            var baseQuery = "/BumsStaffApiV01/Employee/list.api";
            HttpWebResponse response;
            if (queryParams == null)
            {
                response = await GetRequest(baseQuery, true);
            }
            else
            {
                response = await PostRequest(baseQuery, new QueryBuider(queryParams).BuildPostData(), true);
            }
            var content = response.Content();

            var jData = ParseResponse(content);
            var data = jData["employees"].ToString();
            var comment = JsonConvert.DeserializeObject<List<Models.Employee>>(data);
            return comment;
//            var baseQuery = "/BumsStaffApiV01/Employee/list.api";
//            if (queryParams != null)
//            {
//                baseQuery += new QueryBuider(queryParams).Build();
//            }
//
//            var response = await GetRequest(baseQuery, true);
//
//            var content = response.Content();
//
//            var jData = ParseResponse(content);
//            var data = jData["employees"].ToString();
//            var employees = JsonConvert.DeserializeObject<List<Models.Employee>>(data);
//
//            return employees;
        }

        #endregion


        public async Task<DateTime> ServerTime()
        {
            var response = await GetRequest("/BumsCommonApiV01/System/datetime.api", false);

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
                response = await PostRequest(string.Format("/BumsCommonApiV01/User/authorize.api?Login={0}&Password={1}", login, passHash), false);
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
            //                userId = data.UserId;
            //                employeeId = data.EmployeeId;

            IsAuthorized = true;
        }

        private JToken ParseResponse(string response)
        {
            var jObject = JObject.Parse(response);
            if ((string)jObject["status"]["code"] != "ok")
            {
                throw new HttpRequestException((string)jObject["status"]["error"]);
            }
#if DEBUG
            Debug.WriteLine("data {0}", jObject["data"].ToString());
#endif
            return jObject["data"];
        }

        #region Helpers

        [DebuggerStepThrough]
        public Task<HttpWebResponse> CreateRequest(string method, string subUrl,  byte[]postData, bool signRequest)
        {
#if DEBUG
            Debug.WriteLine("{0}:\t{1} | {2}", method, subUrl, signRequest);
#endif
            Request request;
            if (signRequest)
            {
                request = new Request(method, host, subUrl, postData,accessId, secretKey);
            }
            else
            {
                request = new Request(method, host, subUrl, postData);
            }

            return request.GetResponse();
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
        public Task<HttpWebResponse> PostRequest(string subUrl, byte[]postData, bool signRequest)
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