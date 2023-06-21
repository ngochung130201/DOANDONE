using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Share.ViewModels
{
    public class EmailVM
    {
        public string To { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Name {get;set;} = string.Empty;
        
    }
}