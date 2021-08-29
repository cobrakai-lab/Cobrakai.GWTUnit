using System;

namespace Cobrakai.GWTUnit
{
    public static class GWTUnit 
    {
        public static GivenContext<T> Given<T>(Func<T> setupCall)
        {
            GivenContext<T> context = new GivenContext<T>()
            {
                Context = setupCall()
            };
            return context;
        }
        
        public static void Should(string description, Action action)
        {
            action();
        }
    }

   
}