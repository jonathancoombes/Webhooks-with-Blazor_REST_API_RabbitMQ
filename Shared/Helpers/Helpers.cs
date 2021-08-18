using System;

namespace SpaceXAirlines.Shared.Helpers
{
    public class Helpers: IHelpers
    {
        bool IHelpers.UrlIsValid(string url)
        {
            return (Uri.IsWellFormedUriString(url, UriKind.Absolute));
        }
    }

   
}
