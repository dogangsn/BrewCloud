﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BrewCloud.Shared.Dtos
{
    public class Response<T>
    {
        public T Data { get; set; }

        [JsonIgnore]
        public int StatusCode { get; set; }
        
        public bool IsSuccessful { get; set; }

        public List<string> Errors { get; set; }

        public ResponseType ResponseType { get; set; }


        public static Response<T> Success(T data, int statusCode)
        {
            return new Response<T> { Data = data, StatusCode = statusCode, IsSuccessful = true };
        }

        public static Response<T> Success(int statusCode)
        {
            return new Response<T> { Data = default(T), StatusCode = statusCode, IsSuccessful = true };
        }

        public static Response<T> Fail(string errors, int statusCode)
        {
            return new Response<T> { Errors = new List<string>() { errors }, StatusCode = statusCode, IsSuccessful = false };
        }

        public static Response<T> Fail(List<string> errors, int statusCode)
        {
            return new Response<T> { Errors = errors, StatusCode = statusCode, IsSuccessful = false };
        }
        public static Response<T> Fail(int statusCode)
        {
            return new Response<T> { Errors = new List<string>(), StatusCode = statusCode, IsSuccessful = false };
        }

    }

    public enum ResponseType
    {
        Ok = 1,
        Warning = 2,
        Error = 3,
        updatedFromMembershipRegister = 4,
        ConcurrencyException = 5
    }
}
