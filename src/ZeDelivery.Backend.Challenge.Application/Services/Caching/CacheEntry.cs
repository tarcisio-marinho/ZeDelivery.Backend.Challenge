using System.Diagnostics.CodeAnalysis;

namespace ZeDelivery.Backend.Challenge.Application.Services.Caching
{
    [ExcludeFromCodeCoverage]
    public class CacheEntry<T>
    {
        public CacheEntry(T value, bool success)
        {
            Success = success;
            Value = value;
        }

        public bool Success { get; private set; }
        public T Value { get; private set; }
    }
}