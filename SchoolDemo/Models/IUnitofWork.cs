namespace SchoolDemo.Models
{
    public interface IUnitofWork
    {
        public IGenericRepository<Student> StudentRepository { get; }
        public IGenericRepository<UserModel> UserRepository { get; }
        void Save();
    }
}
 