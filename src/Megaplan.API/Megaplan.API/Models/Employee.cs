using System;

namespace Megaplan.API.Models
{
    public class Employee : BaseNamedModel
    {
        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Должность
        /// </summary>
        public Position Position { get; set; }

        /// <summary>
        /// Отдел
        /// </summary>
        public Department Department { get; set; }

        /// <summary>
        /// Телефоны
        /// </summary>
        public Phone[] Phones { get; set; }

        /// <summary>
        /// Электропочта
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Статус
        /// </summary>
        public Status Status { get; set; }

        /// <summary>
        /// Время создания сотрудника
        /// </summary>
        public DateTime? TimeCreated { get; set; }
        
        /// <summary>
        /// Дата увольнения
        /// </summary>
        public DateTime? FireDay { get; set; }

        /// <summary>
        /// Относительный URL аватара сотрудника
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// Логин сотрудника
        /// </summary>
        public string Login { get; set; }
    }
}