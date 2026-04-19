using NoahStore.Core.Dto;

namespace NoahStore.Core.Services
{
    public interface IMailService
    {
         Task SendEmail(EmailDto email);
    }
}
