using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace zangaoApplication.Models
{
    public enum State
    {
        Delete=0,
        Normal=1,
        Verify=2,
        Forbidden=3,
        Expiration=4,
        Unknow=5
    }
}