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
    class CrabSpriteLoader
    {
        public Vector2 position;
        public float rotation = 0.0f;
        public Texture2D spriteIndex;
        public string spriteName = "";
        public float speed = 0.0f;
        public float scale = 1.0f;
        public bool alive = true;


        public Rectangle area;
        public bool solid = false;

        public CrabSpriteLoader (Vector2 Position)//Constructor with position argument
        {
            position = Position;
            
        }

        public CrabSpriteLoader ()//Contructor with no Argument
        {
            
        }

        public virtual void LoadContent(ContentManager Content)
        {
            spriteIndex = Content.Load<Texture2D>("CrabSprites\\" + this.spriteName);
            area = new Rectangle(0, 0, spriteIndex.Width, spriteIndex.Height);
        }

        public virtual void Update(GameTime gameTime)
        {
            if (!alive) return;//If dead stop

            UpdateArea();

  

            pushTo(speed, rotation);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {           
            if (!alive) return;//If dead stop

            Vector2 centre = new Vector2(spriteIndex.Width/2,spriteIndex.Height / 2);

            spriteBatch.Draw(spriteIndex, position, null, Color.White, MathHelper.ToRadians(rotation), centre, scale ,SpriteEffects.None,0);
        }

        public bool Collision(Vector2 position, CrabSpriteLoader obj)
        {
            Rectangle newArea = new Rectangle(area.X, area.Y, area.Width, area.Height);
            newArea.X += (int)position.X;
            newArea.Y += (int)position.Y;

            foreach (CrabSpriteLoader o in Items.objectList)
            {
                if(o.GetType() == obj.GetType())
                {
                    if (o.area.Intersects(newArea))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public CrabSpriteLoader CollisionObj(CrabSpriteLoader obj)//Find obj which it collides with
        {
            foreach (CrabSpriteLoader o in Items.objectList)
            {
                if (o.GetType() == obj.GetType())
                {
                    if (o.area.Intersects(area))
                        return o;
                }
            }
            return new CrabSpriteLoader();
        }

        public void UpdateArea()
        {
            area.X = (int)position.X - (spriteIndex.Width / 2);
            area.Y = (int)position.Y - (spriteIndex.Height / 2);
        }

        public void pushTo(float Pix, float Dir)//
        {
            float newX = (float)Math.Cos(MathHelper.ToRadians(Dir));
            float newY = (float)Math.Sin(MathHelper.ToRadians(Dir));
            position.X += Pix * (float)newX;
            position.Y += Pix * (float)newY;
        }

    }
}
