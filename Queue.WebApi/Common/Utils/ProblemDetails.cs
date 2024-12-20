namespace Queue.WebApi.Common.Utils;

public class ProblemDetails
{
    public ProblemDetails(string type, string detail, int status)
    {
        Type = type;
        Detail = detail;
        Status = status;
    }
    public ProblemDetails()
    {
        
    }

    public string Type { get; set; }
    public string Detail { get; set; }
    public int Status {get;set;}
}
