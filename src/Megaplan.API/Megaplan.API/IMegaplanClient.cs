using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Megaplan.API.Exceptions;
using Megaplan.API.Models;
using Megaplan.API.Queries;
using Task = Megaplan.API.Models.Task;

namespace Megaplan.API
{
    public interface IMegaplanClient
    {
        bool IsAuthorized { get; }

        /// <summary>
        /// Карточка сотрудника
        /// https://help.megaplan.ru/API_employee_card
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Card> Card(int id);

        /// <summary>
        /// Создание задачи
        /// https://help.megaplan.ru/API_task_create
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        Task<Models.Task> AddTask(AddTaskQueryParams queryParams);

        /// <summary>
        /// Список задач
        /// https://help.megaplan.ru/API_task_list
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        Task<List<Task>> Tasks(TasksQueryParams queryParams = null);

        /// <summary>
        /// Список комментариев по задаче/проекту
        ///https://help.megaplan.ru/API_comment_list
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        Task<List<Comment>> Comments(CommentsQueryParams queryParams);

        Task<List<Comment>> UnreadComments(AllCommentsQueryParams queryParams);

        /// <summary>
        /// Список всех комментариев по актуальным задачам и проектам
        /// https://help.megaplan.ru/API_comment_all
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        Task<List<Comment>> Comments(AllCommentsQueryParams queryParams);

        /// <summary>
        /// Отметить комментарий как прочитанный
        /// https://help.megaplan.ru/API_comment_mark_as_read
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        System.Threading.Tasks.Task MarkCommentAsRead(int id);

        /// <summary>
        /// Создание комментария
        /// https://help.megaplan.ru/API_comment_create
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        Task<Comment> AddComment(AddCommentQueryParams queryParams);

        /// <summary>
        /// Список сотрудников
        /// https://help.megaplan.ru/API_employee_list
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        Task<List<Employee>> Employees(EmployeesQueryParams queryParams = null);

        /// <summary>
        /// Список клиентов
        /// https://help.megaplan.ru/API_contractor_list
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        Task<List<Client>> Clients(ClientsQueryParams queryParams = null);

        Task<DateTime> ServerTime();

        /// <exception cref="AuthorizeException"></exception>
        System.Threading.Tasks.Task Authorize(string login, string pass);

        /// <summary>
        /// Карточка сотрудника
        /// https://help.megaplan.ru/API_employee_card
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        Task<Employee> EmployeeCard(int id);
    }
}