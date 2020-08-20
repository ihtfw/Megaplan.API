using System;
using Megaplan.API.Attributes;
using Megaplan.API.Enums;
using Megaplan.API.Models;
using Newtonsoft.Json;

namespace Megaplan.API.Queries
{
    using System.Collections.Generic;

    /// <summary>
    /// Параметры для запроса на создание задачи
    /// https://help.megaplan.ru/API_task_create
    /// </summary>
    public class AddTaskQueryParams
    {
        public AddTaskQueryParams()
        {
            Attaches = new List<Attachment>();
        }

        /// <summary>
        /// Название
        /// Обязательное поле
        /// </summary>
        [JsonProperty("Model[Name]")]
        public string Name { get; set; }

        /// <summary>
        /// Дедлайн (дата со временем)
        /// </summary>
        [JsonProperty("Model[Deadline]")]
        public DateTime? Deadline { get; set; }

        /// <summary>
        /// Дедлайн (только дата)
        /// </summary>
        [JsonProperty("Model[DeadlineDate]")]
        public DateTime? DeadlineDate { get; set; }

        /// <summary>
        /// Тип дедлайна
        /// </summary>
        [JsonProperty("Model[DeadlineDate]")]
        public DeadlineType? DeadlineType { get; set; }

        /// <summary>
        /// Код ответственного (EmployeeId)
        /// Обязательное поле для не массовой задачи
        /// </summary>
        [JsonProperty("Model[Responsible]")]
        public int Responsible { get; set; }

        /// <summary>
        /// Коды соисполнителей
        /// Обязательное поле для массовой задачи
        /// </summary>
        [JsonProperty("Model[Executors]")]
        public List<int> Executors { get; set; }

        /// <summary>
        /// Коды аудиторов
        /// </summary>
        [JsonProperty("Model[Auditors]")]
        public List<int> Auditors { get; set; }

        /// <summary>
        /// Код важности
        /// С версии 2011.3 допустимо отсутствие параметра важности или важность с MasterType=high
        /// </summary>
        [JsonProperty("Model[Severity]")]
        public int? Severity { get; set; }

        /// <summary>
        /// Код надзадачи (если число) или код проекта (если строка в формате 'pКод_проекта')
        /// </summary>
        [JsonProperty("Model[SuperTask]")]
        public string SuperTask { get; set; }

        /// <summary>
        /// Код заказчика
        /// </summary>
        [JsonProperty("Model[Customer]")]
        public int? Customer { get; set; }

        /// <summary>
        /// Массовая задача (каждому соисполнителю будет создана своя задача)
        /// </summary>
        [BuildBoolAsInt]
        [JsonProperty("Model[IsGroup]")]
        public bool IsGroup { get; set; }

        /// <summary>
        /// Суть задачи
        /// </summary>
        [JsonProperty("Model[Statement]")]
        public string Statement { get; set; }

        [Array("Model[Attaches][Add][%]")]
        public List<Attachment> Attaches { get; set; }


            /*
         * Model[Attaches][Add]	array	Массив приложенных файлов	Должен передаваться POST-запросом
         * Model[Attaches][Add][0...n][Content]	string	Данные (контент) файла, закодированные с использованием MIME base64	В устаревших версиях Мегаплана может действовать имя параметра Model[Attaches][Add][][Context]
         * Model[Attaches][Add][0...n][Name]	string	Имя файла	Будет фигурировать при выводе задачи
         */

        /// <summary>
        /// Планирование: старт
        /// Дата со временем
        /// </summary>
        [JsonProperty("Model[Start]")]
        public DateTime? Start { get; set; }

        /// <summary>
        /// Планирование: финиш
        /// Только дата. При указанном Model[PlannedTime] расчитывается автоматически
        /// </summary>
        [JsonProperty("Model[PlannedFinish]")]
        public DateTime? PlannedFinish { get; set; }

        /// <summary>
        /// Планирование: длительность (в днях)
        /// При указанном Model[PlannedFinish] расчитывается автоматически
        /// </summary>
        [JsonProperty("Model[PlannedTime]")]
        public int? PlannedTime { get; set; }

        /// <summary>
        /// Планирование: плановые трудозатраты (в минутах)
        /// </summary>
        [JsonProperty("Model[PlannedWork]")]
        public int? PlannedWork { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">Название</param>
        /// <param name="statement">Суть задачи</param>
        /// <param name="responsibleId">Id ответсвенного</param>
        /// <returns></returns>
        public static AddTaskQueryParams Simple(string name, string statement, int responsibleId)
        {
            return new AddTaskQueryParams
            {
                Name = name,
                Statement = statement,
                Responsible = responsibleId
            };
        }

#if !PCL
        public AddTaskQueryParams AttachFile(string path)
        {
            Attaches.Add(new Attachment(path));
            return this;
        }
#endif
        public AddTaskQueryParams AttachFile(string name, byte[] content)
        {
            Attaches.Add(new Attachment(name, content));
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">Название</param>
        /// <param name="statement">Суть задачи</param>
        /// <param name="responsibleId">Id ответсвенного</param>
        /// <param name="customerId">Id клиента</param>
        /// <returns></returns>
        public static AddTaskQueryParams FromCustomer(string name, string statement, int responsibleId, int customerId)
        {
            var p = Simple(name, statement, responsibleId);
            p.Customer = customerId;
            return p;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">Название</param>
        /// <param name="statement">Суть задачи</param>
        /// <param name="responsible">Ответсвенный</param>
        /// <returns></returns>
        public static AddTaskQueryParams Simple(string name, string statement, Employee responsible)
        {
            return Simple(name, statement, responsible.Id);
        }
    }
}