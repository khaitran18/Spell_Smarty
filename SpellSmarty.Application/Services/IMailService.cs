using SpellSmarty.Domain.Models;

namespace SpellSmarty.Application.Services
{
    public interface IMailService
    {
        Task<bool> SendAsync(MailDataModel mailData, CancellationToken ct);
    }
}
