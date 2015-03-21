using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OpenRPG.Graphics;
using SSize = System.Drawing.Size;

namespace OpenRPG
{
	public class Game : Microsoft.Xna.Framework.Game
	{
		public static readonly char PathSeperator = Path.DirectorySeparatorChar;
		public static readonly string ContentDirectory = "Content";

		public readonly Dictionary<string, Texture2D> Textures = new Dictionary<string, Texture2D>();
		public readonly Dictionary<string, Animation> Animations = new Dictionary<string, Animation>();
		public readonly Dictionary<string, ActorInfo> ActorRules = new Dictionary<string, ActorInfo>();
		public readonly SSize WindowSize;
		public readonly Rectangle ClientBounds;

		public SpriteBatch SpriteBatch { get; private set; }

		int ticks;

		readonly World world;
		readonly InputManager inputMan;

		static ObjectCreator creator = new ObjectCreator();

		public Game()
		{
			new GraphicsDeviceManager(this);
			IsMouseVisible = true;
			Content.RootDirectory = ContentDirectory;
			Window.AllowUserResizing = false;
			WindowSize = new SSize
			(
				GraphicsDevice.Viewport.Width,
				GraphicsDevice.Viewport.Height
			);
			ClientBounds = Window.ClientBounds;

			// 30 ticks/second
			TargetElapsedTime = TimeSpan.FromSeconds(1f / 30f);

			SpriteBatch = new SpriteBatch(GraphicsDevice);
			world = new World(this);
			inputMan = new InputManager(world);
			creator = new ObjectCreator();
		}

		// TODO: fixme with an ActorInit (ASAP)
		void LoadActorTypes(string filename)
		{
			var rules = MetadataTree.NodesFromFile(filename);

			foreach (var actorDef in rules)
			{
				var name = actorDef.Key.ToLowerInvariant();
				var info = new ActorInfo(name, actorDef);

				if (ActorRules.ContainsKey(name))
					throw new ArgumentException("Duplicate definition for `{0}`.".F(name));

				ActorRules.Add(name, info);

				// This is where the bogosity starts
				// Just because we load the rules doesn't mean we want an instance

				var actor = new Actor(world, name);

				foreach (var traitInfo in info.TraitInfos)
					actor.AddTrait(traitInfo.CreateTrait(actor) as ITrait);

				world.AddActor(actor);
			}
		}

		public static T CreateObject<T>(string infoClassName)
		{
			return creator.CreateObject<T>(infoClassName);
		}

		// TODO: Properly use gameTime so we're independent of tick-rate
		protected override void Update(GameTime gameTime)
		{
			++ticks;
			world.Tick();
			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);
			SpriteBatch.Begin();
			world.TickRender();
			HandleInput();
			SpriteBatch.End();
			base.Draw(gameTime);
		}

		void HandleInput()
		{
			var mouseState = Mouse.GetState();
			var keyboardState = Keyboard.GetState();
			inputMan.HandleMouse(mouseState);
			inputMan.HandleKeyboard(keyboardState);
		}

		public static string ContentFileFromString(string str)
		{
			return ContentDirectory+ PathSeperator + str;
		}

		protected override void LoadContent()
		{
			// 1) Parse data tree (get filenames to load)
			// 2) Load image files as Texture2D
			// 3) Construct Sprite -> Animation relationship

			var anims = Directory.EnumerateFiles("rules/anims", "*.meta");
			foreach (var animFile in anims)
			{
				var metaNodes = MetadataTree.NodesFromFile(animFile);
				foreach (var node in metaNodes)
					Animations.Add(node.Key, new Animation(world, node.Key, node.ChildTree.Nodes));
			}

			var rules = Directory.EnumerateFiles("rules", "*.meta");

			// TODO: This also creates an instance of the actor
			// which is required because we don't have an intermediate ActorInit
			foreach (var ruleFile in rules)
				LoadActorTypes(ruleFile);
		}
	}
}
