﻿namespace MicroserviceBasedFintechApp.OrderService.Core.Contracts.Exceptions.Base
{
    public class BaseException : Exception
    {
        public int StatusCode { get; set; }
        public BaseException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
