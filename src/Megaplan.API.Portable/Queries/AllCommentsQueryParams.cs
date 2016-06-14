namespace Megaplan.API.Queries
{
    using System;

    /// <summary>
    /// Список всех комментариев по актуальным задачам и проектам
    /// </summary>
    public class AllCommentsQueryParams
    {
        /// <summary>
        /// Если true, то будут выводиться комментарии только незавершенных задач или проектов
        /// </summary>
        public bool OnlyActual { get; set; }

        /// <summary>
        /// Возвращать только те объекты, которые были изменены после указанный даты  
        /// Дата/время в одном из форматов ISO 8601 
        /// </summary>
        public DateTime? TimeUpdated { get; set; }

        public static AllCommentsQueryParams Actual()
        {
            return new AllCommentsQueryParams()
                   {
                       OnlyActual = true
                   };
        }
    }
}