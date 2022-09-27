namespace JwtNet.Entities.ViewModels
{
    public class ResultViewModel : ResultViewModel<object>
    {
    }
    public class ResultViewModel<T>
    {
        public bool IsSuccess { get; set; }
        public T Result { get; set; }
        public string Message { get; set; }
        public string Url { get; set; }



    }
}
