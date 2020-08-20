namespace Megaplan.API.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Customer : BaseNamedModel
    {
        /// <summary>
        /// Статус
        /// </summary>
        public Status Status { get; set; }

        /// <summary>
        /// Тип
        /// </summary>
        public Type Type { get; set; }
    }
}