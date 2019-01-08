using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using lab_1;


namespace TestTList
{
    [TestClass]
    public class UnitTest1
    {
        private string ListToString<T>(T list) where T : System.Collections.Generic.IList<string>
        {
            return String.Join("", list);
        }

        private T ListFromString<T>(string str) where T : System.Collections.Generic.IList<string>, new()
        {
            T result = new T();
            foreach (var c in str)
            {
                result.Add(c.ToString());
            }
            return result;
        }

        [TestMethod]
        public void TestAdd()
        {
            var list = new lab_1.TList<string>();

            list.Add("a");
            Assert.AreEqual(ListToString(list), "a");

            list.Add("b");
            Assert.AreEqual(ListToString(list), "ab");

            list.Add("c");
            Assert.AreEqual(ListToString(list), "abc");
        }

        [TestMethod]
        public void TestInsert()
        {
            var list = new lab_1.TList<string>();

            list.Insert(0, "a");
            Assert.AreEqual(ListToString(list), "a");

            list.Insert(0, "b");
            Assert.AreEqual(ListToString(list), "ba");

            list.Insert(0, "c");
            Assert.AreEqual(ListToString(list), "cba");

            list.Insert(1, "d");
            Assert.AreEqual(ListToString(list), "cdba");

            list.Insert(3, "e");
            Assert.AreEqual(ListToString(list), "cdbea");

            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => list.Insert(-1, "a"));

            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => list.Insert(6, "a"));
        }

        [TestMethod]
        public void TestRemove()
        {
            var str = "ababcdeb";
            var origin_list = ListFromString<System.Collections.Generic.List<string>>(str);
            var list = ListFromString<lab_1.TList<string>>(str);

            Assert.AreEqual(origin_list.Remove("a"), list.Remove("a"));
            Assert.AreEqual(ListToString(origin_list), ListToString(list));

            Assert.AreEqual(origin_list.Remove("a"), list.Remove("a"));
            Assert.AreEqual(ListToString(origin_list), ListToString(list));

            Assert.AreEqual(origin_list.Remove("c"), list.Remove("c"));
            Assert.AreEqual(origin_list.Remove("e"), list.Remove("e"));
            Assert.AreEqual(origin_list.Remove("d"), list.Remove("d"));
            Assert.AreEqual(origin_list.Remove("f"), list.Remove("f"));
            Assert.AreEqual(ListToString(origin_list), ListToString(list));

            for (int i = 0; i < 4; i++)
            {
                Assert.AreEqual(origin_list.Remove("b"), list.Remove("b"));
                Assert.AreEqual(ListToString(origin_list), ListToString(list));
            }
        }

        [TestMethod]
        public void TestRemoveAt()
        {
            var str = "abcdefg";
            var origin_list = ListFromString<System.Collections.Generic.List<string>>(str);
            var list = ListFromString<lab_1.TList<string>>(str);

            origin_list.RemoveAt(0);
            list.RemoveAt(0);
            Assert.AreEqual(ListToString(origin_list), ListToString(list));

            origin_list.RemoveAt(1);
            list.RemoveAt(1);
            Assert.AreEqual(ListToString(origin_list), ListToString(list));

            origin_list.RemoveAt(2);
            list.RemoveAt(2);
            Assert.AreEqual(ListToString(origin_list), ListToString(list));

            origin_list.RemoveAt(3);
            list.RemoveAt(3);
            Assert.AreEqual(ListToString(origin_list), ListToString(list));

            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => list.RemoveAt(-1));

            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => list.RemoveAt(3));
        }

        [TestMethod]
        public void TestCount()
        {
            var list = new lab_1.TList<string>();
            
            for (int i = 0; i < 5; i++)
            {
                Assert.AreEqual(i, list.Count);
                list.Add("a");
            }

            for (int i = 4; i >= 0; i--)
            {
                list.Remove("a");
                Assert.AreEqual(i, list.Count);
            }
        }

        [TestMethod]
        public void TestClear()
        {
            var list = new lab_1.TList<string>();

            for (int i = 0; i < 5; i++)
            {
                list.Add("a");
            }

            list.Clear();
            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void TestIndexOf()
        {
            var str = "abcde";
            var list = ListFromString<lab_1.TList<string>>(str);

            str = str.Insert(0, "0");
            for (int i = 0; i < str.Length; i++)
            {
                Assert.AreEqual(list.IndexOf(str[i].ToString()), i - 1);

            }
        }

        [TestMethod]
        public void TestContains()
        {
            var str = "abacdb";
            var list = ListFromString<lab_1.TList<string>>(str);
            foreach (var c in new System.Collections.Generic.HashSet<char>(str))
            {
                Assert.IsTrue(list.Contains(c.ToString()));
            }
            Assert.IsFalse(list.Contains("e"));
        }

        [TestMethod]
        public void TestGetItem()
        {
            var str = "abcde";
            var list = ListFromString<lab_1.TList<string>>(str);

            for (int i = 0; i < str.Length; i++)
            {
                Assert.AreEqual(list[i], str[i].ToString());

            }
        }

        [TestMethod]
        public void TestCopyTo()
        {
            var list = ListFromString<lab_1.TList<string>>("abcd");

            Assert.ThrowsException<ArgumentNullException>(
                () => list.CopyTo(null, 0));

            Assert.ThrowsException<ArgumentException>(
                () => {
                    var arr = new string[2];
                    list.CopyTo(arr, 0);
                });

            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => {
                    var arr = new string[4];
                    list.CopyTo(arr, -1);
                });

            string[] array = "012345".Select(c => c.ToString()).ToArray();
            list.CopyTo(array, 1);
            Assert.AreEqual(
                string.Join("", array),
                "0abcd5"
            );
        }
    }
}
