namespace DI.Reminder.BL.Cache
{
    public interface ICacheService
    {
        T GetValueOfCache<T>(int id) where T:class;
        bool AddCache<T>(T value, int id) where T : class;
        void UpdateCache<T>(T value, int id) where T : class;
        void DeleteCache(int id);
    }
}
