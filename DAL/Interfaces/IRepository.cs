using DAL.DBConnector;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Interfaces
{
    public interface IRepository<T>
    {
        IEnumerable<T> Find(Func<T, Boolean> predicate);
        T FindByID(int id);
        T Create(T obj);
        T Update(T obj);
        void Delete(int id);
    }

    public class TaskAppRepository : IRepository<TaskApp>
    {
        private DBMyContext _context;

        public TaskAppRepository(DBMyContext context)
        {
            _context = context;
        }

        public TaskApp Create(TaskApp obj)
        {
            _context.TasksApp.Add(obj);
            _context.SaveChanges();
            return obj;
        }

        public void Delete(int id)
        {
            var obj = _context.TasksApp.FirstOrDefault(x => x.Id == id);
            _context.TasksApp.Remove(obj);
            _context.SaveChanges();
        }

        public IEnumerable<TaskApp> Find(Func<TaskApp, bool> predicate)
        {
            return _context.TasksApp.Where(predicate);
        }

        public TaskApp FindByID(int id)
        {
            return _context.TasksApp.FirstOrDefault(x => x.Id == id);
        }

        public TaskApp Update(TaskApp obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            _context.SaveChanges();
            return obj;
        }
    }

    public class UserRepository : IRepository<User>
    {
        private DBMyContext _context;
        public UserRepository(DBMyContext context)
        {
            _context = context;
        }
        public User Create(User obj)
        {
            _context.Users.Add(obj);
            _context.SaveChanges();
            return obj;
        }

        public void Delete(int id)
        {
            var obj = _context.Users.FirstOrDefault(x => x.Id == id);
            _context.Users.Remove(obj);
            _context.SaveChanges();
        }

        public IEnumerable<User> Find(Func<User, bool> predicate)
        {
            return _context.Users.Where(predicate);
        }

        public User FindByID(int id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }

        public User Update(User obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            _context.SaveChanges();
            return obj;
        }
    }

    public class PipelineRepository : IRepository<Pipeline>
    {
        private DBMyContext _context;
        public PipelineRepository(DBMyContext context)
        {
            _context = context;
        }
        public Pipeline Create(Pipeline obj)
        {
            _context.Pipelines.Add(obj);
            _context.SaveChanges();
            return obj;
        }

        public void Delete(int id)
        {
            var obj = _context.Pipelines.FirstOrDefault(x => x.Id == id);
            _context.Pipelines.Remove(obj);
            _context.SaveChanges();
        }

        public IEnumerable<Pipeline> Find(Func<Pipeline, bool> predicate)
        {
            return _context.Pipelines.Where(predicate);
        }

        public Pipeline FindByID(int id)
        {
            return _context.Pipelines.FirstOrDefault(x => x.Id == id);
        }

        public Pipeline Update(Pipeline obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            _context.SaveChanges();
            return obj;
        }
    }
}
