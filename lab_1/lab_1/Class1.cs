using System;

namespace lab_1
{
    public class TList<T> : System.Collections.Generic.IList<T>
    {
        protected Node head;
        protected Node tail;
        protected int size;

        protected class Node
        {
            public Node next;
            private T data;

            public Node(T t)
            {
                next = null;
                data = t;
            }

            public Node Next
            {
                get { return next; }
                set { next = value; }
            }

            public T Data
            {
                get { return data; }
                set { data = value; }
            }
        }

        public TList()
        {
            Clear();
        }

        private Node getAtIndex(int index)
        {
            int curIndex = 0;
            Node cur = head;
            while (curIndex != index)
            {
                cur = cur.Next;
                curIndex++;
            }
            return cur;
        }

        public void Clear()
        {
            head = tail = null;
            size = 0;
        }

        public int Count => size;

        public int IndexOf(T value)
        {
            int res = 0;
            var cur = head;
            while (cur != null)
            {
                if (cur.Data.Equals(value))
                {
                    return res;
                }
                cur = cur.Next;
                res++;
            }
            return -1;
        }

        public bool Contains(T value)
        {
            return IndexOf(value) != -1;
        }

        public void Add(T value)
        {
            Insert(size, value);
        }

        public void Insert(int index, T value)
        {
            if (index < 0 || index > size)
            {
                throw new ArgumentOutOfRangeException("index");
            }

            var newNode = new Node(value);
            if (size == 0)
            {
                head = tail = newNode;
            }
            else if (index == 0)
            {
                newNode.Next = head;
                head = newNode;
            }
            else if (index == size)
            {
                tail.Next = newNode;
                tail = newNode;
            }
            else
            {
                var prevNode = head;
                int nextIndex = 1;
                while (nextIndex != index)
                {
                    prevNode = prevNode.Next;
                    nextIndex++;
                }
                newNode.Next = prevNode.Next;
                prevNode.Next = newNode;
            }
            size++;
        }

        public bool Remove(T value)
        {
            int index = IndexOf(value);
            if (index != -1)
            {
                RemoveAt(index);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= size)
            {
                throw new ArgumentOutOfRangeException("index");
            }

            if (index == 0)
            {
                head = head.Next;
            }
            else
            {
                var prevNode = head;
                int nextIndex = 1;
                while (nextIndex != index)
                {
                    prevNode = prevNode.Next;
                    nextIndex++;
                }
                prevNode.Next = prevNode.Next.Next;
            }

            size--;
        }

        public bool IsFixedSize
        {
            get
            {
                return false;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public T this[int index]
        {
            get
            {
                return getAtIndex(index).Data;
            }
            set
            {
                getAtIndex(index).Data = value;
            }
        }

        public System.Collections.Generic.IEnumerator<T> GetEnumerator()
        {
            Node current = head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }

            if (array.Length - arrayIndex < size)
            {
                throw new ArgumentException();
            }

            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException("arrayIndex");
            }

            foreach (var nodeData in this)
            {
                array[arrayIndex] = nodeData;
                arrayIndex++;
            }
        }
    }
}
