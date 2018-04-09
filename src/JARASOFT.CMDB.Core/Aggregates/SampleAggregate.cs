using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARASOFT.CMDB.Core.Aggregates
{
    public class SampleAggregate
    {
        public string State;
        public SampleAggregate(string State)
        {
            this.State = State;
        }

        public string Execute()
        {
            if (string.IsNullOrEmpty(this.State))
            {
                throw new Exception("fail");
            }
            return this.State;
        }
    }
}
