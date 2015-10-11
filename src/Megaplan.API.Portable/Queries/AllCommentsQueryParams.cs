namespace Megaplan.API.Queries
{
    /// <summary>
    /// Список всех комментариев по актуальным задачам и проектам
    /// </summary>
    public class AllCommentsQueryParams
    {
        /// <summary>
        /// Если true, то будут выводиться комментарии только незавершенных задач или проектов
        /// </summary>
        public bool OnlyActual { get; set; }

        public static AllCommentsQueryParams Actual()
        {
            return new AllCommentsQueryParams()
                   {
                       OnlyActual = true
                   };
        }
    }
}