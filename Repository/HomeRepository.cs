using Microsoft.EntityFrameworkCore;
using MVCTaskProject.Models;
using Org.BouncyCastle.Bcpg;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCTaskProject.Repository
{
    public class HomeRepository : IHomeRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public HomeRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        //public List<UserTasks> GetUserTask()
        //{
        //    var result = _applicationDbContext.UsersTasks
        //        .Select(x => new UserTasks()
        //        {
        //            TaskId = x.TaskId,
        //            TaskName = x.TaskName,
        //            TaskDescription = x.TaskDescription,
        //            CreateDate = x.CreateDate,
        //            CompleteDate = x.CompleteDate,
        //            TaskStatus = x.TaskStatus,
        //        })
        //        .ToList();
        //    return result;
        //}

        public List<UserTasks> GetUserTasks(int userId)
        {
            return _applicationDbContext.UsersTasks
                                        .Where(x=>x.UserId  == userId)
                                        .ToList();
        }


        public bool AddUserTasks(AddUsersTasks addTask,int userId)
        {
            var record = new UserTasks()
            {
                TaskName = addTask.TaskName,
                TaskDescription = addTask.TaskDescription,
                CreateDate = addTask.CreateDate,
                CompleteDate = addTask.CompleteDate,
                TaskStatus = addTask.TaskStatus,

                UserId = userId
            };

            _applicationDbContext.UsersTasks.Add(record);
            var result = _applicationDbContext.SaveChanges();
            bool res = Convert.ToBoolean(result);
            return res;
        }

        public UserTasksDto Update(int id)
        {
            var record = _applicationDbContext.UsersTasks.Where(x=>x.TaskId == id).FirstOrDefault();
            var result = new UserTasksDto()
            {
                TaskId = record.TaskId,
                TaskName = record.TaskName,
                TaskDescription = record.TaskDescription,
                CreateDate = record.CreateDate,
                CompleteDate = record.CompleteDate,
                TaskStatus = record.TaskStatus,
            };
            return result;
        }

        public bool UpdateUserTask(UserTasksDto userTasksDto, int userId)
        {
            var record = new UserTasks()
            {
                TaskId = userTasksDto.TaskId,
                TaskName = userTasksDto.TaskName,
                TaskDescription = userTasksDto.TaskDescription,
                CreateDate = userTasksDto.CreateDate,
                CompleteDate = userTasksDto.CompleteDate,
                TaskStatus = userTasksDto.TaskStatus,
                UserId = userId
            };
            _applicationDbContext.UsersTasks.Update(record);
            var result = _applicationDbContext.SaveChanges();
            bool res = Convert.ToBoolean(result);
            return res;
        }

        public bool DeleteUserTask(int id)
        {
            var record = _applicationDbContext.UsersTasks.Where(x=>x.TaskId==id).FirstOrDefault();
            if (record != null)
            {
                _applicationDbContext.UsersTasks.Remove(record);
                var result = _applicationDbContext.SaveChanges() ;
                bool res = Convert.ToBoolean(result);
                return res;
            }
            else
            {
                return false;
            }
        }
    }
}
