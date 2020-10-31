using System;
using System.Collections.Generic;

namespace MSO4
{
	class Game
	{
		UIController uic;
		Rules rules;
		Board board;
		GameState gamestate;

		public enum GameState
		{
			SetupGame,
			Playing,
			GameOver
		}

		public Game()
		{
			SetUpGame();
		}

		public void Update()
		{
			while (gamestate == GameState.Playing)
			{
			}
			//Show winner to player, give opportunity to switch rules or restart with same rules
		}

		private void SetUpGame()
		{
			gamestate = GameState.SetupGame;
			uic = new UIController();
			String ruleType = uic.PickRules(RulesDataBase.GetRuleNames());
			int holes = uic.UserInputNrHoles();
			int stones = uic.UserInputNrStones();
			ResetGame(ruleType, holes, stones);
			Update();
		}

		private void ResetGame(string ruleType, int holes, int stones)
		{
			//TODO: create rules here using string assigned above
			//TODO create board here using nr of holes and stones assigned above
		}
	}
}

