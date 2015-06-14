using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Megaplan.API.Models
{
    public class Client : BaseNamedModel
    {
        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// Описание клиента
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// E-mail
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Facebook
        /// </summary>
        public string Facebook { get; set; }

        /// <summary>
        /// Jabber
        /// </summary>
        public string Jabber { get; set; }

        /// <summary>
        /// Список плательщиков
        /// </summary>
        public Client[] Payers { get; set; }

        /// <summary>
        /// Тип клиента
        /// </summary>
        public string PersonType { get; set; }

        /// <summary>
        /// Предпочтительный способ связи
        /// </summary>
        public string PreferTransport { get; set; }

        /// <summary>
        /// Перспективность
        /// </summary>
        public string PromisingRate { get; set; }

        /// <summary>
        /// Ответственные
        /// </summary>
        public Client[] Responsibles { get; set; }

        /// <summary>
        /// Сайт
        /// </summary>
        public string Site { get; set; }

        /// <summary>
        /// Время создания
        /// </summary>
        public DateTime? TimeCreated { get; set; }

        /// <summary>
        /// Время обновления
        /// </summary>
        public DateTime? TimeUpdated { get; set; }

        /// <summary>
        /// Twitter
        /// </summary>
        public string Twitter { get; set; }

        /// <summary>
        /// Тип
        /// </summary>
        public Type Type { get; set; }
    }
}
