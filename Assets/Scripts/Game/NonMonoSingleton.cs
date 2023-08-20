
namespace BattleTank.Generics
{
    public class NonMonoSingleton<T> where T : NonMonoSingleton<T>
    {
        private static T instance = null;
        public static T Instance { get { return instance; } }

        protected NonMonoSingleton()
        {
            if (instance == null)
            {
                instance = (T)this;
            }
        }
    }
}