namespace Megaplan.API.Queries
{
    using System;

    using Megaplan.API.Attributes;
    using Megaplan.API.Enums;

    public class TasksQueryParams
    {
        /// <summary>
        /// Папка
        ///По умолчанию: all
        /// </summary>
        public FolderType? Folder { get; set; }

        /// <summary>
        /// Возвращать только те объекты, которые были изменены после указанный даты
        /// Дата/время в одном из форматов ISO 8601
        /// </summary>
        public DateTime? TimeUpdated { get; set; }

        /// <summary>
        /// Статус
        ///По умолчанию: any
        /// </summary>
        public StatusType? Status { get; set; }

        /// <summary>
        /// Только избранное
        /// </summary>
        [BuildBoolAsInt]
        public bool? FavoritesOnly { get; set; }

        /// <summary>
        /// Строка поиска
        /// </summary>
        public string Search { get; set; }

        /// <summary>
        /// Нужно ли показывать в списке задач все поля из карточки задачи
        /// </summary>
        public bool? Detailed { get; set; }

        /// <summary>
        /// Если true, то будут выводиться только незавершенные задачи
        /// Допустимые значения: true, false
        /// По умолчанию: false
        /// </summary>
        public bool? OnlyActual { get; set; }

        /// <summary>
        /// Код фильтра
        /// Допустимые значения: любая строка (может быть как числом, так и строковым идентификатором)
        /// </summary>
        public string FilterId { get; set; }

        /// <summary>
        /// Если передан этот параметр со значением true, то вместо списка будет выводиться только количество задач, удовлетворяющих условиям
        /// </summary>
        public bool? Count { get; set; }

        /// <summary>
        /// Код сотрудника, для которого нужно загрузить задачи
        /// </summary>
        public int? EmployeeId { get; set; }

        /// <summary>
        /// Возвращает только задачи, входящие в проект ProjectId
        /// </summary>
        public int? ProjectId { get; set; }

        /// <summary>
        /// Возвращает только задачи, входящие в надзадачу SuperTaskId
        /// </summary>
        public int? SuperTaskId { get; set; }

        /// <summary>
        /// Сортировка результата
        /// </summary>
        public SortByType? SortBy { get; set; }

        /// <summary>
        /// Направление сортировки
        /// По умолчанию: asc
        /// </summary>
        public SortOrderType? SortOrder { get; set; }

        /// <summary>
        /// Нужно ли показывать в списке возможные действия над задачей
        /// </summary>
        public bool? ShowActions { get; set; }
        
        /// <summary>
        /// Сколько выбрать задач (LIMIT)
        /// Целочисленное значение в диапазоне [1,100]
        /// По умолчанию: 50
        /// </summary>
        public int? Limit { get; set; }

        /// <summary>
        /// Начиная с какой выбирать задачи (OFFSET)
        /// </summary>
        public int? Offset { get; set; }

        public static TasksQueryParams BaseParams()
        {
            return new TasksQueryParams
            {
                Limit = 100,
                OnlyActual = true,
                Status = StatusType.Actual,
                SortBy = SortByType.Activity,
                SortOrder = SortOrderType.Desc
            };
        }
        public static TasksQueryParams All()
        {
            var baseParams = BaseParams();
            return baseParams;
        }

        public static TasksQueryParams Responsible()
        {
            var baseParams = BaseParams();
            baseParams.Folder = FolderType.Responsible;
            return baseParams;
        }

        /// <summary>
        /// Соисполнитель
        /// </summary>
        /// <returns></returns>
        public static TasksQueryParams Executor()
        {
            var baseParams = BaseParams();
            baseParams.Folder = FolderType.Executor;
            return baseParams;
        }

        public static TasksQueryParams Auditor()
        {
            var baseParams = BaseParams();
            baseParams.Folder = FolderType.Auditor;
            return baseParams;
        }
    }
}