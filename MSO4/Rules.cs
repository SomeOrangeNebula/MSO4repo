using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace MSO4
{
	interface Rules
	{
        Board Move(int hole, Board board, int player);  //changed Hole hole to int hole for ease of programming, added an int for the player because that allows for recursive moves
        bool PlayerSwapped { get; set; }                //turned this into a variable the game class can read instead of a method that had to access the game class
        List<int> AllowedMoves(Board board, int player); //made into a list of indexes for the holes.
        int CalculateWinners(Board board);
	}

    class WariRules : Rules
    {
        public bool PlayerSwapped { get; set; }
        public Board Move(int hole, Board board, int player)
        {
            PlayerSwapped = false;
            Hole[] holes = board.holes;
            int hand = holes[hole].stones;
            holes[hole].stones = 0;
            for(int i = hand; i > 0; i--)
            {
                if(hole > 1)
                {
                    hole--;
                    if (holes[hole].GetType() != typeof(SpecialHole))
                    { 
                        holes[hole].stones++; 
                    }
                }
                else
                {
                    hole = holes.Length - 1;
                    if (holes[hole].GetType() != typeof(SpecialHole)) 
                    { 
                        holes[hole].stones++; 
                    }
                }
            }
            if (holes[hole].stones == 2 || holes[hole].stones == 3)
            {
                if (holes[0].owner == player)
                {
                    holes[0].stones += holes[hole].stones;
                    holes[hole].stones = 0;
                }
                else
                {
                    holes[holes.Length / 2].stones += holes[hole].stones;
                    holes[hole].stones = 0;
                }
                PlayerSwapped = true;
                return new Board(holes);
            }
            else
            {
                PlayerSwapped = true;
                return new Board(holes);
            }
        }
        public List<int> AllowedMoves(Board board, int player)
        {
            List<int> opslag = new List<int>();
            for(int i = 0; i < board.holes.Length; i++)
            {
                if(board.holes[i].owner == player && board.holes[i].GetType() == typeof(BasicHole) && board.holes[i].stones > 0)
                {
                    opslag.Add(i);
                }
            }
            return opslag;
        }
        public int CalculateWinners(Board board)
        {
            if (board.holes[0].stones > board.holes[board.holes.Length / 2].stones)
            {
                return 1;
            }
            else if (board.holes[0].stones < board.holes[board.holes.Length / 2].stones)
            {
                return 2;
            }
            else            //tie
            {
                return 0;
            }
        }
    }
    class MankalaRules : Rules
    {
        public bool PlayerSwapped { get; set; }
        public Board Move(int hole, Board board, int player)
        {
            PlayerSwapped = false;
            Hole[] holes = board.holes;
            int hand = holes[hole].stones;
            holes[hole].stones = 0;
            for (int i = hand; i > 0; i--)
            {
                if (hole > 0)
                {
                    hole--;
                    holes[hole].stones++;
                }
                else
                {
                    hole = holes.Length - 1;
                    holes[hole].stones++;
                }
            }
            if(holes[hole].owner != player)
            {
                if(holes[hole].stones == 1)
                {
                    PlayerSwapped = true;
                    return new Board(holes);
                }
                else
                {
                    return Move(hole, new Board(holes), player);
                }
            }
            else if(holes[hole].GetType() == typeof(SpecialHole))
            {
                if(holes[hole].owner == player)
                {
                    return new Board(holes);
                }
                else
                {
                    PlayerSwapped = true;
                    return new Board(holes);
                }
            }
            else            //last stone dropped in own hole
            {
                if (holes[hole].stones > 1)
                {
                    return new Board(holes);
                }
                else if (holes[hole].stones == 1 && holes[holes.Length-hole].stones == 0)
                {
                    PlayerSwapped = true;
                    return new Board(holes);
                }
                else
                {
                    int tussenstand = holes[hole].stones;
                    tussenstand += holes[holes.Length - hole].stones;
                    holes[holes.Length - hole].stones = 0;
                    holes[hole].stones = 0;
                    if(holes[0].owner == player)
                    {
                        holes[0].stones += tussenstand;
                    }
                    PlayerSwapped = true;
                    return new Board(holes);
                }
            }
        }
        public List<int> AllowedMoves(Board board, int player)
        {
            List<int> opslag = new List<int>();
            for (int i = 0; i < board.holes.Length; i++)
            {
                if (board.holes[i].owner == player && board.holes[i].GetType() == typeof(BasicHole) && board.holes[i].stones > 0)
                {
                    opslag.Add(i);
                }
            }
            return opslag;
        }
        public int CalculateWinners(Board board)
        {
            if (board.holes[0].stones > board.holes[board.holes.Length / 2].stones)
            {
                return 1;
            }
            else if (board.holes[0].stones < board.holes[board.holes.Length / 2].stones)
            {
                return 2;
            }
            else            //tie
            {
                return 0;
            }
        }
    }


}
