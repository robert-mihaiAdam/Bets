using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bets.Constants
{
    static class Constants
    {
        public const long noEntities = 10;
        public const long noBets = 20;
        public const long noClientBets = 10;
        public const string connection_data = "Server=localhost;Database=DotNetCourseDatabase;TrustServerCertificate=true;Trusted_Connection=true;";

        public enum BetsOptions { 
            A = 'A',
            B = 'B',
            X = 'X'
        }

    }
}
