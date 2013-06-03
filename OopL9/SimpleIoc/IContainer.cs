namespace SimpleIoc
{
    public interface IContainer
    {
        #region Public methods

        void RegisterType<T>(bool singleton) where T : class;
        void RegisterType<TFrom, TTo>(bool singleton) where TTo : TFrom;
        T Resolve<T>();

        void RegisterInstance<T>(T Instance);

        #endregion
    }
}