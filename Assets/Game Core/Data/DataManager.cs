using System;

namespace Game_Core.Data
{
    public class DataManager
    {
        public void Save()
        {
            
        }

        public T Get<T>() where T : class
        {
            throw new ArgumentException();
        }
    }
}
