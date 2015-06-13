namespace Megaplan.API.Models
{
    using System;

    public class DeadlineChange : BaseModel
    {
        /// <summary>
        /// Объяснение запроса
        /// </summary>
        public string Request { get; set; }

        /// <summary>
        /// Дата-время нового дедлайна
        /// </summary>
        public DateTime? DateTime { get; set; }
    }
}