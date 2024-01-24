using GeoMVC7.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace GeoMVC7.Domain.Exceptions
{
    public class CustomConcurrencyException : CustomException
    {
        public CustomConcurrencyException() { }
        public CustomConcurrencyException(string message) : base(message) { }
        public CustomConcurrencyException(
        string message, DbUpdateConcurrencyException innerException)
        : base(message, innerException) { }
    }
}
