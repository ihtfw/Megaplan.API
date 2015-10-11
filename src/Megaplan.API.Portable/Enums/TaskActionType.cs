namespace Megaplan.API.Enums
{
    using System.Runtime.Serialization;

    public enum TaskActionType
    {
        /// <summary>
        /// ����������� ��������� ������
        /// </summary>
        [EnumMember(Value = @"act_accept_task")] AcceptTask,

        /// <summary>
        /// ����������� ��������� ������
        /// </summary>
        [EnumMember(Value = @"act_reject_task")] RejectTask,

        /// <summary>
        /// ����������� ��������� ����������� ������
        /// </summary>
        [EnumMember(Value = @"act_accept_work")] AcceptWork,

        /// <summary>
        /// ����������� ��������� ����������� ������
        /// </summary>
        [EnumMember(Value = @"act_reject_work")] RejectWork,

        /// <summary>
        /// ����������� ����������� ������, ������ ������� ���������
        /// </summary>
        [EnumMember(Value = @"act_done")] Done,

        /// <summary>
        /// �������� ������������� ���������� ������
        /// </summary>
        [EnumMember(Value = @"act_pause")] Pause,

        /// <summary>
        /// ���������� ���������� ���������������� ������
        /// </summary>
        [EnumMember(Value = @"act_resume")] Resume,

        /// <summary>
        /// �������� ������
        /// </summary>
        [EnumMember(Value = @"act_cancel")] Cancel,

        /// <summary>
        /// ��������� ������
        /// </summary>
        [EnumMember(Value = @"act_expire")] Expire,

        /// <summary>
        ///  ������� ����� ����������� ������
        /// </summary>
        [EnumMember(Value = @"act_renew")] Renew,

        /// <summary>
        /// ������� ������ (�������� � ������ 2011.03)
        /// </summary>
        [EnumMember(Value = @"act_delete")] Delete,

        /// <summary>
        ///  ������������� ������ (�������� � ������ 2011.03)
        /// </summary>
        [EnumMember(Value = @"act_edit")] Edit,

        /// <summary>
        /// ������������� ������ � ������
        /// </summary>
        [EnumMember(Value = @"act_convert")] Convert,

        /// <summary>
        /// ������� ���������
        /// </summary>
        [EnumMember(Value = @"act_subtask")] Subtask,

        /// <summary>
        /// ������������ ������
        /// </summary>
        [EnumMember(Value = @"act_delegate")] Delegate,

        /// <summary>
        /// ��������� �������������� � ���������
        /// </summary>
        [EnumMember(Value = @"act_edit_executors")] EditExecutors
    }
}