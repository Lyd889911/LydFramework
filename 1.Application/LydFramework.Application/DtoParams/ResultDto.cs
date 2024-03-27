namespace LydFramework.Application.DtoParams
{
    public class ResultDto
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public ResultDto(int code,string message,object data) 
        { 
            Code = code;
            Message = message;
            Data = data;
        }
        public ResultDto(object data)
        {
            Code = 200;
            Data = data;
        }
        public ResultDto(int code,string message)
        {
            Code=code;
            Message = message;
        }
        private ResultDto()
        {

        }
    }

    public class ResultDto<T>
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
