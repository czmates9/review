using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.InterfaceClass
{
    interface IKonifig
    {
        // Property signatures:
        int X
        {
            get;
            set ;
        }

        int Y
        {
            get;
            set;
        }

        
        double Distance
        {
            get;
        }

        void SampleMethod();

    }

    class ImplementationKonifigInterface : IKonifig
    {

        private object _data = null;
        private double _dist;

        // Constructor:
        /// <summary>
        /// C'tor -- velikost okna
        /// </summary>
        /// <param name="x"> sirka okna</param>
        /// <param name="y"> vyska okna</param>
        public ImplementationKonifigInterface(int x, int y)
        {
            X = x;
            Y = y;
        }

        // Property implementation:
        public int X { get; set; }

        public int Y { get; set; }

        // Property implementation
        public double Distance =>
           Math.Sqrt(X * X + Y * Y);


        static void Main()
        {
            // Declare an interface instance.
           // IKonifigInterface obj = new ImplementationKonifigInterface();

            // Call the member.
            //obj.SampleMethod();
        }

        public void SampleMethod()
        {
            throw new NotImplementedException();
        }
    }
}
