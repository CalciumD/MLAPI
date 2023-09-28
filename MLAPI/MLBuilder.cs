using Microsoft.ML;

namespace MLAPI
{
    public class MLBuilder
    {
        public string GetSolution(string userIssue)
        {
            MLContext mlCTX = new MLContext();

            // Set paths to your data and model
            var dataPath = "machinelearningdata.csv";
            //var modelPath = "filestoziplol.zip";

            var context = new MLContext();

            // Load training data
            var dataView = context.Data.LoadFromTextFile<UserIssues>(path: dataPath,separatorChar: ',', hasHeader: true);

            // Define data preprocessing and transformation pipeline
            var pipeline = context.Transforms.Text.FeaturizeText("Issue", "Issue")
                .Append(context.Transforms.Text.FeaturizeText("Solution", "Solution"))
                .Append(context.Transforms.Concatenate("Features", "Issue", "Solution"))
                .Append(context.Transforms.NormalizeMinMax("Features"))
                .Append(context.Transforms.CopyColumns("Label", "Solution"));

            // Choose a machine learning algorithm
            var trainer = context.Transforms.Conversion.MapValueToKey("Label")
                .Append(context.Transforms.Text.TokenizeIntoWords("Features"))
                .Append(context.Transforms.Text.RemoveDefaultStopWords("Features"))
                .Append(context.Transforms.Text.ProduceNgrams("Features"))
                .Append(context.Transforms.Text.FeaturizeText("Features"))
                .Append(context.Transforms.NormalizeMinMax("Features"))
                .Append(context.MulticlassClassification.Trainers.SdcaNonCalibrated());

            var trainingPipeline = pipeline.Append(trainer);

            // Train the model
            var model = trainingPipeline.Fit(dataView);

            // Save the model
            // context.Model.Save(model, dataView.Schema, modelPath);

            // Make predictions
            var predictor = context.Model.CreatePredictionEngine<UserIssues, PredictedSolution>(model);

            // passing users request to  the variable.
            var issue1 = userIssue.ToString();

            // passing the issue to the predict to get a solution
            var prediction = predictor.Predict(new UserIssues { Issue = issue1 });

            Console.WriteLine($"Issue: {userIssue}");
            Console.WriteLine($"Predicted Solution: {prediction.PredictedLabel}");


            return "res";
        }
    }
}
