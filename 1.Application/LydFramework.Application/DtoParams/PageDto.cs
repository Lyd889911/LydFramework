namespace LydFramework.Application.DtoParams
{
    public record PageDto<T>(int Total,T Data);
}
