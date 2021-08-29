using System;

namespace Cobrakai.GWTUnit
{
    public class WhenResult<TCtx, TRes>
    {
        public TCtx Context { get; set; }
        public TRes Result { get; set; }
        public void Then(Action<TRes> call)
        {
            call(Result);
        }

        public void Then(Action<TCtx, TRes> call)
        {
            call(Context, Result);
        }
        
        public void Then(Action call)
        {
            call();
        }
    }
    
    public class WhenResult<TCtx>
    {
        public TCtx Context { get; set; }

        public void Then(Action<TCtx> call)
        {
            call(Context);
        }
    }
}