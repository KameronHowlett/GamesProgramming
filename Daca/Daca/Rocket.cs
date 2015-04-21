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
    class Rocket : CrabSpriteLoader
    {

        public Rocket(Vector2 Position)
            : base(Position)
        {
            Position = position;
            spriteName = "Rocket";
        }
            

        public override void Update(GameTime gameTime)
        {


            if (!alive) return;

            if(Collision(Vector2.Zero, new Box(new Vector2(0,0))))
            {
                Game1.expFeed1 = true;
                speed = 0;
               alive = false;
            }

            CrabSpriteLoader o = CollisionObj(new Enemy(new Vector2(0, 0)));

            if (o.GetType() == typeof(Enemy))
            {               
                Enemy e = (Enemy)o;
                if (e.alive == true)
                {
                    Game1.expFeed1 = true;
                    alive = false;
                    e.Damage(40);
                }
            }

            CrabSpriteLoader o2 = CollisionObj(new Enemy2(new Vector2(0, 0)));

            if (o2.GetType() == typeof(Enemy2))
            {
                Enemy2 f = (Enemy2)o2;
                if (f.alive == true)
                {
                    Game1.expFeed1 = true;
                    alive = false;
                    f.Damage(40);
                }
            }

            Game1.rX = position.X - 58;
            Game1.rY = position.Y - 58;

            if(position.X < 0 || position.Y < 0 || position.X > Game1.area.Width|| position.Y > Game1.area.Height)//Off the screen
            {
                alive = false;
            }

            base.Update(gameTime);
        }

    }
}
