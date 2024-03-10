using Pulumi;
using SSTAlumniAssociation.Infra;

return await Deployment.RunAsync(() =>
{
    GitHub.Run();

    return new Dictionary<string, object?>();
});