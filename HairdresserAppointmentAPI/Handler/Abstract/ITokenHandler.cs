using HairdresserAppointmentAPI.Handler.Model;
using System.Security.Claims;

namespace HairdresserAppointmentAPI.Handler.Abstract
{
    public interface ITokenHandler
    {
        Token CreateAccessToken(DateTime endDate, IList<Claim> claims);
    }
}
