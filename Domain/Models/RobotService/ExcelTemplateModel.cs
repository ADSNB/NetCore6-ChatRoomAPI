using static Domain.Helpers.EppPlusExtensionHelper;

namespace Domain.Models.RobotService
{
    public class ExcelTemplateModel
    {
        [Column(1)]
        public string Symbol { get; set; }

        [Column(2)]
        public string Date { get; set; }

        [Column(3)]
        public string Time { get; set; }

        [Column(4)]
        public string Open { get; set; }

        [Column(5)]
        public string High { get; set; }

        [Column(6)]
        public string Low { get; set; }

        [Column(7)]
        public string Close { get; set; }

        [Column(8)]
        public string Volume { get; set; }

        // public string Status { get; set; }
        // public bool IsValid { get; set; }
    }

    /*
    [AttributeUsage(AttributeTargets.All)]
    public class Column : Attribute
    {
        public int ColumnIndex { get; set; }

        public Column(int column)
        {
            ColumnIndex = column;
        }
    }

    public static class EPPLusExtensions
    {
        public static IEnumerable<T> ConvertSheetToObjects<T>(this ExcelWorksheet worksheet) where T : new()
        {
            Func<CustomAttributeData, bool> columnOnly = y => y.AttributeType == typeof(Column);

            var columns = typeof(T)
                    .GetProperties()
                    .Where(x => x.CustomAttributes.Any(columnOnly))
            .Select(p => new
            {
                Property = p,
                Column = p.GetCustomAttributes<Column>().First().ColumnIndex //safe because if where above
            }).ToList();

            var rows = worksheet.Cells
                .Select(cell => cell.Start.Row)
                .Distinct()
                .OrderBy(x => x);

            //Create the collection container
            var collection = rows.Skip(1)
                .Select(row =>
                {
                    var tnew = new T();
                    columns.ForEach(col =>
                    {
                        //This is the real wrinkle to using reflection - Excel stores all numbers as double including int
                        var val = worksheet.Cells[row, col.Column];
                        //If it is numeric it is a double since that is how excel stores all numbers
                        if (val.Value == null)
                        {
                            col.Property.SetValue(tnew, null);
                            return;
                        }
                        if (col.Property.PropertyType == typeof(Int32))
                        {
                            col.Property.SetValue(tnew, val.GetValue<int>());
                            return;
                        }
                        if (col.Property.PropertyType == typeof(double))
                        {
                            col.Property.SetValue(tnew, val.GetValue<double>());
                            return;
                        }
                        if (col.Property.PropertyType == typeof(DateTime))
                        {
                            col.Property.SetValue(tnew, val.GetValue<DateTime>());
                            return;
                        }
                        //Its a string
                        col.Property.SetValue(tnew, val.GetValue<string>());
                    });

                    return tnew;
                });

            //Send it back
            return collection;
        }
    }*/
}