using System.Security.Cryptography;
using System.Text;
using Application.Interfaces;

namespace Infrastructure.Services;

public sealed class PasswordHasher : IPasswordHasher
{
    private readonly HashAlgorithm _hashAlgorithm;

    public PasswordHasher(HashAlgorithm hashAlgorithm)
    {
        _hashAlgorithm = hashAlgorithm;
    }

    public string Hash(string password)
    {
        var bytes = Encoding.UTF8.GetBytes(password);

        var hash = _hashAlgorithm.ComputeHash(bytes);

        var builder = new StringBuilder();
        foreach (var b in hash)
        {
            builder.Append(b.ToString("x2"));
        }

        return builder.ToString();
    }
}