namespace Megaplan.API.Models
{
    using System;
    using Newtonsoft.Json;

    public class TaskQueryParams
    {
       

        /// <summary>
        /// Возвращать только те объекты, которые были изменены после указанный даты
        /// Дата/время в одном из форматов ISO 8601
        /// </summary>
        public DateTime TimeUpdated { get; set; }


        /// <summary>
        /// Только избранное
        /// Допустимые значения: 0, 1
        /// По умолчанию: 0
        /// </summary>
        [JsonConverter(typeof(BoolToIntConverter))]
        public bool FavoritesOnly { get; set; }

        /// <summary>
        /// Строка поиска
        /// </summary>
        public string Search { get; set; }

        /// <summary>
        /// Нужно ли показывать в списке задач все поля из карточки задачи
        /// Допустимые значения: true, false 
        /// По умолчанию: false
        /// </summary>
        public bool Detailed { get; set; }

        

        /// <summary>
        /// Код фильтра
        /// Допустимые значения: любая строка
        /// (может быть как числом, так и строковым идентификатором)
        /// </summary>
        public string FilterId { get; set; }

        /// <summary>
        /// Если передан этот параметр со значением true, то вместо списка будет выводиться только количество задач, удовлетворяющих условиям
        /// Допустимые значения: true, false
        /// По умолчанию: false
        /// </summary>
        public bool Count { get; set; }

        /// <summary>
        /// Код сотрудника, для которого нужно загрузить задачи
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// Возвращает только задачи, входящие в проект ProjectId
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// Возвращает только задачи, входящие в надзадачу SuperTaskId
        /// </summary>
        public int SuperTaskId { get; set; }


        /// <summary>
        /// Нужно ли показывать в списке возможные действия над задачей
        /// Допустимые значения: true, false
        /// По умолчанию: false
        /// </summary>
        public bool ShowActions { get; set; }

        /// <summary>
        /// Начиная с какой выбирать задачи (OFFSET)
        /// </summary>
        public int Offset { get; set; }

        
    }
}