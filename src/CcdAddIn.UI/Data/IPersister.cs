namespace CcdAddIn.UI.Data
{
    public interface IPersister<T>
    {
        void Save(T objectToBeSaved);
        T Load();
    }
}