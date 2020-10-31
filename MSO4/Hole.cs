using System;
using System.Collections.Generic;
using System.Text;

namespace MSO4
{
	interface Hole
	{
        int stones {  get; set;  }
        int owner { get; set;  }
	}

	class BasicHole : Hole
    {
        public int stones { get; set; }
        public int owner { get; set; }
        public BasicHole(int stones, int owner)
        {
            this.stones = stones;
            this.owner = owner;
        }
    }

    class SpecialHole : Hole
    {
        public int stones { get; set; }
        public int owner { get; set; }
        public SpecialHole(int stones, int owner)
        {
            this.stones = stones;
            this.owner = owner;
        }
    }

}
