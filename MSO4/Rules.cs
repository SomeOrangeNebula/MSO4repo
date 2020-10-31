using System;
using System.Collections.Generic;
using System.Text;

namespace MSO4
{
	interface Rules
	{
        Board Move();
        bool PlayerSwapped();
        Hole[] AllowedMoves();
	}
}
