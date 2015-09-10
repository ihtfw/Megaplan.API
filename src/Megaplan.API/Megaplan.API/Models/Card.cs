using Megaplan.API.Enums;

namespace Megaplan.API.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Карточка задачи
    /// </summary>
    public class Card : BaseNamedModel
    {
        /// <summary>
        /// Суть задачи
        /// </summary>
        public string Statement { get; set; }
        
        /// <summary>
        /// Старт задачи
        /// </summary>
        public DateTime? Start { get; set; }

        /// <summary>
        /// Время фактического завершения задачи
        /// </summary>
        public DateTime? Finish { get; set; }

        /// <summary>
        /// Соисполнители (сотрудники)
        /// </summary>
        public List<Employee> Executors { get; set; }

        /// <summary>
        /// Аудиторы (сотрудники)
        /// </summary>
        public List<Employee> Auditors { get; set; }

        /// <summary>
        /// Заказчик
        /// </summary>
        public Customer Customer { get; set; }

        /// <summary>
        /// еотвеченный запрос на перенос дедлайна
        /// </summary>
        public DeadlineChange DeadlineChange { get; set; }



        public StatusType Status { get; set; }

        /// <summary>
        /// Файлы, прикрепленные 
        /// </summary>
        public List<Attach> Attaches { get; set; }
    }
}