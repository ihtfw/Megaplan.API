namespace Megaplan.API.Queries
{
    using System.Collections.Generic;

    /// <summary>
    /// �������� ����������� ��� �����������
    /// </summary>
    public class MarkCommentAsReadQueryParams
    {
        /// <summary>
        /// ��� �����������
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ������ ����� ������������
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