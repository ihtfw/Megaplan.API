namespace Megaplan.API.Queries
{
    using System;
    using System.Collections.Generic;

    using Megaplan.API.Attributes;
    using Megaplan.API.Enums;

    using Newtonsoft.Json;

    public class AddCommentQueryParams
    {
        public AddCommentQueryParams()
        {
            Attaches = new List<Attachment>();
        }

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


        [Array("Model[Attaches][%]")]
        public List<Attachment> Attaches { get; set; }
        
#if !PCL
        public AddCommentQueryParams AttachFile(string path)
        {
            Attaches.Add(new Attachment(path));
            return this;
        }
#endif

        public AddCommentQueryParams AttachFile(string name, byte[] content)
        {
            Attaches.Add(new Attachment(name, content));
            return this;
        }

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