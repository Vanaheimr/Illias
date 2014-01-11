
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace eu.Vanaheimr.Illias.Commons.Collections
{

    // HashSet is basically implemented as a reduction of Dictionary<T, Boolean>
    // 
    public class HashSet<T> : IEnumerable<T>
    {

        private Dictionary<T, Boolean> InternalDictionary;


        public UInt64 Count
        {
            get
            {
                return (UInt64) InternalDictionary.LongCount();
            }
        }

        public HashSet()
        {
            this.InternalDictionary = new Dictionary<T, Boolean>();
        }

        public HashSet(IEnumerable<T> collection)
        {
        }

        //ToDo: Implement me!
        public HashSet(IEqualityComparer<T> Comparer)
        {
        }

        //ToDo: Implement me!
        public HashSet(IEnumerable<T> collection, IEqualityComparer<T> Comparer)
        {
        }

        public Boolean Add(T Item)
        {

            if (!InternalDictionary.ContainsKey(Item))
            {
                InternalDictionary.Add(Item, true);
                return true;
            }

            return false;

        }

        public void Clear()
        {
            InternalDictionary.Clear();
        }

        public Boolean Contains(T Item)
        {
            return InternalDictionary.ContainsKey(Item);
        }

        public Boolean Remove(T Item)
        {
            return InternalDictionary.Remove(Item);
        }

        public UInt64 RemoveWhere(Predicate<T> Match)
        {

            if (Match == null)
                throw new ArgumentNullException("match");

            var Candidates = new List<T>();

            foreach (var item in this)
                if (Match(item))
                    Candidates.Add(item);

            foreach (var item in Candidates)
                Remove(item);

            return (UInt64) Candidates.Count;

        }


        // set operations

        public void IntersectWith(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            var other_set = ToSet(other);

            RemoveWhere(item => !other_set.Contains(item));
        }

        public void ExceptWith(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            foreach (var item in other)
                Remove(item);
        }

        public bool Overlaps(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            foreach (var item in other)
                if (Contains(item))
                    return true;

            return false;
        }

        public bool SetEquals(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            var other_set = ToSet(other);

            if ((UInt64) InternalDictionary.LongCount() != other_set.Count)
                return false;

            foreach (var item in this)
                if (!other_set.Contains(item))
                    return false;

            return true;
        }

        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            foreach (var item in ToSet(other))
                if (!Add(item))
                    Remove(item);
        }

        HashSet<T> ToSet(IEnumerable<T> enumerable)
        {

            var set = enumerable as HashSet<T>;

            if (set == null)
                set = new HashSet<T>(enumerable);

            return set;

        }

        public void UnionWith(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            foreach (var item in other)
                Add(item);
        }

        bool CheckIsSubsetOf(HashSet<T> other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            foreach (var item in this)
                if (!other.Contains(item))
                    return false;

            return true;
        }

        public bool IsSubsetOf(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            if (InternalDictionary.Count == 0)
                return true;

            var other_set = ToSet(other);

            if ((UInt64) InternalDictionary.LongCount() > other_set.Count)
                return false;

            return CheckIsSubsetOf(other_set);
        }

        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            if (InternalDictionary.Count == 0)
                return true;

            var other_set = ToSet(other);

            if ((UInt64) InternalDictionary.LongCount() >= other_set.Count)
                return false;

            return CheckIsSubsetOf(other_set);
        }

        bool CheckIsSupersetOf(HashSet<T> other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            foreach (var item in other)
                if (!Contains(item))
                    return false;

            return true;
        }

        public bool IsSupersetOf(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            var other_set = ToSet(other);

            if ((UInt64) InternalDictionary.LongCount() < other_set.Count)
                return false;

            return CheckIsSupersetOf(other_set);
        }

        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            var other_set = ToSet(other);

            if ((UInt64) InternalDictionary.LongCount() <= other_set.Count)
                return false;

            return CheckIsSupersetOf(other_set);
        }






        public IEnumerator<T> GetEnumerator()
        {
            return InternalDictionary.Keys.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return InternalDictionary.Keys.GetEnumerator();
        }



    }


}
