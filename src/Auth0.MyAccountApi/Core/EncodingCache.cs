using global::System.Text;

namespace Auth0.MyAccountApi.Core;

internal static class EncodingCache
{
    internal static readonly Encoding Utf8NoBom = new UTF8Encoding(
        encoderShouldEmitUTF8Identifier: false,
        throwOnInvalidBytes: true
    );
}
