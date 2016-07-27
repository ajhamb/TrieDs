using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;

namespace TrieDs.Entities
{
    public class TrieNode<T>
    {
        public TrieNode<T> Parent { get; set; }

        private List<TrieNode<T>> _children;
        private List<T> _valueList;

        public char Data;

        public int Depth;

        public List<T> ValueList
        {
            get
            {
                return _valueList ?? (_valueList = new List<T>());
            }
            set
            {
                ValueList.AddRange(value);
            }
        }

        public List<TrieNode<T>> Children
        {
            get
            {
                return _children ?? (_children = new List<TrieNode<T>>());
            }
            set
            {
                Children.AddRange(value);
            }
        }

        internal bool HasChild(char c)
        {
            return Children.Any(s => s.Data.Equals(char.ToLower(c)));
        }

        internal void AddChild(char c)
        {
            Children.Add(new TrieNode<T>(char.ToLower(c), Depth + 1, this));
        }

        internal TrieNode<T> GetChild(char c)
        {
            return Children.FirstOrDefault(s => s.Data == char.ToLower(c));
        }

        private TrieNode(char data, int depth, TrieNode<T> parent)
        {
            Data = char.ToLower(data);
            Depth = depth;
            Parent = parent;
        }

        public static TrieNode<T> InitializeRoot()
        {
            return new TrieNode<T>('$',0,null);
        }

        internal IEnumerable<T> GetAll()
        {
            foreach (T value in ValueList)
            {
                yield return value;
            }

            foreach (var trieNode in Children)
            {
                foreach (var s in trieNode.GetAll())
                {
                    yield return s;
                }
            }
        }
    }
}
