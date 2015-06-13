namespace Megaplan.API.Queries
{
    using Megaplan.API.Enums;

    public class TasksQueryParams
    {
        /// <summary>
        /// Если true, то будут выводиться только незавершенные задачи
        /// Допустимые значения: true, false
        /// По умолчанию: false
        /// </summary>
        public bool? OnlyActual { get; set; }

        /// <summary>
        /// Сколько выбрать задач (LIMIT)
        /// Целочисленное значение в диапазоне [1,100]
        /// По умолчанию: 50
        /// </summary>
        public int? Limit { get; set; }

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
        /// Папка
        ///По умолчанию: all
        /// </summary>
        public FolderType? Folder { get; set; }
        
        /// <summary>
        /// Статус
        ///По умолчанию: any
        /// </summary>
        public StatusType? Status { get; set; }

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