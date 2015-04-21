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
    class Enemy2 : CrabSpriteLoader
    {
         int health = 0;
        const int maxHealth = 15;

        public Enemy2(Vector2 Position)
            : base(Position)
        {
            health = maxHealth;
            Position = position;
            spriteName = "Enemy2";          
        }
        
        public override void Update(GameTime gameTime)
        {
            if(health <= 0)
            {
                Game1.score += 30;
                alive = false;
                health = maxHealth;
            }

            if (!alive) return;

            CrabSpriteLoader o = CollisionObj(new Character(new Vector2(0, 0)));

            if(o.GetType() == typeof(Character))
            {
                Character e = (Character)o;
                if (e.alive == true)
                {
                    Game1.expFeed2 = true;
                    Damage(40);
                    e.Damaged(20);
                }
            }

            if (position.X < -180 || position.Y < -180 || position.X > Game1.area.Width + 150 || position.Y > Game1.area.Height + 150)//Off the screen
            {
                alive = false;
                health = maxHealth;
            }

            rotation = PointDirection(position.X, position.Y, Character.character.position.X, Character.character.position.Y);

            base.Update(gameTime);
        }



        public void Damage(int dmg)
        {
            health -= dmg;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.DrawString(Game1.font, "Health " + health, new Vector2(position.X, position.Y), Color.White);

            base.Draw(spriteBatch);
        }

        private float PointDirection(float x, float y, float x2, float y2)//finds the angle between two points based on two coords
        {
            float diffx = x - x2;
            float diffy = y - y2;
            float adj = diffx;
            float opp = diffy;
            float tan = opp / adj;
            float res = MathHelper.ToDegrees((float)Math.Atan2(opp, adj));
            res = (res - 180) % 360;
            if (res < 0)
            { res += 360; }
            return res;
        }
    }
}
