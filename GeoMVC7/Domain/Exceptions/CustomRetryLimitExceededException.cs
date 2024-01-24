// CustomRetryLimitExceededException.cs
using System;
using GeoMVC7.Domain.Exceptions;
using Microsoft.EntityFrameworkCore.Storage;
namespace GeoMVC7.Domain.Exceptions
{
    public class CustomRetryLimitExceededException : CustomException
    {
        public CustomRetryLimitExceededException() { }
        public CustomRetryLimitExceededException(string message) : base(message) { }
        public CustomRetryLimitExceededException(string message, RetryLimitExceededException innerException) : base(message, innerException) { }
    }
}