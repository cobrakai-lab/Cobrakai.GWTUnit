using System;

namespace Cobrakai.GWTUnit
{
    public class GivenContext<TCtx>
    {
        public TCtx Context { get; set; }

        public WhenResult<TCtx, TRes> When<TRes>(Func<TCtx, TRes> call)
        {
            WhenResult<TCtx, TRes> result = new WhenResult<TCtx, TRes>
            {
                Result = call(this.Context),
                Context = Context
            };
            return result;
        }

        public WhenResult<TCtx> When(Action<TCtx> call)
        {
            call(Context);
            return new WhenResult<TCtx>
            {
                Context = Context
            };
        }
    }
}