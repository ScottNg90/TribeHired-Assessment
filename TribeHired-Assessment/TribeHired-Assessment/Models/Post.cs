using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TribeHired_Assessment.Models
{
    public class Post
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
