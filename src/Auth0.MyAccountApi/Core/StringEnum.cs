namespace Auth0.MyAccountApi.Core;

public interface IStringEnum : IEquatable<string>
{
    public string Value { get; }
}
