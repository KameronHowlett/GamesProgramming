using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace Daca
{
    public class Player
    {


        //The current position of the Sprite
        public Vector2 playerPosition, tempCurrentFrame;

        KeyboardState keyState;
        //The texture object used when drawing the sprite
        Texture2D playerImage;
        float moveSpeed = 100;



        Animation playerAnimation = new Animation();

        public void Initialize()
        {
            playerAnimation.Initialize(playerPosition, new Vector2(9, 1));//Store the players frame position
            tempCurrentFrame = Vector2.Zero;
        }
        public void LoadContent(ContentManager Content)
        {
            playerImage = Content.Load<Texture2D>("CrabSprites/CrabWalkingAtlas");
            playerAnimation.AnimationImage = playerImage;//Reference to the Animation Class
        }

        public void Update(GameTime gameTime)
        {

            

            keyState = Keyboard.GetState();

            playerPosition = playerAnimation.Position;

            if (keyState.IsKeyDown(Keys.Down))
            {
                playerPosition.Y += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                tempCurrentFrame.Y = 0;
            }

            else if (keyState.IsKeyDown(Keys.Up))
            {
                playerPosition.Y -= moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                tempCurrentFrame.X = 1;
            }

            if (keyState.IsKeyDown(Keys.Right))
            {
                playerPosition.X += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                tempCurrentFrame.X = 2;
            }
            else if (keyState.IsKeyDown(Keys.Left))
            {
                playerPosition.X -= moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                tempCurrentFrame.X = 3;
            }

            //else
            // playerAnimation.Active = false;


            //turret.Follow(playerPosition);

            tempCurrentFrame.X = playerAnimation.CurrentFrame.X;

            playerAnimation.Position = playerPosition;
            playerAnimation.CurrentFrame = tempCurrentFrame;


            playerAnimation.Active = false;
            playerAnimation.Update(gameTime);
        }

        //Draw the sprite to the screen
        public void Draw(SpriteBatch spriteBatch)
        {
            //playerAnimation.Draw(spriteBatch);
        }

    }
}
