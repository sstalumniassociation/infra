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
                Title = "Deploy key for API repo",
                Repository = ApiClientTypescriptRepository,
                ReadOnly = true,
                Key = apiClientTsPrivateKey.PublicKeyOpenssh
            });

        var grpcClientTsPrivateKey = new PrivateKey("grpcClientTypescriptDeployKeyPrivateKey",
            new PrivateKeyArgs
            {
                Algorithm = "ED25519"
            });

        var grpcClientTsDeployKey = new RepositoryDeployKey("grpcClientTypescriptDeployKey",
            new RepositoryDeployKeyArgs
            {
                Title = "Deploy key for API repo",
                Repository = GrpcClientTypescriptRepository,
                ReadOnly = true,
                Key = grpcClientTsPrivateKey.PublicKeyOpenssh
            });

        var apiActionSecrets = new ActionsSecret("apiClientTypescriptActionSecret", new ActionsSecretArgs
        {
            SecretName = "API_CLIENT_TYPESCRIPT_DEPLOY_KEY",
            Repository = ApiRepository,
            PlaintextValue = apiClientTsDeployKey.Key
        });

        var grpcActionSecrets = new ActionsSecret("grpcClientTypescriptActionSecret", new ActionsSecretArgs
        {
            SecretName = "GRPC_CLIENT_TYPESCRIPT_DEPLOY_KEY",
            Repository = ApiRepository,
            PlaintextValue = grpcClientTsDeployKey.Key
        });
    }
}