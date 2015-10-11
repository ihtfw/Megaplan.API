namespace Megaplan.API.Enums
{
    public enum FolderType
    {
        All,
        /// <summary>
        /// входящие
        /// </summary>
        Incoming,
        /// <summary>
        /// ответственный
        /// </summary>
        Responsible,
        /// <summary>
        /// соисполнитель
        /// </summary>
        Executor,
        /// <summary>
        /// исходящие
        /// </summary>
        Owner,
        /// <summary>
        /// аудируемые
        /// </summary>
        Auditor
    }
}