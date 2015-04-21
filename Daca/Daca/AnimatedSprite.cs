using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Daca
{
    public class AnimatedSprite
    {
        public Texture2D Texture { get; set; }
        public int Rows { get; set; }//rows in the texture atlas
        public int Columns { get; set; }//Columns in the texture atlas
        private int currentFrame;//animation frame
        private int totalFrames;//total frames in an animation(Atlas)

        public AnimatedSprite(Texture2D texture, int rows, int columns)
        {
            Texture = texture;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
        }
        public void Update(bool EXP)
        {
            currentFrame++;
            if (currentFrame == totalFrames && EXP)//ables to calculate the frames and loop it
            {
                currentFrame = 0;
                Game1.expFeed1 = false;
                Game1.expFeed2 = false;
            }
            //if (Game1.hit == false)
            //    currentFrame = 0;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = (int)((float)currentFrame / (float)Columns);
            int column = currentFrame % Columns;//These work out the image width and height using the rows and columns

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationReactangle = new Rectangle((int)location.X, (int)location.Y, width, height); // This rectangle represents the visable image

            spriteBatch.Begin();
            spriteBatch.Draw(Texture, destinationReactangle, sourceRectangle, Color.White);
            spriteBatch.End();
        }
    }
}
