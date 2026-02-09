namespace EntityLayer.Models
{
    public class ErrorVM
    {

        public ErrorVM(List<string> errors, int statusCode)
        {
            Errors = errors;
            StatusCode = statusCode;
        }
        public ErrorVM(string error, int statusCode)
        {
            Errors = new List<string> { error };
            StatusCode = statusCode;
        }

        public ErrorVM()
        {
        }

        public List<string> Errors = new List<string>();
        public int StatusCode { get; set; }
    }
}
