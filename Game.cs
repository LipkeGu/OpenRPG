using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OpenRPG
{
	public class Game : Microsoft.Xna.Framework.Game
	{
		public static readonly char PathSeperator = Path.DirectorySeparatorChar;
		public readonly Dictionary<string, Texture2D> Textures = new Dictionary<string, Texture2D>();

		public Dictionary<string, ActorInfo> ActorRules = new Dictionary<string, ActorInfo>();

		int ticks;
		SpriteBatch spriteBatch;

		readonly World world;

		static ObjectCreator creator = new ObjectCreator();

		public Game()
		{
			new GraphicsDeviceManager(this);
			IsMouseVisible = true;
			Content.RootDirectory = "Content";

			// 30 ticks/second
			TargetElapsedTime = TimeSpan.FromSeconds(1f / 30f);

			world = new World(this);
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

		public static T CreateObject<T>(string className)
		{
			return creator.CreateObject<T>(className);
		}

		protected override void Update(GameTime gameTime)
		{
			++ticks;
			world.Tick();
			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			spriteBatch.Begin();
			world.TickRender();
			spriteBatch.End();
			base.Draw(gameTime);
		}

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);

			// https://github.com/flibitijibibo/FNA/issues/295
			var pngs = Directory.EnumerateFiles(Content.RootDirectory, "*.png");
			foreach (var png in pngs)
			{
				var filename = png.Split(PathSeperator)[1].Split('.')[0].ToLowerInvariant();
				Textures.Add(filename, Content.Load<Texture2D>(filename));
			}

			var rules = Directory.EnumerateFiles("rules", "*.meta");

			// TODO: This also creates an instance of the actor
			// which is required because we don't have an intermediate ActorInit
			foreach (var ruleFile in rules)
				LoadActorTypes(ruleFile);
		}
	}
}
