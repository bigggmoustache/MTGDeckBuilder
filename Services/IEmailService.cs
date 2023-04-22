using MTGDeckBuilder.DTO.Email;

namespace MTGDeckBuilder.Services
{
    public interface IEmailService
    {
        void SendEmail(EmailDto emailDto, IConfiguration configuration);
    }
}
