using System;
using Microsoft.Xna.Framework.Input;

namespace OpenRPG
{
	public class InputManager
	{
		readonly Game game;

		public InputManager(Game game)
		{
			this.game = game;
		}

		public void HandleMouse(MouseState currMouse)
		{
			var left = currMouse.LeftButton;
			var scroll = currMouse.ScrollWheelValue;
			var right = currMouse.RightButton;

			Console.WriteLine(left.ToString());
		}
		
		public void HandleKeyboard(KeyboardState currKey)
		{
			if (currKey.IsKeyDown(Keys.Escape))
				game.Exit();
		}
	}
}
