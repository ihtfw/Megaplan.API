using Newtonsoft.Json;

namespace Megaplan.API.Queries
{
    /// <summary>
    /// https://help.megaplan.ru/API_contractor_list
    /// </summary>
    public class ClientsQueryParams
    {
        /// <summary>
        /// ������������� �������
        /// </summary>
        public int? FilterId { get; set; }

        /// <summary>
        /// ������� ������� �������� (LIMIT) ������� ���������� � ������ �� ������� ID � �������
        /// </summary>
        public int? Limit { get; set; }

        /// <summary>
        /// ������� � ������ �������� ������� (OFFSET)
        /// </summary>
        public int? Offset { get; set; }

        /// <summary>
        /// ������� ������
        /// </summary>
        [JsonProperty("qs")]
        public string Filter { get; set; }

        /// <summary>
        /// ����� �������� � ������������ �������
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