using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testy
{
    [TestClass]
    public class Zad1Test
    {
        [TestMethod]
        public void TestNormalSingletonInstanceEquality()
        {
            var s1 = Zad1.Singleton.Instance;
            var s2 = Zad1.Singleton.Instance;

            Assert.AreSame(s1, s2);
        }

        [TestMethod]
        public void TestNormalSingletonThreadsEquality()
        {
            var s1 = Zad1.Singleton.Instance;
            Zad1.Singleton s2 = null;

            var thread = new Thread(() =>
                {
                    s2 = Zad1.Singleton.Instance;
                });

            thread.Start();
            thread.Join();

            Assert.AreSame(s1, s2);
        }

        [TestMethod]
        public void TestThreadSingletonEquality()
        {
            var s1 = Zad1.ThreadSingleton.Instance;
            var s2 = Zad1.ThreadSingleton.Instance;            

            Assert.AreSame(s1, s2);
        }

        [TestMethod]
        public void TestThreadSingletonNotEquality()
        {
            var s1 = Zad1.ThreadSingleton.Instance;
            Zad1.ThreadSingleton s2 = null;

            var thread = new Thread(() =>
            {
                s2 = Zad1.ThreadSingleton.Instance;                
            });

            thread.Start();
            thread.Join();

            Assert.AreNotSame(s1, s2);
        }

        [TestMethod]
        public void TestLifespanSingletonEquality()
        {
            var s1 = Zad1.LifespanSingleton.Instance;
            var s2 = Zad1.LifespanSingleton.Instance;

            Assert.AreSame(s1, s2);
        }

        [TestMethod]
        public void TestLifespanSingletonNotEquality()
        {
            const int SINGLETON_LIFESPAN_SEC = 6000;

            var s1 = Zad1.LifespanSingleton.Instance;
            var s2 = Zad1.LifespanSingleton.Instance;

            Thread.Sleep(SINGLETON_LIFESPAN_SEC);
            var s3 = Zad1.LifespanSingleton.Instance;

            Assert.AreSame(s1, s2);
            Assert.AreNotSame(s1, s3);
            Assert.AreNotSame(s2, s3);
        }
    }
}
