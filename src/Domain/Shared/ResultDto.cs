namespace Domain.Shared
{
    public class ResultDto<TData>
    {
        public bool IsSuccess { get; private set; }
        public TData Data { get; private set; }
        public string ErrorMessage { get; private set; }

        private ResultDto(bool success, TData data, string errorMessage)
        {
            IsSuccess = success;
            Data = data;
            ErrorMessage = errorMessage;
        }

        public static ResultDto<TData> Success(TData data)
        {
            return new ResultDto<TData>(true, data, null);
        }

        public static ResultDto<TData> Fail(string errorMessage)
        {
            return new ResultDto<TData>(false, default(TData), errorMessage);
        }
    }
}
