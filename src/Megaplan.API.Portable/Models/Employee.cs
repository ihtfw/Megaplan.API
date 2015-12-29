using System;
using Megaplan.API.Enums;

namespace Megaplan.API.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// https://help.megaplan.ru/API_employee_card
    /// </summary>
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
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Пол
        /// </summary>
        public GenderType? Gender { get; set; }

        /// <summary>
        /// Должность
        /// </summary>
        public Position Position { get; set; }

        /// <summary>
        /// Отдел
        /// </summary>
        public Department Department { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// Скрывать дату рождения
        /// </summary>
        public bool HideMyBirthday { get; set; }

        /// <summary>
        /// Возраст
        /// </summary>
        public int? Age { get; set; }

        /// <summary>
        /// Телефоны
        /// </summary>
        public List<Phone> Phones { get; set; }

        /// <summary>
        /// Электропочта
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Icq
        /// </summary>
        public string Icq { get; set; }

        /// <summary>
        /// Skype
        /// </summary>
        public string Skype { get; set; }

        /// <summary>
        /// Jabber
        /// </summary>
        public string Jabber { get; set; }


        //Address	object (Id, City, Street, House)	Адрес

        /// <summary>
        /// График работы
        /// </summary>
        public string Behaviour { get; set; }

        /// <summary>
        /// ИНН
        /// </summary>
        public string Inn { get; set; }

        /// <summary>
        /// Паспортные данные
        /// </summary>
        public string PassportData { get; set; }

        /// <summary>
        /// О себе
        /// </summary>
        public string AboutMe { get; set; }

        /// <summary>
        /// Начальники
        /// </summary>
        public List<Employee> ChiefsWithoutMe { get; set; }

        /// <summary>
        /// Подчиненные
        /// </summary>
        public List<Employee> SubordinatesWithoutMe { get; set; }

        /// <summary>
        /// Координаторы
        /// </summary>
        public List<Employee> Coordinators { get; set; }

        /// <summary>
        /// Статус
        /// </summary>
        public Status Status { get; set; }

        /// <summary>
        /// Дата принятия на работу
        /// </summary>
        public DateTime? AppearanceDay { get; set; }

        /// <summary>
        /// Дата увольнения
        /// </summary>
        public DateTime? FireDay { get; set; }

        /// <summary>
        /// Время создания сотрудника
        /// </summary>
        public DateTime? TimeCreated { get; set; }

        /// <summary>
        /// Время обновления
        /// </summary>
        public DateTime? TimeUpdated { get; set; }

        /// <summary>
        /// Относительный URL аватара сотрудника
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// Адрес большого фото сотрудника
        /// </summary>
        public string Photo { get; set; }

        /// <summary>
        /// Логин сотрудника
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Дата и время последнего обращения пользователя к системе
        /// </summary>
        public DateTime? LastOnline { get; set; }

        /// <summary>
        /// Сейчас в Мегаплане
        /// </summary>
        public bool IsOnline { get; set; }

        /// <summary>
        /// Количество непрочитанных комментариев
        /// </summary>
        public int? UnreadCommentsCount { get; set; }
    }
}