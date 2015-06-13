namespace Megaplan.API.Enums
{
    using Newtonsoft.Json;

    public enum StatusType
    {
        Any,

        /// <summary>
        /// ����������
        /// </summary>
        Actual,

        /// <summary>
        /// � ��������
        /// </summary>
        Inprocess,

        /// <summary>
        /// �����
        /// </summary>
        New,

        /// <summary>
        /// ������������
        /// </summary>
        Overdue,

        /// <summary>
        /// ������� �����������
        /// </summary>
        Done,

        /// <summary>
        /// ����������
        /// </summary>
        Delayed,

        /// <summary>
        /// �����������
        /// </summary>
        Completed,

        /// <summary>
        /// �����������
        /// </summary>
        Failed,

        /// <summary>
        /// ����������
        /// </summary>
        Cancelled,

        /// <summary>
        /// �������
        /// </summary>
        Accepted,

        /// <summary>
        /// ����������
        /// </summary>
        Expired,

        Assigned
    }
}