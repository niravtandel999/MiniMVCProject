using MVCTaskProject.Models;

namespace MVCTaskProject.Repository
{
    public interface IHomeRepository
    {
        // List<UserTasks> GetUserTask();
        List<UserTasks> GetUserTasks(int userId);
        bool AddUserTasks(AddUsersTasks addTask, int userId);
        UserTasksDto Update(int id);
        bool UpdateUserTask(UserTasksDto userTasksDto, int userId);
        bool DeleteUserTask(int id);
    }
}
