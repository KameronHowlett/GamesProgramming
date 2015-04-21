#region Using Statements
using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
#endregion

namespace Daca
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        KeyboardState keyboard;
        KeyboardState previousKeyboard;

        public static Texture2D BulletImage;

        public string playerN;

        //~~~~~ new Player/animation Class~~~~~~

        Player player = new Player();

        public static SpriteFont font;
        public static SpriteFont Scorey;

        //~~~~~ new Player/animation Class 2~~~~~~
        Cursor cursor = new Cursor(new Vector2(0, 0));
        CharacterBase baseC = new CharacterBase(new Vector2(50, 50));
        public static Rectangle area;

        public static int score = 0;

        public bool controls = true;

        private AnimatedSprite animatedSprite, animatedEX;

        int mouseX;
        int mouseY;

        public static float pX;
        public static float pY;

        public static bool hit;
        public static float rX;
        public static float rY;

        public static int amz = 0;

        public static bool expFeed1, expFeed2;

       // int Move = 0;

        ParticleEngine particleEngine;

        Vector2 pPosition;

        public Game1()
            : base()
        {
            this.IsMouseVisible = false;

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

        }

        protected override void Initialize()
        {
            this.Window.Title = "Drone Pipe";
            // TODO: Add your initialization logic here

            Items.Initilize();//Item class initilize
            area = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight); //Rectangle of room

            player.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            //~~~~~ new Player/animation Class~~~~~~
            player.LoadContent(Content);

            //~~~~~ new Player/animation Class 2~~~~~~
            foreach(CrabSpriteLoader o in Items.objectList)
            {
                o.LoadContent(this.Content);
            }

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //spriteBatch = new SpriteBatch(GraphicsDevice);
            //Texture2D image = Content.Load<Texture2D>("LogoImages/Block");
            font = Content.Load<SpriteFont>("Text");
            Scorey = Content.Load<SpriteFont>("SpriteFontMenu");

            List<Texture2D> textures = new List<Texture2D>();
            textures.Add(Content.Load<Texture2D>("Particles/Smoke"));
            textures.Add(Content.Load<Texture2D>("Particles/Smoke2"));
            textures.Add(Content.Load<Texture2D>("Particles/Smoke3"));
           particleEngine = new ParticleEngine(textures, new Vector2(400, 240));


            Texture2D texture = Content.Load<Texture2D>("CrabSprites/explosion");
            animatedSprite = new AnimatedSprite(texture, 4, 10);//(rows and columns)

            Texture2D bigExplode = Content.Load<Texture2D>("CrabSprites/explosion2");
            animatedEX = new AnimatedSprite(bigExplode, 4, 5);//(rows and columns)

            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {

            pPosition = new Vector2(rX, rY);



            //~~~~~ new Player/animation Class 2~~~~~~

                foreach (CrabSpriteLoader o in Items.objectList)
                {
                    o.Update(gameTime);
                }



            MouseState mouseState = Mouse.GetState();

            mouseX = mouseState.X;
            mouseY = mouseState.Y;

            //part = character.position;
            
            particleEngine.EmitterLocation = new Vector2(pX, pY);   

            particleEngine.Update();
            //~~~~Esc to exit~~~~~~~
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (expFeed1)
            {
                animatedSprite.Update(expFeed1);
            }

            if (expFeed2)
            {
                animatedEX.Update(expFeed2);
            }

            // TODO: Add your update logic here

            if (Keyboard.GetState().IsKeyDown(Keys.C))
                controls = false;


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            if (!Character.dead)
            {
                particleEngine.Draw(spriteBatch);
            }


            //~~~~~ new Player/animation Class~~~~~~


            if (expFeed1)
            {
                animatedSprite.Draw(spriteBatch, pPosition);
            }

            

            spriteBatch.Begin();
            //~~~~~ new Player/animation Class 2~~~~~~
            foreach (CrabSpriteLoader o in Items.objectList)
            {
                o.Draw(spriteBatch);
            }

            spriteBatch.DrawString(Game1.font, "Score: " + score, new Vector2(180, 60), Color.White);

            if (Character.dead)
            {
                spriteBatch.DrawString(Game1.Scorey, "Your Score: " + score, new Vector2(200, 200), Color.White);
                spriteBatch.DrawString(Game1.font, "Press Esc to quit", new Vector2(300, 300), Color.Orange);
            }

            if(controls)
            {
                spriteBatch.DrawString(Game1.font, "Press A,S,D and W to Move", new Vector2(500, 60), Color.Orange);
                spriteBatch.DrawString(Game1.font, "Press The left Mouse Button to Fire MachineGun", new Vector2(500, 90), Color.Orange);
                spriteBatch.DrawString(Game1.font, "Press The right Mouse Button to Fire Rockets", new Vector2(500, 120), Color.Orange);
                spriteBatch.DrawString(Game1.font, "Press X to display wave info and Z to remove it", new Vector2(500, 150), Color.Orange);
                spriteBatch.DrawString(Game1.font, "Press C to remove this info", new Vector2(500, 180), Color.Orange);
            }
           
            spriteBatch.End();

            if (expFeed2)
            {
                animatedEX.Draw(spriteBatch, new Vector2(pX - 50, pY - 50));
            }

            base.Draw(gameTime);
        }
    }
}
