namespace Source.Dtos
{
    public class ApiReponse<T>
    {
        public T? Data { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}