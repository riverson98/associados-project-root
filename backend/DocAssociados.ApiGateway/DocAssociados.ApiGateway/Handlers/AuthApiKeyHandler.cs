using DocAssociados.ApiGateway.Config;

namespace DocAssociados.ApiGateway.Handlers;

public class AuthApiKeyHandler : DelegatingHandler
{
    private readonly ApiKeyContainer _keys;

    public AuthApiKeyHandler(ApiKeyContainer keys)
    {
        _keys = keys;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.Headers.Add("X-Api-Key", _keys.AuthKey);
        return await base.SendAsync(request, cancellationToken);
    }
}
