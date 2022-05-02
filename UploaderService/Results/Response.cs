using System.Collections.Generic;

namespace UploaderService.Results
{
    public static class Response
    {
        public static Response<T> Success<T>(T data, List<string> messages) 
            => new(data,messages , true);
        public static Response<T> Success<T>(T data, string message)
            => new(data, message, true);
        public static Response<T> Fail<T>(List<string> messages, T data = default)
            => new(data, messages, false);
        public static Response<T> Fail<T>(string message, T data = default)
            => new(data, message, false);
    }

    public class Response<T> 
    {
        public T Data { get; set; }
        public List<string> Messages { get; set; } = new();
        public bool Success { get; set; }

        public Response()
        {
            
        }
        public Response(T data, List<string> messages, bool success)
        {
            Data = data;
            Success = success;
            Messages = messages;
        }
        public Response(T data, string messages, bool success)
        {
            Data = data;
            Success = success;
            Messages = new List<string> {messages};
        }

        public Response<T> AddError(string message)
        {
            this.Messages.Add(message);
            return this;
        }
        public Response<T> AddError(List<string> message)
        {

            this.Messages.AddRange(message);
            return this;
        }

    }
}
