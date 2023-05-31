using connect_.Models;

namespace connect_.Services
{
    public interface IAuthServices
    {
        Task<AuthModel> RegisterationAsync(RegisterationModel model);
        Task<AuthModel> GetTokenAsync(TokenRequestModel model);
    }
}
