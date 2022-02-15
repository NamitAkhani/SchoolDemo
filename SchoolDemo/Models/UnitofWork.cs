namespace SchoolDemo.Models
{
    public class UnitofWork : IUnitofWork
    {
        public StudentContext _studentContext;
        public IGenericRepository<Student> _genericRepository;
        public IStudentRepository _studentRepository;
        public IGenericRepository<UserModel> _userRepository;   

        public UnitofWork()
        {
        }

        public UnitofWork(StudentContext studentContext)
        {
            _studentContext = studentContext;
        }
        public IGenericRepository<Student> StudentRepository
        {
            get
            {
                return _genericRepository ?? new GenericRepository<Student>(_studentContext);
            }
        }
        public IGenericRepository<UserModel> UserRepository
        {
            get { return _userRepository ?? new GenericRepository<UserModel>(_studentContext); } 
        }

        public void Save()
        {
            _studentContext.SaveChanges();
        }
    }
}
