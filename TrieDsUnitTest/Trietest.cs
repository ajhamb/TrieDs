
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using NUnit.Framework;
using TrieDs;

namespace TrieDsUnitTest
{
    [TestFixture]
    public class Trietest
    {
        [Test]
        public void TestTrieAddAndSearch()
        {
            string[] Words = { "Amit", "Rohit", "Rohita" };

            ITrie<string> trie = new SuffixTrie<string>();

            foreach (string s in Words)
            {
                trie.AddData(s.ToLower(), s);
            }

            var data = trie.Search("hi");

            var k = data.ToList();

            data = trie.Search("mi");

            k = data.ToList();

            var node = trie.SearchExact("hi");

        }

        [Test]
        public void TestTrieAddAndSearchWithFileData()
        {
            string startupPath = System.AppDomain.CurrentDomain.BaseDirectory;
            var pathItems = startupPath.Split(Path.DirectorySeparatorChar);
            string projectPath = String.Join(Path.DirectorySeparatorChar.ToString(), pathItems.Take(pathItems.Length - 2));
            string pathtoUse = Path.Combine(projectPath, "TestData.txt");

            string[] Lines = File.ReadAllText(pathtoUse).Split(' ');

            int Upper = 100000;

            Stopwatch swStopwatch = new Stopwatch();

            swStopwatch.Start();

            ITrie<string> trie = new SuffixTrie<string>();

            foreach (string s in Lines)
            {
                trie.AddData(s.ToLower(), s);
            }

            for (int i = 0; i < Upper; i++)
            {
                var data = trie.Search("pop");
                var k = data.ToList();
            }


            swStopwatch.Stop();
            long time1 = swStopwatch.ElapsedMilliseconds;

            swStopwatch.Reset();
            swStopwatch.Start();

            for (int i = 0; i < Upper; i++)
            {
                var data = Lines.Where(s => s.Contains("pop")).ToList();
            }


            swStopwatch.Stop();
            long time2 = swStopwatch.ElapsedMilliseconds;

            Assert.IsTrue(time1 < time2);

        }
    }
}
