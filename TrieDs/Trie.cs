using System;
using System.Collections.Generic;
using TrieDs.Entities;

namespace TrieDs
{
    public class Trie<T> : ITrie<T>
    {
        protected Entities.TrieNode<T> TrieRootNode;        

        public Trie()
        {
            TrieRootNode = TrieNode<T>.InitializeRoot();
        }

        public virtual bool AddData(string data, T value)
        {
            Entities.TrieNode<T> currentNode = TrieRootNode;

            foreach(var c in data)
            {
                if(!currentNode.HasChild(c))
                {
                    currentNode.AddChild(c);
                }

                currentNode = currentNode.GetChild(c);

            }

            currentNode.ValueList.Add(value);

            return true;
        }

        public virtual bool RemoveData(string data)
        {
            TrieNode<T> currentNode = SearchExact(data);
            currentNode?.ValueList.Clear();

            while (currentNode != null && currentNode.Children.Count == 0)
            {
                TrieNode<T> tempNode = currentNode.Parent;
                tempNode.Children.RemoveAll(s => s.Data == currentNode.Data);
                currentNode = tempNode;

            }
            return true;
        }

        public TrieNode<T> SearchExact(string data)
        {
            Entities.TrieNode<T> currentNode = TrieRootNode;
            int i = 0;
            for (; i < data.Length; i++)
            {
                if (currentNode.HasChild(data[i]))
                {
                    currentNode = currentNode.GetChild(data[i]);
                }
                else
                {
                    break;
                }
            }

            if(i == data.Length)
            {
                return currentNode;
            }

            return null;
        }

        public IEnumerable<T> Search(string data)
        {
            Entities.TrieNode<T> currentNode = SearchExact(data);
            
            if (currentNode != null)
            {
                foreach (T value in currentNode.GetAll())
                {
                    yield return value;
                }
            }
        }
    }
}
