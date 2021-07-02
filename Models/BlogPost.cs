using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class BlogPost
    {
        public int id { get; set; } = -1;
        public string title { get; set; }
        public string imgurl { get; set; }
        public string content { get; set; }
        public string author { get; set; }
        public int readCount { get; set; } = 0;
        public string[] tags { get; set; }

    }
    
}
