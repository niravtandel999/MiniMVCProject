using MVCTaskProject.Models;

namespace MVCTaskProject.Repository
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProfileRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public UserProfileDto GetUserProfile(int userId)
        {
            var result = _dbContext.Profiles
                .Where(x => x.UserId == userId)
                .Select(x => new UserProfileDto
                {
                    ProfileId = x.ProfileId,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    Phone = x.Phone,
                    DOB = x.DOB,
                    City = x.City,
                    UserId = userId
                }).FirstOrDefault();
            return result;

        }

        public List<UserProfile> GetAllUserProfiles()
        {
            return _dbContext.Profiles.ToList();
        }

        public bool AddUserProfile(UserProfileDto addProfile, int userId)
        {
            var record = new UserProfile()
            {
                FirstName = addProfile.FirstName,
                LastName = addProfile.LastName,
                Email = addProfile.Email,
                Phone = addProfile.Phone,
                DOB = addProfile.DOB,
                City = addProfile.City,

                UserId = userId
            };

            _dbContext.Profiles.Add(record);
            var result = _dbContext.SaveChanges();
            bool res = Convert.ToBoolean(result);
            return res;
        }



        public bool EditUserProfile(UserProfileDto userProfileDto, int userId)
        {
            var existingProfile = _dbContext.Profiles.FirstOrDefault(x => x.UserId == userId);

            if (existingProfile != null)
            {
                existingProfile.FirstName = userProfileDto.FirstName;
                existingProfile.LastName = userProfileDto.LastName;
                existingProfile.Email = userProfileDto.Email;
                existingProfile.Phone = userProfileDto.Phone;
                existingProfile.DOB = userProfileDto.DOB;
                existingProfile.City = userProfileDto.City;

                if (!string.IsNullOrEmpty(userProfileDto.ProfilePictureUrl))
                {
                    existingProfile.ProfilePictureUrl = userProfileDto.ProfilePictureUrl;
                }

                _dbContext.SaveChanges();
                return true; 
            }

            return false;
        }


    }
}
