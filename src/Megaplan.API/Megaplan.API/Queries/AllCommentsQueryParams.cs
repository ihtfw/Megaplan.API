namespace Megaplan.API.Queries
{
    /// <summary>
    /// ������ ���� ������������ �� ���������� ������� � ��������
    /// </summary>
    public class AllCommentsQueryParams
    {
        /// <summary>
        /// ���� true, �� ����� ���������� ����������� ������ ������������� ����� ��� ��������
        /// </summary>
        public bool OnlyActual { get; set; }

        public static AllCommentsQueryParams Actual()
        {
            return new AllCommentsQueryParams()
                   {
                       OnlyActual = true
                   };
        }
    }
}