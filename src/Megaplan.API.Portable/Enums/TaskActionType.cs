namespace Megaplan.API.Enums
{
    using System.Runtime.Serialization;

    public enum TaskActionType
    {
        /// <summary>
        /// исполнитель принимает задачу
        /// </summary>
        [EnumMember(Value = @"act_accept_task")] AcceptTask,

        /// <summary>
        /// исполнитель отклоняет задачу
        /// </summary>
        [EnumMember(Value = @"act_reject_task")] RejectTask,

        /// <summary>
        /// постановщик принимает выполненную задачу
        /// </summary>
        [EnumMember(Value = @"act_accept_work")] AcceptWork,

        /// <summary>
        /// постановщик отклоняет выполненную задачу
        /// </summary>
        [EnumMember(Value = @"act_reject_work")] RejectWork,

        /// <summary>
        /// исполнитель заканчивает работу, задача условно завершена
        /// </summary>
        [EnumMember(Value = @"act_done")] Done,

        /// <summary>
        /// временно приостановить выполнение задачи
        /// </summary>
        [EnumMember(Value = @"act_pause")] Pause,

        /// <summary>
        /// продолжить выполнение приостановленной задачи
        /// </summary>
        [EnumMember(Value = @"act_resume")] Resume,

        /// <summary>
        /// отменить задачу
        /// </summary>
        [EnumMember(Value = @"act_cancel")] Cancel,

        /// <summary>
        /// провалить задачу
        /// </summary>
        [EnumMember(Value = @"act_expire")] Expire,

        /// <summary>
        ///  принять ранее отклоненную задачу
        /// </summary>
        [EnumMember(Value = @"act_renew")] Renew,

        /// <summary>
        /// удалить задачу (доступно с версии 2011.03)
        /// </summary>
        [EnumMember(Value = @"act_delete")] Delete,

        /// <summary>
        ///  редактировать задачу (доступно с версии 2011.03)
        /// </summary>
        [EnumMember(Value = @"act_edit")] Edit,

        /// <summary>
        /// преобразовать задачу в проект
        /// </summary>
        [EnumMember(Value = @"act_convert")] Convert,

        /// <summary>
        /// создать подзадачу
        /// </summary>
        [EnumMember(Value = @"act_subtask")] Subtask,

        /// <summary>
        /// делегировать задачу
        /// </summary>
        [EnumMember(Value = @"act_delegate")] Delegate,

        /// <summary>
        /// изменение соисполнителей и аудиторов
        /// </summary>
        [EnumMember(Value = @"act_edit_executors")] EditExecutors
    }
}