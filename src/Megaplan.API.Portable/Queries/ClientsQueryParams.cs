using Newtonsoft.Json;

namespace Megaplan.API.Queries
{
    /// <summary>
    /// https://help.megaplan.ru/API_contractor_list
    /// </summary>
    public class ClientsQueryParams
    {
        /// <summary>
        /// Идентификатор фильтра
        /// </summary>
        public int? FilterId { get; set; }

        /// <summary>
        /// Сколько выбрать клиентов (LIMIT) Выборка происходит с начала от меньших ID к большим
        /// </summary>
        public int? Limit { get; set; }

        /// <summary>
        /// Начиная с какого выбирать клиента (OFFSET)
        /// </summary>
        public int? Offset { get; set; }

        /// <summary>
        /// Условие поиска
        /// </summary>
        [JsonProperty("qs")]
        public string Filter { get; set; }

        /// <summary>
        /// Номер телефона в произвольном формате
        /// </summary>
        public string Phone { get; set; }

        public static ClientsQueryParams WithFilter(string filter)
        {
            return new ClientsQueryParams
            {
                Filter = filter
            };
        }
    }
}