using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Daca
{
    public class Animation
    {
        int frameCounter;
        int switchFrame;
        bool active = false;

        Vector2 position, amountofFrames, currentFrame;
        Texture2D Image;
        Rectangle sourceRect;

        public bool Active
        {
            get { return active; }
            set { active = value; }
        }

        public Vector2 CurrentFrame
        {
            get { return currentFrame; }
            set { currentFrame = value;}
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Texture2D AnimationImage
        {
            set { Image = value; }
        }

        public int FrameWidth
        {
            get { return Image.Width / (int)amountofFrames.X; }//Returns the indiviusal Frame Width
        }

        public int FrameHeight
        {
            get { return Image.Height / (int)amountofFrames.Y; }//Returns the indiviusal Frame Width
        }

        public void Initialize(Vector2 Position, Vector2 Frames)
        {
            active = false;
            switchFrame = 200;
            this.position = Position;
            this.amountofFrames = Frames;
        }
        public void Update(GameTime gameTime)
        {
            if (active)
                frameCounter += (int)gameTime.ElapsedGameTime.TotalMilliseconds;//Recieves the amount of time played
            else
                frameCounter = 0;

            if(frameCounter >= switchFrame)
            {
                frameCounter = 0;
                currentFrame.X += FrameWidth;
                if (currentFrame.X >= Image.Width)
                    currentFrame.X = 0; //If the Current frame is at the edge of the image return to 0
                
            }
            sourceRect = new Rectangle((int)currentFrame.X, (int)currentFrame.Y, FrameWidth, FrameHeight);//
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Image, position, sourceRect, Color.White);
        }
    }
}
