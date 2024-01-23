namespace Shop.Microservice.EndPoint.Dtos
{
    public class ResultDto
    {
        public ResultDto(string? message)
        {
            Message = message;
        }
        public ResultDto(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public ResultDto(string? message, bool isSuccess) : this(message)
        {
            IsSuccess = isSuccess;
        }

        public string? Message { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class ResultDto<T> : ResultDto
    {
       
        public ResultDto(string? message, bool isSuccess, T ?data) : base(message,isSuccess)
        {
           Data = data;
        }

     
        public T ?Data { get; set; }
    }
}
