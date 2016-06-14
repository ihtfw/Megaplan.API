namespace Megaplan.API.Enums
{
    using Newtonsoft.Json;

    public enum StatusType
    {
        Any,

        /// <summary>
        /// актуальные
        /// </summary>
        Actual,

        /// <summary>
        /// в процессе
        /// </summary>
        Inprocess,

        /// <summary>
        /// новые
        /// </summary>
        New,

        /// <summary>
        /// просроченные
        /// </summary>
        Overdue,

        /// <summary>
        /// условно завершенные
        /// </summary>
        Done,

        /// <summary>
        /// отложенные
        /// </summary>
        Delayed,

        /// <summary>
        /// завершенные
        /// </summary>
        Completed,

        /// <summary>
        /// проваленные
        /// </summary>
        Failed,

        /// <summary>
        /// Отмененная
        /// </summary>
        Cancelled,

        /// <summary>
        /// Принята
        /// </summary>
        Accepted,

        /// <summary>
        /// Просрочена
        /// </summary>
        Expired,

        Assigned,
        Rejected


    }
}