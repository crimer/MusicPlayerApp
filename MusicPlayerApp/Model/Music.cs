using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayerApp.Model
{
    public class Music
    {
        public string Title { get; set; }
        public string Band { get; set; }
        public string Url { get; set; }
        public string CoverImage { get; set; }
        public bool IsRecent { get; set; }
    }
}
