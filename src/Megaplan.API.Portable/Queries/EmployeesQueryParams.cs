using Megaplan.API.Attributes;
using Megaplan.API.Enums;

namespace Megaplan.API.Queries
{
    public class EmployeesQueryParams
    {
        /// <summary>
        /// Идентификатор отдела
        /// Если передать значение -1, то будут возвращены сотрудники, не привязанные ни к какому отделу.
        /// Если отдела с указанным Id не существует, то будет возвращена ошибка "Department not found"
        /// </summary>
        public int? Department { get; set; }

        /// <summary>
        /// Параметр для сортировки
        /// </summary>
        public EmployeeOrderByType? OrderBy { get; set; }

        /// <summary>
        /// Фильтрация по части имени сотрудника
        /// </summary>
        [BuildWithoutToLower]
        public string Name { get; set; }

        public static EmployeesQueryParams FilterByName(string name)
        {
            return new EmployeesQueryParams
            {
                Name = name
            };
        }
    }
}