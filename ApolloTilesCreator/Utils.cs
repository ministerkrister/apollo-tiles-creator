using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApolloTilesCreator
{
    internal static class Utils
    {
        internal static bool IsValidURI(string uri)
        {
            Uri tmp = null;
            if (!Uri.IsWellFormedUriString(uri, UriKind.Absolute))
                return false;
            Uri.TryCreate(uri, UriKind.Absolute, out tmp);
            return tmp.Scheme == Uri.UriSchemeHttp || tmp.Scheme == Uri.UriSchemeHttps;
        }
    }
}
