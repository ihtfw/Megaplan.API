namespace Megaplan.API.Queries
{
    using System.Collections.Generic;

    /// <summary>
    /// Отметить комментарий как прочитанный
    /// </summary>
    public class MarkCommentAsReadQueryParams
    {
        /// <summary>
        /// Код комментария
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Список кодов комментариев
        /// </summary>
        public List<int> IdList { get; set; }

        public static MarkCommentAsReadQueryParams Create(int id)
        {
            return new MarkCommentAsReadQueryParams
                   {
                       Id = id
                   };
        }
    }
}