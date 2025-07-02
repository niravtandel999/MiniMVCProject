using MVCTaskProject.Models;

namespace MVCTaskProject.Services
{
    public interface IMailService
    {
        bool SendEmail(SendEmail email);
    }
}
