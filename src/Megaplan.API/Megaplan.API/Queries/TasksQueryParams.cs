namespace Megaplan.API.Queries
{
    using Megaplan.API.Enums;

    public class TasksQueryParams
    {
        /// <summary>
        /// ���� true, �� ����� ���������� ������ ������������� ������
        /// ���������� ��������: true, false
        /// �� ���������: false
        /// </summary>
        public bool? OnlyActual { get; set; }

        /// <summary>
        /// ������� ������� ����� (LIMIT)
        /// ������������� �������� � ��������� [1,100]
        /// �� ���������: 50
        /// </summary>
        public int? Limit { get; set; }

        /// <summary>
        /// ���������� ����������
        /// </summary>
        public SortByType? SortBy { get; set; }

        /// <summary>
        /// ����������� ����������
        /// �� ���������: asc
        /// </summary>
        public SortOrderType? SortOrder { get; set; }

        /// <summary>
        /// �����
        ///�� ���������: all
        /// </summary>
        public FolderType? Folder { get; set; }
        
        /// <summary>
        /// ������
        ///�� ���������: any
        /// </summary>
        public StatusType? Status { get; set; }

        public static TasksQueryParams BaseParams()
        {
            return new TasksQueryParams
            {
                Limit = 100,
                OnlyActual = true,
                Status = StatusType.Actual,
                SortBy = SortByType.Activity,
                SortOrder = SortOrderType.Desc
            };
        }
        public static TasksQueryParams All()
        {
            var baseParams = BaseParams();
            return baseParams;
        }

        public static TasksQueryParams Responsible()
        {
            var baseParams = BaseParams();
            baseParams.Folder = FolderType.Responsible;
            return baseParams;
        }

        /// <summary>
        /// �������������
        /// </summary>
        /// <returns></returns>
        public static TasksQueryParams Executor()
        {
            var baseParams = BaseParams();
            baseParams.Folder = FolderType.Executor;
            return baseParams;
        }

        public static TasksQueryParams Auditor()
        {
            var baseParams = BaseParams();
            baseParams.Folder = FolderType.Auditor;
            return baseParams;
        }
    }
}