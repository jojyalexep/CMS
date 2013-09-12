using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.CMS.Infrastructure.ExternalServices.Utility
{
    public static class WSReponseHandler
    {
        public static void Handle(string response)
        {
            response = response.ToLower();
            if (response.Contains("error") || String.IsNullOrEmpty(response))
            {
                throw new ArgumentException(response);
            }
        }
    }
}
