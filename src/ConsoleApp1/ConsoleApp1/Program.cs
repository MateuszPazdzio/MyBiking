using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;

ExcelFactory excelFactory = new ExcelFactory();
var file = excelFactory.GetExcelFile(ExcelEnum.TYPE1);

var result = file.Operate(new OprArgs<List<string>>()
{
    Args = new List<string>() { "arg1", "arg2", "arg3" }
});

var data = result as OprResult1;
    data.Result.ForEach(e=>Console.WriteLine(e));



internal class ExcelFactory : IExcelFactory
{
    private Dictionary<ExcelEnum, ExcelItem> keyValuePairs = new Dictionary<ExcelEnum, ExcelItem>()
    {
        {ExcelEnum.TYPE1, new ExcelVer1() }
    };

    public ExcelItem GetExcelFile(ExcelEnum excelFile)
    {
        return keyValuePairs[excelFile];
    }
}

internal interface IExcelFactory
{
    ExcelItem GetExcelFile(ExcelEnum excelFile);
}
public enum ExcelEnum
{
    TYPE1,TYPE2, TYPE3, TYPE4, TYPE5, TYPE6,
}
class ExcelVer1 : ExcelItem
{
    public void OpenExcel() {
        base.OpenExcel("..//");
    }

    public override OprResult<T> Operate<T>(OprArgs<T> oprArgs)
    {

        List<string> args = new List<string>();
        args = oprArgs.Args as List<string>;
        args=args.Select(arg => arg.ToUpper()).ToList();

        return new OprResult1()
        {
            Result = args.ToList()
        } as OprResult<T>;
    }
}
abstract public class ExcelItem
{
    protected XLWorkbook wb;
    protected void OpenExcel(string path)
    {
        try
        {
            using(wb=new XLWorkbook(path))
            {
                Console.WriteLine(wb.Author);
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    public abstract OprResult<T> Operate<T>(OprArgs<T> oprArgs);
}

public class OprResult1 : OprResult<List<string>>
{
    public List<string> Result { get; set; }
}

public class OprResult<T>
{
    public T Result { get; set; }

}

public class OprArgs<T>
{
    public T Args { get; set; }

}