using Pulumi;
using SSTAlumniAssociation.Infra;

return await Deployment.RunAsync(() =>
{
    if (Deployment.Instance.StackName == "production")
    {
        GitHub.Run();
    }

    return new Dictionary<string, object?>();
});