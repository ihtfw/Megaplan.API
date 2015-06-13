namespace Megaplan.API.Queries
{
    using System;

    using Megaplan.API.Enums;

    using Newtonsoft.Json;

    public class AddCommentQueryParams
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
        /// Текст комментария
        /// </summary>
        [JsonProperty("Model[Text]")]
        public string Text { get; set; }

        /// <summary>
        /// Кол-во потраченных часов, которое приплюсуется к комментируемому объекту (задача или проект)
        /// </summary>
        [JsonProperty("Model[Work]")]
        public int? Work { get; set; }

        /// <summary>
        /// Дата, на которую списаны потраченные часы
        /// </summary>
        [JsonProperty("Model[WorkDate]")]
        public DateTime? WorkDate { get; set; }        

        public static AddCommentQueryParams Task(int id, string text)
        {
            return new AddCommentQueryParams
                   {
                       SubjectType = SubjectType.Task,
                       SubjectId = id,
                       Text = text
                   };
        }
    }
}