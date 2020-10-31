using System;

namespace MSO4
{
	class Game
	{
		UIController uic;
		public Board board;
		public int activePlayer;
		public Rules rules;

		public Game()
		{
			uic = new UIController();
		}

	}
}

