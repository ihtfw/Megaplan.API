using Megaplan.API.Attributes;
using Megaplan.API.Enums;

namespace Megaplan.API.Queries
{
    public class EmployeesQueryParams
    {
        /// <summary>
        /// ������������� ������
        /// ���� �������� �������� -1, �� ����� ���������� ����������, �� ����������� �� � ������ ������.
        /// ���� ������ � ��������� Id �� ����������, �� ����� ���������� ������ "Department not found"
        /// </summary>
        public int? Department { get; set; }

        /// <summary>
        /// �������� ��� ����������
        /// </summary>
        public EmployeeOrderByType? OrderBy { get; set; }

        /// <summary>
        /// ���������� �� ����� ����� ����������
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