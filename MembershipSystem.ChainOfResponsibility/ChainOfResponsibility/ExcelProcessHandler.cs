namespace MembershipSystem.ChainOfResponsibility.ChainOfResponsibility
{
    public class ExcelProcessHandler<T> : ProcessHandler
    {
        private static DataTable GetTable(object obj)
        {
            DataTable table = new();
            var type = typeof(T);
            type.GetProperties().ToList().ForEach(x => table.Columns.Add(x.Name, x.PropertyType));

            var list = obj as List<T>;

            list.ForEach(x =>
            {
                var values = type.GetProperties().Select(propertyInfo => propertyInfo.GetValue(x, null)).ToArray();
                table.Rows.Add(values);
            });

            return table;
        }

        public override object Handle(object obj)
        {
            XLWorkbook workbook = new();
            DataSet dataSet = new();

            dataSet.Tables.Add(GetTable(obj));
            workbook.Worksheets.Add(dataSet);

            MemoryStream excelMemoryStream = new();
            workbook.SaveAs(excelMemoryStream);

            return base.Handle(excelMemoryStream);
        }
    }
}
