using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARASOFT.CMDB.Core.Values
{
    public class Interval<T> where T : IComparable<T>
    {
        public T Start;
        public T End;
        public long Id;

        public Interval(T start, T end, long id = 0)
        {
            if (start.CompareTo(end) >= 0)
            {
                throw new ArgumentException("the start value of the interval must be smaller than the end value. null interval are not allowed");
            }

            this.Start = start;
            this.End = end;
            this.Id = id;
        }

        public bool OverlapsWith(Interval<T> other)
        {
            return this.Start.CompareTo(other.End) <= 0 && this.End.CompareTo(other.Start) >= 0;
        }

        public Interval<T> Intersection(Interval<T> other)
        {
            return new Interval<T>(this.Start.CompareTo(other.Start) < 0 ? other.Start : this.Start,
                this.End.CompareTo(other.End) < 0 ? this.End : other.End);
        }

        public override string ToString()
        {
            return string.Format("[{0}, {1}]", this.Start.ToString(), this.End.ToString());
        }
    }
}
