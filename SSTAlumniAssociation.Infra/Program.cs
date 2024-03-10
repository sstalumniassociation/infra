using Pulumi;

return await Deployment.RunAsync(() =>
{
    return new Dictionary<string, object?>
    {
        ["outputKey"] = "outputValue"
    };
});