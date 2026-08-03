namespace Auth0.MyAccountApi;

/// <summary>
/// This exception type will be thrown for any non-2XX API responses.
/// </summary>
[Serializable]
public class BadRequestError(ErrorResponse body, Auth0.MyAccountApi.RawResponse? rawResponse = null)
    : MyAccountApiException("BadRequestError", 400, body, rawResponse: rawResponse)
{
    /// <summary>
    /// The body of the response that triggered the exception.
    /// </summary>
    public new ErrorResponse Body => body;
}
