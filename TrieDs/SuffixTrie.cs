using System.Collections.Generic;
using TrieDs.Entities;

namespace TrieDs
{
    public class SuffixTrie<T> : Trie<T>
    {
        public override bool AddData(string data, T value)
        {
            Queue<TrieNode<T>> treeNodeQueue = new Queue<TrieNode<T>>();
            treeNodeQueue.Enqueue(TrieRootNode);
            
            for (int i = 0 ; i < data.Length ; i++)
            {
                Queue<TrieNode<T>> treeNodeQueueTemp = new Queue<TrieNode<T>>();
                treeNodeQueueTemp.Enqueue(TrieRootNode);

                while (treeNodeQueue.Count > 0)
                {
                    var node = treeNodeQueue.Dequeue();

                    if (!node.HasChild(data[i]))
                    {
                        node.AddChild(data[i]);
                    }

                    treeNodeQueueTemp.Enqueue(node.GetChild(data[i]));

                    if (i == data.Length - 1)
                    {
                        if (node != TrieRootNode)
                        {
                            node.ValueList.Add(value);
                        }
                    }
                }

                treeNodeQueue = treeNodeQueueTemp;
            }

            return true;
        }

        public override bool RemoveData(string data)
        {
            // TODO Need to implement
            return true;
        }
    }
}
