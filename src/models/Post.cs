using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Catedra_3_Backend.src.models
{
    public class Post
    {
        public string Title { get; set; } = "";
        public DateOnly PostDate { get; set; }
        public string Url { get; set; } = "";
        [ForeignKey("User")]
        public string UserId { get; set; } = "";
    }
}