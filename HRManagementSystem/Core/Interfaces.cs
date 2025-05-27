namespace HRManagementSystem
{
    public interface IFileStorage
    {
        bool SaveData<T>(string filename, T data);
        T LoadData<T>(string filename);
    }

    public interface IService<T>
    {
        List<T> GetAll();
        T GetById(string id);
        bool Add(T entity);
        bool Update(T entity);
        bool Delete(string id);
    }

}