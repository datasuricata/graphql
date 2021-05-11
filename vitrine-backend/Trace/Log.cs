using System;

namespace vitrine_backend.Trace
{
    public class Log
    {
        public Log()
        {

        }

        public Log(string payload)
        {
            Id = Guid.NewGuid().ToString();
            Payload = payload;
        }

        public string Id { get; set; } 
        public string Payload { get; set; }
    }
}
