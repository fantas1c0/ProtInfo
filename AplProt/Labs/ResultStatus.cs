namespace AplProt.Labs
{

    public enum Status
    {
        Success,
        Warning,
        Failed
    }
    public record ResultStatus(Status status, string message);

    //public class ResultStatus
    //{
    //    private Status status { get; }
    //    private string message { get; }

    //    public ResultStatus(Status s, string m) { status = s; message = m; }

        //public static R Success() =>
        //        new(ResultStatus.Success, "Sucsess"); 

        //public static OperationResult Warning() =>
        //        new(ResultStatus.Warning, "Warning");

        //public static OperationResult Failed() =>
        //        new(ResultStatus.Failed, "Failed");

    //}
}
