using System.Collections;

namespace DLinkedList
{
    public class DLinkedList<TKey,TValue> :IEnumerable<TValue>, IEnumerator<TValue>
    {
        public DLinkedList() { }
        public class ListEntry {
            internal ListEntry? Next { get; set; }
            internal ListEntry? Previous { get; set; }
            public TKey Key { get; set; } = default!;
            public TValue Value { get; set; } = default!;
        }
        ListEntry? First { get; set; }
        ListEntry? Last { get; set; }
        public int Count { get; set; } = 0;
        ListEntry? CurrentEntry { get; set; }
        public TValue Current { get; set; }
        object IEnumerator.Current { get => Current ?? default!; }

        public void Add(TKey key, TValue value) {
            ListEntry entry = new ListEntry() { Key = key, Value = value };
            if (First == null) {
                First = entry;
                Last = entry;
            } else {
                if(Last == null) { throw new Exception("Unhandled error in list"); }
                Last.Next = entry;
                entry.Previous = Last;
                Last = entry;
            }
            Count++;
        }
        public bool Remove(TKey key)
        {
            var item = Get(key);
            if(item != null)
            {
                if(item.Previous != null) {
                    item.Previous.Next = item.Next;
                }
                if(item.Next != null) {
                    item.Next.Previous = item.Previous;
                }
                if(First== item) {
                    First = item.Next;
                }
                if(Last== item) {
                    Last = item.Previous;
                }
                Count--;
                return true;
            }
            return false;
        }
        protected ListEntry? Get(TKey key)
        {
            var current = First;
            while(current != null && key?.Equals(current.Key)==false)
            {
                current = current.Next;
            }
            return current;
        }
        public void Clear()
        {
            First = null;
            Last = null;
            Count = 0;
            Reset();
        }
        public List<TValue> Find(Func<TValue,bool> predicate) {
            List<TValue> list = [];
            var current= First;
            while(current != null) {
                if(predicate(current.Value))
                {
                    list.Add(current.Value);
                }
                current = current.Next;
            }
            return list;
        }

        public IEnumerator<TValue> GetEnumerator()  => this;
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public bool MoveNext()
        {
            CurrentEntry = CurrentEntry == null ? First : CurrentEntry?.Next;
            if(CurrentEntry == null) return false;
            Current = CurrentEntry.Value;
            return true;
        }

        public void Reset()
        {
            CurrentEntry = First;
            Current = First != null ? First.Value : default!;
        }

        public void Dispose()
        {
            
        }
    }
}
