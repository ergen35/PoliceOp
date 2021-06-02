using System;
using System.Collections.Generic;
using System.Text;
using LazyCache;

namespace PoliceOp.OpCenter.AppLevel
{
    public static class CachingService
    {
        public static IAppCache appCache = new LazyCache.CachingService();
    }
}
