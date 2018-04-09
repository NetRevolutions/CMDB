using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JARASOFT.CMDB.Core.Values;

namespace JARASOFT.CMDB.Core.Extensions
{
    public static class IntervalsListExtension
    {
        /// <summary>
        /// Tested
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <returns></returns>
        public static List<Interval<T>> Merge<T>(this List<Interval<T>> a) where T : IComparable<T>
        {
            var merged = new List<Interval<T>>();

            if (a.Count < 2) return a;

            var i = 0;

            Interval<T> it, current;

            current = a[i++];
            merged.Add(current);

            while (i < a.Count)
            {
                it = a[i];

                if (it.OverlapsWith(current))
                {
                    current.Start = Min(it.Start, current.Start);
                    current.End = Max(it.End, current.End);
                }
                else
                {
                    current = it;
                    merged.Add(current);
                }
                i++;
            }
            return merged;
        }

        /// <summary>
        /// Tested
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="main"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static List<Interval<T>> Intersect<T>(this List<Interval<T>> main, List<Interval<T>> other) where T : IComparable<T>
        {
            var newone = new List<Interval<T>>();

            var i = 0;
            var j = 0;

            Interval<T> it1, it2;

            while (i < main.Count && j < other.Count)
            {
                it1 = main[i];
                it2 = other[j];

                if (it1.OverlapsWith(it2))
                {
                    newone.Add(new Interval<T>(Max(it1.Start, it2.Start), Min(it1.End, it2.End)));
                }

                if (it1.End.CompareTo(it2.End) < 0)
                {
                    i++;
                }
                else
                {
                    j++;
                }
            }

            return newone;
        }

        /// <summary>
        /// Tested
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="main"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static List<Interval<T>> GetOverlapsWith<T>(this List<Interval<T>> main, List<Interval<T>> other) where T : IComparable<T>
        {
            var newone = new List<Interval<T>>();

            var i = 0;
            var j = 0;
            var last = -1;

            Interval<T> it1, it2;

            while (i < main.Count && j < other.Count)
            {
                it1 = main[i];
                it2 = main[j];

                if (it1.OverlapsWith(it2))
                {
                    if (last != i)
                    {
                        newone.Add(new Interval<T>(it1.Start, it1.End, it1.Id));
                        last = i;
                    }
                }

                if (it1.End.CompareTo(it2.End) < 0)
                {
                    i++;
                }
                else
                {
                    j++;
                }
            }

            return newone;
        }

        /// <summary>
        /// Tested
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static List<Interval<T>> Exclude<T>(this List<Interval<T>> a, List<Interval<T>> b) where T : IComparable<T>
        {
            var no_b = new List<Interval<T>>();

            if (a.Count == 0 || b.Count == 0) return a;

            var i = 0;
            var j = 0;

            Interval<T> ia, ib;

            while (i < a.Count && j < b.Count)
            {
                ia = a[i];
                ib = b[j];

                if (ia.OverlapsWith(ib))
                {
                    var start = Max(ia.Start, ib.Start);
                    var end = Min(ia.End, ib.End);

                    if (ia.Start.CompareTo(ib.Start) < 0)
                    {
                        no_b.Add(new Interval<T>(ia.Start, start));
                    }

                    if (ia.End.CompareTo(ib.End) > 0)
                    {
                        ia.Start = end;
                    }
                }
                else if (ia.End.CompareTo(ib.Start) < 0)
                {
                    no_b.Add(ia);
                }

                if (ia.End.CompareTo(ib.End) <= 0)
                {
                    i++;
                }
                else
                {
                    j++;
                }
            }

            while (i < a.Count)
            {
                no_b.Add(a[i++]);
            }

            return no_b;
        }

        public static List<Interval<T>> Negative<T>(this List<Interval<T>> main, Interval<T> scope) where T : IComparable<T>
        {
            var negative = new List<Interval<T>>();

            if (main.Count == 0)
            {
                negative.Add(scope);
                return negative;
            }

            if (main[0].Start.CompareTo(scope.Start) > 0)
                negative.Add(new Interval<T>(scope.Start, main[0].Start));

            int i = 0;

            for (; i < main.Count - 1; i++)
            {
                negative.Add(new Interval<T>(main[i].End, main[i + 1].Start));
            }

            if (main[i].End.CompareTo(scope.End) < 0)
                negative.Add(new Interval<T>(main[i].End, scope.End));

            return negative;
        }

        private static T Max<T>(T t1, T t2) where T : IComparable<T>
        {
            return t1.CompareTo(t2) > 0 ? t1 : t2;
        }

        private static T Min<T>(T t1, T t2) where T : IComparable<T>
        {
            return t1.CompareTo(t2) < 0 ? t1 : t2;
        }
    }
}
