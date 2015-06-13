using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Megaplan.API.Models
{
    using Megaplan.API.Enums;

    public  class Comment : BaseModel
    {
        /// <summary>
        /// Текст комментария
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Кол-во потраченных минут, которое приплюсовано к комментируемому объекту (задаче или проекту)
        /// </summary>
        public int Work { get; set; }

        /// <summary>
        /// Дата, на которую списаны потраченные часы
        /// </summary>
        public DateTime? WorkDate { get; set; }

        /// <summary>
        /// Время создания
        /// </summary>
        public DateTime TimeCreated { get; set; }

        /// <summary>
        /// Автор комментария (сотрудник)
        /// </summary>
        public Employee Author { get; set; }

        /// <summary>
        /// Адрес аватара автора
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// Файлы, прикрепленные к комментарию
        /// </summary>
        public List<Attach> Attaches { get; set; }

        /// <summary>
        /// Является ли комментарий непрочитанным
        /// </summary>
        public bool IsUnread { get; set; }

        /// <summary>
        /// Находится ли комментарий в избранном
        /// </summary>
        public bool IsFavorite { get; set; }

        /// <summary>
        /// Тип объекта, к которому привязан комментарий: задача (task) или проект (project)
        /// </summary>
        public SubjectType? SubjectType { get; set; }

        /// <summary>
        /// Код объекта, к которому привязан комментарий
        /// </summary>
        public int? SubjectId { get; set; }

        #region Equals

        protected bool Equals(Comment other)
        {
            return base.Equals(other) && string.Equals(Text, other.Text) && Work == other.Work && WorkDate.Equals(other.WorkDate) && TimeCreated.Equals(other.TimeCreated) && Equals(Author, other.Author) && string.Equals(Avatar, other.Avatar) && IsUnread.Equals(other.IsUnread) && IsFavorite.Equals(other.IsFavorite);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            return Equals((Comment)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = base.GetHashCode();
                hashCode = (hashCode * 397) ^ (Text != null ? Text.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Work;
                hashCode = (hashCode * 397) ^ WorkDate.GetHashCode();
                hashCode = (hashCode * 397) ^ TimeCreated.GetHashCode();
                hashCode = (hashCode * 397) ^ (Author != null ? Author.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Avatar != null ? Avatar.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ IsUnread.GetHashCode();
                hashCode = (hashCode * 397) ^ IsFavorite.GetHashCode();
                return hashCode;
            }
        }

        #endregion

    }
}
