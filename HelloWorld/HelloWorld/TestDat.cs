using System;
using System.Collections.Generic;
using System.Text;

namespace HelloWorld
{
    class TestDat
    {
        private int id = 0;
        public TestDat(int id)
        {
            this.id = id;
        }

        public int getId()
        {
            return this.id;
        }
    }
}
