using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Share.ViewModels
{
    public class FormatExport
    {
            public byte[] FileContext { get; set; }
            public string ContextType { get; set; }
            public string DownloadName { get; set; }
    }
}