namespace HairdresserAppointmentAPI.Model
{
    public class ResponseModel<T>
    {
        public Dictionary<string, string> ValidationErrors { get; set; }
        public bool HasError { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public ResponseModel()
        {
            this.ValidationErrors = new Dictionary<string, string>();
            this.Message = string.Empty;
        }
    }
}
