using ClosedXML.Excel;

namespace MembershipSystem.Command.Commands
{
    public class ExcelFile<T>
    {
        public readonly List<T> _list;
        public string FileName => $"{typeof(T).Name}.xlsx";
        public string FileType => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        public ExcelFile(List<T> list)
        {
            _list = list;
        }

        public MemoryStream Create()
        {
            XLWorkbook workBook = new();
            DataSet dataset = new();

            dataset.Tables.Add(GetTable());

            workBook.Worksheets.Add(dataset);

            MemoryStream excelMemory = new();
            workBook.SaveAs(excelMemory);

            return excelMemory;
        }

        private DataTable GetTable()
        {
            DataTable table = new();

            var type = typeof(T);
            type.GetProperties().ToList().ForEach(x => table.Columns.Add(x.Name, x.PropertyType));

            _list.ForEach(x =>
            {
                var values = type.GetProperties().Select(properyInfo => properyInfo.GetValue(x, null)).ToArray();
                table.Rows.Add(values);
            });

            return table;
        }
    }
}
