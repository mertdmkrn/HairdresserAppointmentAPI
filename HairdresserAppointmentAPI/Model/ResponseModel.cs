namespace HairdresserAppointmentAPI.Model
{
    public class ResponseModel<T>
    {
        public List<ValidationError> ValidationErrors { get; set; }
        public bool HasError { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public ResponseModel()
        {
            this.ValidationErrors = new List<ValidationError>();
            this.Message = string.Empty;
        }
    }

    public class ValidationError
    { 
        public string key { get; set; }
        public string value { get; set; }

        public ValidationError(string key, string value)
        {
            this.key = key;
            this.value = value;
        }
    }
}
