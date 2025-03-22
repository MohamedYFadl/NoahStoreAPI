namespace NoahStore.API.Errors
{
    public class APIValidationErrorResponse : ApiResponse
    {
        public APIValidationErrorResponse() : base(400)
        {
            Errors = new List<string>();
        }

        public List<string> Errors { get; set; }

    }
}
