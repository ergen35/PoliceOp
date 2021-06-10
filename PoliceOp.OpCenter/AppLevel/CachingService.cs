using System;
using System.Collections.Generic;
using System.Text;
using LazyCache;
using PoliceOp.OpCenter.Services;

namespace PoliceOp.OpCenter.AppLevel
{
    public static class CachingService
    {
        public static IAppCache appCache = new LazyCache.CachingService();

    }

    public static class JWTAuthServices
    {
        public static JWTServices jwtSvc = new JWTServices();
    }
}
