using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SZMALAPP.Models
{
    public class BigModel
    {
        public IEnumerable<zgloszenie> EventToShowModel { get; set; }
        public UserLoginModel UserLoginModel { get; set; }
    }
}