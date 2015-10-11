namespace Megaplan.API.Queries
{
    using System;

    using Megaplan.API.Enums;

    /// <summary>
    /// ������ ������������ �� ������/�������
    /// </summary>
    public class CommentsQueryParams
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
        /// ���������� ������ �� �������, ������� ���� �������� ����� ��������� ����
        /// ����/����� � ����� �� �������� ISO 8601
        /// </summary>
        public DateTime? TimeUpdated { get; set; }

        /// <summary>
        /// ����������� ���������� �� ���� (�� ��������� asc)
        /// </summary>
        public SortOrderType? Order { get; set; }

        /// <summary>
        /// ���������� �� ����������� � Html ������� (�� ��������� false)
        /// </summary>
        public bool? TextHtml { get; set; }

        /// <summary>
        /// ���������� ������ ������������� ����������� ���� true, �� ��������� false
        /// </summary>
        public bool? UnreadOnly { get; set; }

        /// <summary>
        /// ������� ������� ������������ (LIMIT)
        /// </summary>
        public int? Limit { get; set; }

        /// <summary>
        /// ������� � ������ �������� ����������� (OFFSET)
        /// </summary>
        public int? Offset { get; set; }

        public static CommentsQueryParams Task(int id)
        {
            return new CommentsQueryParams
                   {
                       SubjectType = SubjectType.Task,
                       SubjectId = id,
                       Order = SortOrderType.Desc
                   };
        }
    }
}