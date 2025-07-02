using MVCTaskProject.Models;

namespace MVCTaskProject.Repository
{
    public interface IProfileRepository
    {
        UserProfileDto GetUserProfile(int userId);
        List<UserProfile> GetAllUserProfiles();
        bool AddUserProfile(UserProfileDto addProfile, int userId);
        bool EditUserProfile(UserProfileDto userProfileDto, int userId);
    }
}
