using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace OpenRPG
{
	public class InputManager
	{
		readonly World world;

		public InputManager(World world)
		{
			this.world = world;
		}

		public void HandleMouse(MouseState currMouse)
		{
			// var left = currMouse.LeftButton;
			// var scroll = currMouse.ScrollWheelValue;
			// var right = currMouse.RightButton;
		}
		
		public void HandleKeyboard(KeyboardState currKey)
		{
			if (currKey.IsKeyDown(Keys.Escape))
				world.Game.Exit();
		}
	}
}
