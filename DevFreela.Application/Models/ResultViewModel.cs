namespace DevFreela.Application.Models;

public class ResultViewModel
{
    public ResultViewModel(bool isSuccess = true, string message = "")
    {
        IsSuccess = isSuccess;
        Message = message;
    }

    public bool IsSuccess { get; set; }
    public string Message { get; set; }

    public static ResultViewModel Success()
    {
        return new();
    }

    public static ResultViewModel Error(string message)
    {
        return new(false, message);
    }
}

public class ResultViewModel<T> : ResultViewModel
{
    public ResultViewModel(T? data, bool isSuccess = true, string message = "") : base(isSuccess, message)
    {
        Data = data;
    }
    public T? Data { get; private set; }

    public static ResultViewModel<T> Success(T data)
    {
        return new ResultViewModel<T>(data);
    }

    public static ResultViewModel<T> Error(string message)
    {
        return new(default, false, message);
    }
}
