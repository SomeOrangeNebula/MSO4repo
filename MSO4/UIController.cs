using System;
using System.Collections.Generic;
using System.Text;

namespace MSO4
{
	class UIController
	{

		public UIController()
		{
			GreetPlayer();
		}


		//An extra function we added to greet the player and serve als a help function:
		void GreetPlayer()
		{
			LineRow();
			Console.WriteLine("Welcome to our amazing game! \n " +
				"Made by Lumen de Vries and Mischa Korthagen as an excersise for a course at the University of Utrecht \n " +
				"To close the programme at any time, you can type '(q)uit' and press enter \n " +
				"To see this screen again at any time you can type '(h)elp' and press enter");
		}

		

		public string PickRules(List<String> posibilities)
		{
			LineRow();
			int holder = -1;
			if (posibilities.Count == 0)
			{
				throw new Exception("Can't pick an item from an empty list");
			}

			Console.WriteLine("Please select a ruleset from the list below, by either typing the name of the ruleset or it's number in the list");
			for (int i = 1; i <= posibilities.Count; i++)
			{
				Console.WriteLine(i.ToString() + ". " + posibilities[i - 1]);
			}
			string line = GetInput();
			if (posibilities.Contains(line))
			{
				Console.WriteLine("You selected: " + line);
				return line;
			}
			else if (int.TryParse(line, out holder) && (holder - 1) >= 0 && (holder-1) < posibilities.Count)
			{
				string selected = posibilities[int.Parse(line)-1];
				Console.WriteLine("You selected: " + selected);
				return selected;
			}
			else
			{
				Console.WriteLine("Unfortunately we could not make sense of your input, please try again");
				return PickRules(posibilities);
			}
		}

		//Added active player as an argument to reduce required logic in this class
		public int GetMove(List<int> allowedMoves, int activePlayer)
		{
			ConsoleColor color;

			if (activePlayer == 1)
			{
				color = ConsoleColor.Red;
			}
			else
			{
				color = ConsoleColor.Green;
			}

			string allowedMovesString = "";
			foreach (int move in allowedMoves)
			{
				allowedMovesString = allowedMovesString + move.ToString() + ", ";
			}
			Console.WriteLine("Player " + activePlayer.ToString() + " please select a hole to start your next move from the allowed list:");

			Console.ForegroundColor = color;
			Console.WriteLine(allowedMovesString);
			Console.ResetColor();


			int input = GetNumberFromPlayer();
			while (!allowedMoves.Contains(input))
			{
				Console.WriteLine("That is not an alowed move, Player " + activePlayer.ToString() + " please select a hole from the list:");
				Console.ForegroundColor = color;
				Console.WriteLine(allowedMovesString);
				Console.ResetColor();

				input = GetNumberFromPlayer();
			}
			return input;
		}

		public void DrawBoard(Board bord)
		{
			LineRow();
			int fieldWidth = 1;
			foreach (Hole h in bord.holes)
			{
				int lengt = h.stones.ToString().Length;
				if ( lengt > fieldWidth)
				{
					fieldWidth = lengt;
				}
			}
			if (bord.holes.Length.ToString().Length > fieldWidth)
			{
				fieldWidth = bord.holes.Length.ToString().Length;
			}

			string upper = padTo("", fieldWidth);
			string lowerupper = padTo("0", fieldWidth);
			string middle = padTo(bord.holes[0].stones.ToString(), fieldWidth);
			string upperlower = padTo("", fieldWidth);
			string lower = padTo("", fieldWidth);

			for (int i = 1; i < (bord.holes.Length / 2); i++)
			{
				int tophole = i;
				upper = upper + '|' + padTo(bord.holes[tophole].stones.ToString(), fieldWidth) ;
				lowerupper = lowerupper + '|' + padTo(tophole.ToString(), fieldWidth);
				middle = middle + '|' + new string('-', fieldWidth);
				int bottomhole = bord.holes.Length - i;
				lower = lower + '|' + padTo(bord.holes[bottomhole].stones.ToString(), fieldWidth) ;
				upperlower = upperlower + '|' + padTo(bottomhole.ToString(), fieldWidth);
			}

			upper = upper + '|' + padTo("", fieldWidth);
			lowerupper = lowerupper + '|' ;
			middle = middle + '|' + padTo(bord.holes[bord.holes.Length/2].stones.ToString(), fieldWidth);
			upperlower = upperlower + '|' + (bord.holes.Length / 2).ToString();
			lower = lower + '|' + padTo("", fieldWidth);

			Console.WriteLine(upper);

			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(lowerupper);
			Console.ResetColor();

			Console.WriteLine(middle);

			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine(upperlower);
			Console.ResetColor();

			Console.WriteLine(lower);
		}

		private void LineRow()
		{
			Console.WriteLine(new string('-', 80));
		}
		private string padTo(string msg, int lengt)
		{
			return msg + new string(' ', Math.Max(lengt - msg.Length, 0));
		}

		//We forgot to add these two functions in our design, this could have been patched by concatenating strings in PickRules, but we thought simply adding single functions is cleaner:

		public int UserInputNrHoles()
		{
			return AskPlayerForNumber("What number of holes would you like (each player) to have?");
		}

		public int UserInputNrStones()
		{
			return AskPlayerForNumber("What number of stones would you like each hole to have at the start of the game?");
		}

		//We also forgot to add a function in our design for this simple end of game function:

		public void EndOfGameMessage(int VictoryPlayer)
		{
			LineRow();
			Console.WriteLine("The game is over!");
			if (VictoryPlayer == 0)
			{
				Console.WriteLine("The game is a tie!");
			}
			else
			{
				Console.WriteLine("Player " + VictoryPlayer.ToString() + " won! Congratulations!");
			}
			Console.WriteLine("Thank you for playing!");
		}

		//Function to let players restart the game:

		public bool StartWithSameSettings()
		{
			LineRow();
			Console.WriteLine("The game is over. \n" +
				"If you would like to restart the game with the same settins: type r(estart) \n" +
				"If you would like to select different settings: type s(ettings) \n" +
				"if you would like to quit the programe: type q(uit)");
			string line = GetInput();
			if (line == "r" || line == "restart" || line == "(r)estart")
			{
				return true;
			}
			else if (line == "s" || line == "settings" || line == "(s)ettings")
			{
				return false;
			}
			else
			{
				Console.WriteLine("We do not understand your input, please select one of the follwing options:");
				return StartWithSameSettings();
			}
		}

		//Private help functions to reduce code duplication:

		private int AskPlayerForNumber(string question)
		{
			Console.WriteLine(question);
			return GetNumberFromPlayer();
		}

		private string GetInput()
		{
			string line = Console.ReadLine();
			if (line == "q" || line == "quit" || line == "(q)uit")
			{
				Environment.Exit(1);
			}
			else if (line == "help" || line == "h" || line == "(h)elp")
			{
				GreetPlayer();
			}
			else
			{
				return line;
			}
			return GetInput();
		}

		private int GetNumberFromPlayer()
		{
			string line = GetInput();
			int output;
			if (int.TryParse(line, out output))
			{
				return output;
			}
			else
			{
				Console.WriteLine("We could not derrive a number from your input, please try again:");
				return GetNumberFromPlayer();
			}
		}
	}
}

