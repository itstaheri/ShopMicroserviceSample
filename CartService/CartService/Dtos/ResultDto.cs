namespace CartService.Dtos
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

        public ResultDto(bool isSuccess,string? message) : this(message)
        {
            IsSuccess = isSuccess;
        }

        public string? Message { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class ResultDto<T>
    {
       
        public ResultDto(string? message, bool isSuccess, T data) 
        {
            IsSuccess = isSuccess;
            Data = data;
        }

        public string? Message { get;private set; }
        public bool IsSuccess { get;private set; }
        public T Data { get; set; }
    }
}
