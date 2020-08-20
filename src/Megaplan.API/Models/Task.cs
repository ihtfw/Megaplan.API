using System;
using System.Collections.Generic;

namespace Megaplan.API.Models
{
    using Megaplan.API.Enums;

    using Newtonsoft.Json;

    public class Task : BaseNamedModel
    {
        /// <summary>
        /// Статус
        /// </summary>
        public StatusType Status { get; set; }

        /// <summary>
        /// Дата запланированного финиша
        /// </summary>
        public DateTime? PlannedFinish { get; set; }

        /// <summary>
        /// Количество запланированной работы в минутах
        /// </summary>
        public int PlannedWork { get; set; }

        /// <summary>
        /// Отработанное время в минутах
        /// </summary>
        public int ActualWork { get; set; }

        /// <summary>
        /// Аналогично ActualWork, но с учетом отработанного времени во всех подзадачах
        /// </summary>
        public int ActualWorkWithSubTasks { get; set; }

        /// <summary>
        /// Процент завершения задачи
        /// </summary>
        public int Completed { get; set; }

        /// <summary>
        /// Дедлайн
        /// </summary>
        public DateTime? Deadline { get; set; }

        /// <summary>
        /// Постановщик (сотрудник)
        /// </summary>
        public Employee Owner { get; set; }

        /// <summary>
        /// Ответственный (сотрудник)
        /// </summary>
        public Employee Responsible { get; set; }

        /// <summary>
        /// Важность
        /// </summary>
        public string Severity { get; set; }
        
        /// <summary>
        /// Надзадача
        /// </summary>
        public Task SuperTask { get; set; }

        /// <summary>
        /// Проект
        /// </summary>
        public Project Project { get; set; }
        
        /// <summary>
        /// В избранном
        /// </summary>
        [JsonConverter(typeof(BoolToIntConverter))]
        public bool Favorite { get; set; }

        /// <summary>
        /// Время создания
        /// </summary>
        public DateTime TimeCreated { get; set; }

        /// <summary>
        /// Время последней модификации
        /// </summary>
        public DateTime TimeUpdated { get; set; }

        /// <summary>
        /// Список папок, в которые попадает задача
        /// </summary>
        public List<string> Folders { get; set; } 

        /// <summary>
        /// Тэги, привязанные к задаче
        /// </summary>
        public List<Tag> Tags { get; set; }

        /// <summary>
        /// Дата и время последней активности по задаче
        /// </summary>
        public DateTime? Activity { get; set; }

        /// <summary>
        /// Список доступных действий над задачей
        /// </summary>
        public List<TaskActionType> Actions { get; set; }

        /// <summary>
        /// Является ли задача просроченной
        /// </summary>
        public bool IsOverdue { get; set; }

        /// <summary>
        /// Количество непрочитанных комментариев
        /// </summary>
        public int СommentsUnread { get; set; }
    }
}