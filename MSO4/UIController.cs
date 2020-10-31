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
	}
}

