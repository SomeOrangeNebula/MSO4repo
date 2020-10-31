using System;
using System.Collections.Generic;
using System.Text;

namespace MSO4
{
    class Boardfactory
    {
        public Board MakeBoard(string type, int holes, int stones)
        {
            Hole[] holeArray = new Hole[(2 * holes)];
            for(int i = 0; i < holes; i++)
            {
                holeArray[i] = new BasicHole(stones, 1);
            }
            for(int i = holes; i < 2*holes; i++)
            {
                holeArray[i] = new BasicHole(stones, 2);
            }
            if(type == "Wari")
            {
                return MakeWari(holeArray);
            }
            else //if(type == "Mankala"), commented out to ensure the if statement always returns a board, 
                 //in case of more gamemodes you can add them and create varying logic for the specialholes in new methods to keep the main MakeBoard clean.
            {
                return MakeMankala(holeArray, stones);
            }
        }
        //Added two functions for the creation of the specialholes, allowing for 
        private Board MakeWari(Hole[] holes)
        {
            holes[0] = new SpecialHole(0, 1);
            holes[holes.Length / 2] = new SpecialHole(0, 2);
            return new Board(holes);
        }
        private Board MakeMankala(Hole[] holes, int stones)
        {
            holes[0] = new SpecialHole(stones, 1);
            holes[holes.Length/2] = new SpecialHole(stones, 2);
            return new Board(holes);
        }
    }
}
