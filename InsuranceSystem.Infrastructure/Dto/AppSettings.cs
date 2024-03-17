using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceSystem.Infrastructure.Dto
{
    public class AppSettings
    {
        public string? ClientId { get; set; }
        public string? ClientKey { get; set; }
        public string? IV { get; set; }
        public string AES_KEY { get; set; }
        public string ASE_IV { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Secret { get; set; }
        public string LogPath { get; set; }
        public string DbConnection { get; set; }
    }
}
