using System.Collections.Generic;
using TrieDs.Entities;

namespace TrieDs
{
    public interface ITrie<T>
    {
        IEnumerable<T> Search(string data);

        bool AddData(string data, T value);

        bool RemoveData(string data);

        TrieNode<T> SearchExact(string data);
    }
}
