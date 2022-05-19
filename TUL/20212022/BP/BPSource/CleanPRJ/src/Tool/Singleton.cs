using System;
using System.Collections.Generic;
using System.Text;

namespace DataGrabber.src.Tool
{
    public abstract class Singleton<T> where T:new()
    {
        private static T instance = default;
        public static T I
        {
            get
            {
                if (instance == null)
                {
                    instance = new T();
                }
                return instance;
            }
        }
    }
}
