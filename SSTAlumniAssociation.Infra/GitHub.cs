using Pulumi.Github;
using Pulumi.Tls;

namespace SSTAlumniAssociation.Infra;

public static class GitHub
{
    private const string ApiRepository = "api";
    private const string ApiClientTypescriptRepository = "api-client-typescript";
    private const string GrpcClientTypescriptRepository = "grpc-client-typescript";

    public static void Run()
    {
        var apiClientTsPrivateKey = new PrivateKey("apiClientTypescriptDeployKeyPrivateKey",
            new PrivateKeyArgs
            {
                Algorithm = "ED25519"
            });

        var apiClientTsDeployKey = new RepositoryDeployKey("apiClientTypescriptDeployKey",
            new RepositoryDeployKeyArgs
            {
                Title = "Deploy key for API repo. Managed by Pulumi.",
                Repository = ApiClientTypescriptRepository,
                Key = apiClientTsPrivateKey.PublicKeyOpenssh,
                ReadOnly = false,
            });

        var grpcClientTsPrivateKey = new PrivateKey("grpcClientTypescriptDeployKeyPrivateKey",
            new PrivateKeyArgs
            {
                Algorithm = "ED25519"
            });

        var grpcClientTsDeployKey = new RepositoryDeployKey("grpcClientTypescriptDeployKey",
            new RepositoryDeployKeyArgs
            {
                Title = "Deploy key for API repo. Managed by Pulumi.",
                Repository = GrpcClientTypescriptRepository,
                Key = grpcClientTsPrivateKey.PublicKeyOpenssh,
                ReadOnly = false,
            });

        var apiActionSecrets = new ActionsSecret("apiClientTypescriptActionSecret", new ActionsSecretArgs
        {
            SecretName = "API_CLIENT_TYPESCRIPT_DEPLOY_KEY",
            Repository = ApiRepository,
            PlaintextValue = apiClientTsPrivateKey.PrivateKeyOpenssh
        });

        var grpcActionSecrets = new ActionsSecret("grpcClientTypescriptActionSecret", new ActionsSecretArgs
        {
            SecretName = "GRPC_CLIENT_TYPESCRIPT_DEPLOY_KEY",
            Repository = ApiRepository,
            PlaintextValue = grpcClientTsPrivateKey.PrivateKeyOpenssh
        });
    }
}