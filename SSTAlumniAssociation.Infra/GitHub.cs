using Pulumi.Github;
using Pulumi.Tls;

namespace SSTAlumniAssociation.Infra;

public static class GitHub
{
    private const string ApiClientTypescriptRepository = "sstalumniassociation/api-client-typescript";
    private const string GrpcClientTypescriptRepository = "sstalumniassociation/grpc-client-typescript";

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

        var apiActionSecrets = new ActionsSecret("apiClientTypescriptSecret", new ActionsSecretArgs
        {
            SecretName = "API_CLIENT_TYPESCRIPT_DEPLOY_KEY",
            Repository = ApiClientTypescriptRepository,
            PlaintextValue = apiClientTsDeployKey.Key
        });

        var grpcActionSecrets = new ActionsSecret("grpcClientTypescriptSecret", new ActionsSecretArgs
        {
            SecretName = "GRPC_CLIENT_TYPESCRIPT_DEPLOY_KEY",
            Repository = GrpcClientTypescriptRepository,
            PlaintextValue = grpcClientTsDeployKey.Key
        });
    }
}