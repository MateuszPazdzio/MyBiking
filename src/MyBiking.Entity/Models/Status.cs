using MyBiking.Entity.Enums;

namespace MyBiking.Application.Dtos
{
    public class Status
    {
        public int StatusCode { get; set; }
        public Code Code { get; set; }
        public string Message { get; set; }
    }
}
