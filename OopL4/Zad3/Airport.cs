using System.Collections.Generic;
using System.Linq;

namespace Zad3
{
    public class Airport
    {
        #region Constants

        private const int PLANES_LIMIT = 10;

        #endregion
        #region Private fields

        private static HashSet<Plane> _inUse;

        private static readonly Airport _instance = new Airport();
        private static Plane[] _planes;

        #endregion
        #region Properties
        

        public static Airport Instance
        {
            get { return _instance; }
        }

        #endregion
        #region Ctors

        private Airport()
        {
            _planes = Enumerable.Range(0, PLANES_LIMIT).Select(i => new Plane(i)).ToArray();
            _inUse = new HashSet<Plane>();
        }

        #endregion
        #region Public methods

        public Plane AcquirePlane()
        {
            var free = _planes.Except(_inUse).FirstOrDefault();
            if (free != null)
            {
                RefreshPlane(free);
                _inUse.Add(free);
                return free;
            }

            throw new PlanePoolDepletedException();
        }

        public void ReleasePlane(Plane plane)
        {
            _inUse.Remove(plane);
        }

        private static void RefreshPlane(Plane plane)
        {
            plane.Passengers = 0;
        }

        #endregion
    }
}