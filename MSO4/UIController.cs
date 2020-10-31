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

		void GreetPlayer()
		{
			Console.WriteLine("Welcome to our amazing game! \n " +
				"Made by Lumen de Vries and Mischa Korthagen as an excersise for the University of Utrecht \n " +
				"To close the programme at any time, you can type 'q' and press enter \n " +
				"To see this screen again at any time you can type 'help' and press enter");
		}

		

		public string PickRules(List<String> posibilities)
		{
			if (posibilities.Count == 0)
			{
				throw new Exception("Can't pick an item from an empty list");
			}

			Console.WriteLine("Please select a ruleset from the list below, by either typing the name of the ruleset or it's number in the list");
			for (int i = 1; i <= posibilities.Count; i++)
			{
				Console.WriteLine(i.ToString() + posibilities[i - 1]);
			}
			string line = GetInput();
			if (posibilities.Contains(line))
			{
				Console.WriteLine("You selected: " + line);
				return line;
			}
			else if ((int.Parse(line) - 1) >= 0 && int.Parse(line) < posibilities.Count)
			{
				string selected = posibilities[int.Parse(line)];
				Console.WriteLine("You selected: " + selected);
				return selected;
			}
			else
			{
				Console.WriteLine("Unfortunately we could not make sense of your input, please try again");
				return PickRules(posibilities);
			}
		}

		public int UserInputNrHoles()
		{
			return AskPlayerForNumber("What number of holes would you like (each player) to have?");
		}

		public int UserInputNrStones()
		{
			return AskPlayerForNumber("What number of stones would you like each hole to have at the start of the game?");
		}

		public void DrawBoard(Board bord)
		{
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
			if (line == "q")
			{
				Environment.Exit(1);
			}
			else if (line == "help")
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

