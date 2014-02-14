using System;

namespace NHarvestApi
{
    class DefaultResourcePathFactory : IResourcePathFactory
    {
        public string WhoAmI()
        {
            return "account/who_am_i";
        }

        // TODO: add rest of uris
    }
}