using Microsoft.ML.Data;

namespace MLAPI
{
    public class PredictedSolution
    {
        [ColumnName("PredictedLabel")]
        public string PredictedLabel;
    }
}
