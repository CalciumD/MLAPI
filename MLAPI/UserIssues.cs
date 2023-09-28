using Microsoft.ML.Data;

namespace MLAPI
{
    public class UserIssues
    {
        [LoadColumn(0)] public string Issue { get; set; }

        [LoadColumn(1)] public string Solution { get;set; }
    }
}
