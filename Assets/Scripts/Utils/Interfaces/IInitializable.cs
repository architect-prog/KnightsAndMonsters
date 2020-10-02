namespace Assets.Scripts.Utils
{
    public interface IInitializable
    {
        void Initialize();
    }

    public interface IInitializable<T>
    {
        void Initialize(T value);
    }
}
