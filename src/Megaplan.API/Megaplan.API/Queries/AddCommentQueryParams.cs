namespace Megaplan.API.Queries
{
    using System;

    using Megaplan.API.Enums;

    using Newtonsoft.Json;

    public class AddCommentQueryParams
    {
        /// <summary>
        /// ��� ��������������� �������
        /// </summary>
        public SubjectType SubjectType { get; set; }
        
        /// <summary>
        /// ID ��������������� �������
        /// </summary>
        public int SubjectId { get; set; }

        /// <summary>
        /// ����� �����������
        /// </summary>
        [JsonProperty("Model[Text]")]
        public string Text { get; set; }

        /// <summary>
        /// ���-�� ����������� �����, ������� ������������ � ��������������� ������� (������ ��� ������)
        /// </summary>
        [JsonProperty("Model[Work]")]
        public int? Work { get; set; }

        /// <summary>
        /// ����, �� ������� ������� ����������� ����
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