namespace HairdresserAppointmentAPI.Handler.Model
{
    public class TokenInfo<T>
    {
        public T userInfo { get; set; }
        public Token token { get; set; }
    }
}
