namespace SmartCharging.Core.Exceptions;

public class ValidationFailureModel
{
    public ValidationFailureModel()
    {
        Errors = new List<string>();
    }

    public string Field { get; set; }
    public IEnumerable<string> Errors { get; set; }
}