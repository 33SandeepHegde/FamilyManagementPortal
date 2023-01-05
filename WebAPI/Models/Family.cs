using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class Family
    {
        public int SerialId { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }
        public string DOB { get; set; }
        public string PhotoFileName { get; set; }

    }
}