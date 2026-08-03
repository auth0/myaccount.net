namespace Auth0.MyAccountApi;

/// <summary>
/// Base exception class for all exceptions thrown by the SDK.
/// </summary>
public class MyAccountException(string message, Exception? innerException = null)
    : Exception(message, innerException);
