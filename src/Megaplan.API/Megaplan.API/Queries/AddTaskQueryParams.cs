using System;
using Megaplan.API.Attributes;
using Megaplan.API.Enums;
using Megaplan.API.Models;
using Newtonsoft.Json;

namespace Megaplan.API.Queries
{
    public class AddTaskQueryParams
    {
        /// <summary>
        /// ��������
        /// ������������ ����
        /// </summary>
        [JsonProperty("Model[Name]")]
        public string Name { get; set; }

        /// <summary>
        /// ������� (���� �� ��������)
        /// </summary>
        [JsonProperty("Model[Deadline]")]
        public DateTime? Deadline { get; set; }

        /// <summary>
        /// ������� (������ ����)
        /// </summary>
        [JsonProperty("Model[DeadlineDate]")]
        public DateTime? DeadlineDate { get; set; }

        /// <summary>
        /// ��� ��������
        /// </summary>
        [JsonProperty("Model[DeadlineDate]")]
        public DeadlineType? DeadlineType { get; set; }

        /// <summary>
        /// ��� �������������� (EmployeeId)
        /// ������������ ���� ��� �� �������� ������
        /// </summary>
        [JsonProperty("Model[Responsible]")]
        public int Responsible { get; set; }

        /// <summary>
        /// ���� ��������������
        /// ������������ ���� ��� �������� ������
        /// </summary>
        [JsonProperty("Model[Executors]")]
        public int[] Executors { get; set; }

        /// <summary>
        /// ���� ���������
        /// </summary>
        [JsonProperty("Model[Auditors]")]
        public int[] Auditors { get; set; }

        /// <summary>
        /// ��� ��������
        /// � ������ 2011.3 ��������� ���������� ��������� �������� ��� �������� � MasterType=high
        /// </summary>
        [JsonProperty("Model[Severity]")]
        public int? Severity { get; set; }

        /// <summary>
        /// ��� ��������� (���� �����) ��� ��� ������� (���� ������ � ������� 'p���_�������')
        /// </summary>
        [JsonProperty("Model[SuperTask]")]
        public string SuperTask { get; set; }

        /// <summary>
        /// ��� ���������
        /// </summary>
        [JsonProperty("Model[Customer]")]
        public int? Customer { get; set; }

        /// <summary>
        /// �������� ������ (������� ������������� ����� ������� ���� ������)
        /// </summary>
        [BuildBoolAsInt]
        [JsonProperty("Model[IsGroup]")]
        public bool IsGroup { get; set; }

        /// <summary>
        /// ���� ������
        /// </summary>
        [JsonProperty("Model[Statement]")]
        public string Statement { get; set; }

        /*
         * Model[Attaches][Add]	array	������ ����������� ������	������ ������������ POST-��������
         * Model[Attaches][Add][0...n][Content]	string	������ (�������) �����, �������������� � �������������� MIME base64	� ���������� ������� ��������� ����� ����������� ��� ��������� Model[Attaches][Add][][Context]
         * Model[Attaches][Add][0...n][Name]	string	��� �����	����� ������������ ��� ������ ������
         */

        /// <summary>
        /// ������������: �����
        /// ���� �� ��������
        /// </summary>
        [JsonProperty("Model[Start]")]
        public DateTime? Start { get; set; }

        /// <summary>
        /// ������������: �����
        /// ������ ����. ��� ��������� Model[PlannedTime] ������������� �������������
        /// </summary>
        [JsonProperty("Model[PlannedFinish]")]
        public DateTime? PlannedFinish { get; set; }

        /// <summary>
        /// ������������: ������������ (� ����)
        /// ��� ��������� Model[PlannedFinish] ������������� �������������
        /// </summary>
        [JsonProperty("Model[PlannedTime]")]
        public int? PlannedTime { get; set; }

        /// <summary>
        /// ������������: �������� ������������ (� �������)
        /// </summary>
        [JsonProperty("Model[PlannedWork]")]
        public int? PlannedWork { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">��������</param>
        /// <param name="statement">���� ������</param>
        /// <param name="responsibleId">Id �������������</param>
        /// <returns></returns>
        public static AddTaskQueryParams Simple(string name, string statement, int responsibleId)
        {
            return new AddTaskQueryParams
            {
                Name = name,
                Statement = statement,
                Responsible = responsibleId
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">��������</param>
        /// <param name="statement">���� ������</param>
        /// <param name="responsible">������������</param>
        /// <returns></returns>
        public static AddTaskQueryParams Simple(string name, string statement, Employee responsible)
        {
            return Simple(name, statement, responsible.Id);
        }
    }
}