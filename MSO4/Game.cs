using System;
using System.Collections.Generic;

namespace MSO4
{
	class Game
	{
		UIController uic;
		Rules rules;
		Board board;
		int activePlayer = 1;

		String ruleType;
		int holes;
		int stones;

		//We added this enum for internal logic:
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
				uic.DrawBoard(board);
				Console.WriteLine();

				List<int> allowedMoves = rules.AllowedMoves(board, activePlayer);
				if (allowedMoves.Count == 0)
				{
					gamestate = GameState.GameOver;
					break;
				}
				board = rules.Move(uic.GetMove(allowedMoves, activePlayer), board, activePlayer);

				if (rules.PlayerSwapped)
				{
					if (activePlayer == 1)
					{
						activePlayer = 2;
					}
					else
					{
						activePlayer = 1;
					}
				}


			}
			uic.EndOfGameMessage(rules.CalculateWinners(board));
			if (uic.StartWithSameSettings())
			{
				ResetGame(ruleType, holes, stones);
			}
			else
			{
				SetUpGame();
			}
		}

		private void SetUpGame()
		{
			gamestate = GameState.SetupGame;
			uic = new UIController();
			ruleType = uic.PickRules(RulesDataBase.GetRuleNames());
			holes = uic.UserInputNrHoles();
			stones = uic.UserInputNrStones();
			ResetGame(ruleType, holes, stones);
		}

		private void ResetGame(string ruleType, int holes, int stones)
		{
			Boardfactory b = new Boardfactory();
			RulesFactory r = new RulesFactory();

			activePlayer = 1;

			board = b.MakeBoard(ruleType, holes, stones);
			rules = r.GetRules(ruleType);
			gamestate = GameState.Playing;
			Update();
		}
	}
}

