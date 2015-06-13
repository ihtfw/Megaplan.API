namespace Megaplan.API.Queries
{
    using System;

    using Megaplan.API.Enums;

    /// <summary>
    /// Список комментариев по задаче/проекту
    /// </summary>
    public class CommentsQueryParams
    {
        /// <summary>
        /// Тип комментируемого объекта
        /// </summary>
        public SubjectType SubjectType { get; set; }

        /// <summary>
        /// ID комментируемого объекта
        /// </summary>
        public int SubjectId { get; set; }

        /// <summary>
        /// Возвращать только те объекты, которые были изменены после указанный даты
        /// Дата/время в одном из форматов ISO 8601
        /// </summary>
        public DateTime? TimeUpdated { get; set; }

        /// <summary>
        /// Направление сортировки по дате (по умолчанию asc)
        /// </summary>
        public SortOrderType? Order { get; set; }

        /// <summary>
        /// Возвращать ли комментарий в Html формате (по умолчанию false)
        /// </summary>
        public bool? TextHtml { get; set; }

        /// <summary>
        /// Возвращает только непрочитанные комментарии если true, по умолчанию false
        /// </summary>
        public bool? UnreadOnly { get; set; }

        /// <summary>
        /// Сколько выбрать комментариев (LIMIT)
        /// </summary>
        public int? Limit { get; set; }

        /// <summary>
        /// Начиная с какого выбирать комментарии (OFFSET)
        /// </summary>
        public int? Offset { get; set; }

        public static CommentsQueryParams Task(int id)
        {
            return new CommentsQueryParams
                   {
                       SubjectType = SubjectType.Task,
                       SubjectId = id,
                       Order = SortOrderType.Desc
                   };
        }
    }
}