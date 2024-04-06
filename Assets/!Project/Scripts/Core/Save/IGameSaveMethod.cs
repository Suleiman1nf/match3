namespace _Project.Scripts.Core.Save
{
    public interface IGameSaveMethod<T> where T : class
    {
        public void Save(T obj);

        public T Load();
    }
}