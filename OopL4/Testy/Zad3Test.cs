using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zad3;

namespace Testy
{
    [TestClass]
    public class Zad3Test
    {
        #region Private fields

        private static HashSet<Plane> _inUse;

        #endregion
        #region Public static methods

        [ClassInitialize]
        public static void InitTests(TestContext context)
        {
            const int PLANES_N = 10;
            _inUse = new HashSet<Plane>(Enumerable.Range(0, PLANES_N).Select(_ => Airport.Instance.AcquirePlane()));
        }

        #endregion
        #region Public methods

        [TestInitialize]
        public void Init()
        {
            foreach (var plane in _inUse)
            {
                Airport.Instance.ReleasePlane(plane);
            }
        }

        [TestMethod]
        public void TestAcquirePlaneDistinct()
        {
            var p2 = Airport.Instance.AcquirePlane();
            var p1 = Airport.Instance.AcquirePlane();

            Assert.AreNotSame(p1, p2);
        }

        [TestMethod]
        [Timeout(250)]
        public void TestAcquireRefreshed()
        {
            var p1 = Airport.Instance.AcquirePlane();
            Assert.IsTrue(p1.Passengers == 0);
            p1.Passengers = 100;
            Airport.Instance.ReleasePlane(p1);

            var p2 = Airport.Instance.AcquirePlane();
            while (p1 != p2)
            {
                p2 = Airport.Instance.AcquirePlane();
            }
            Assert.IsTrue(p2.Passengers == 0);
        }

        [TestMethod]
        public void TestDoubleRelease()
        {
            var airport = Airport.Instance;
            var plane = airport.AcquirePlane();

            airport.ReleasePlane(plane);
            airport.ReleasePlane(plane);
        }

        [TestMethod]
        [ExpectedException(typeof (PlanePoolDepletedException))]
        public void TestPoolDepleted()
        {
            const int PLANES_N = 20;
            foreach (var i in Enumerable.Range(0, PLANES_N))
            {
                Airport.Instance.AcquirePlane();
            }
        }

        [TestMethod]
        [Timeout(250)]
        public void TestRelease()
        {
            var p1 = Airport.Instance.AcquirePlane();
            Airport.Instance.ReleasePlane(p1);

            var p2 = Airport.Instance.AcquirePlane();
            while (p1 != p2)
            {
                p2 = Airport.Instance.AcquirePlane();
            }
        }

        #endregion
    }
}