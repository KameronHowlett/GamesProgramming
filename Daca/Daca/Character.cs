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
    class Character : CrabSpriteLoader
    {


        KeyboardState keyboard;
        KeyboardState previousKeyboard;

        MouseState mouseState;
        MouseState PreviousMouseState;

        private float spd;
        float bSpeed = 30;
        float offSet;
        float recoil = 0;

        const int maxAmmo = 32;
        int ammo = 32;
        int rate = 10;//60 fps so 6 bullets per second
        int firingTimer = 0;

        int rRate = 200;
        int rFiringTimer = 0;

        int health = 0;
        int healthMax = 100;

        public static Character character;

        public static bool dead = false;

        bool reloading = false;
        int reloadTimer = 0;
        int reloadTime = 60 * 4;
        int recoilDelayTime = 30;
        string rString = "";


        Random random;

        public Character (Vector2 Position) : base(Position)
        {
            Position = position;
            spd = 0.2f;

            spriteName = "droneSmall";
            health = healthMax;
        }

        public override void Update(GameTime gameTime)
        {

            character = this;

            if(health <= 0)
            {
                dead = true;
                alive = false;
            }
            Game1.pX = position.X;
            Game1.pY = position.Y;

            Game1.amz = ammo;

            random = new Random();

           offSet = (float)(random.NextDouble() * recoil - (recoil /2));

            if (recoil > 0 && mouseState.LeftButton == ButtonState.Released || recoil > 0 &&  reloading)
            {
                    recoilDelayTime--;
            }

            if(recoilDelayTime <= 0)
            {
                recoil--;
                //recoilDelayTime = 0;
            }

            if(recoil == 0)
            {
                recoilDelayTime = 30;
            }

            if (!alive) return;//If dead stop


            keyboard = Keyboard.GetState();//Get the current state of the keyboard at that frame
            mouseState = Mouse.GetState();

            if (keyboard.IsKeyDown(Keys.A) && !Collision(new Vector2(-5,0), new Box(new Vector2(0,0))) && !Collision(new Vector2(-5,0), new Box2(new Vector2(0,0))))
            {
                position.X -= spd * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            if (keyboard.IsKeyDown(Keys.D) && !Collision(new Vector2(5, 0), new Box(new Vector2(0, 0))) && !Collision(new Vector2(5, 0), new Box2(new Vector2(0, 0))))
            {
                position.X += spd * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            if (keyboard.IsKeyDown(Keys.W) && !Collision(new Vector2(0, -5), new Box(new Vector2(0, 0))) && !Collision(new Vector2(0, -5), new Box2(new Vector2(0, 0))))
            {
                position.Y -= spd * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            if (keyboard.IsKeyDown(Keys.S) && !Collision(new Vector2(0, 5), new Box(new Vector2(0, 0))) && !Collision(new Vector2(0, 5), new Box2(new Vector2(0, 0))))
            {
                position.Y += spd * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }

            if(keyboard.IsKeyDown(Keys.L))
            {
                Damaged(100);
            }

            firingTimer++;
            rFiringTimer++;

            if(mouseState.LeftButton == ButtonState.Pressed && !reloading)
            {
                CheckShooting();
            }

            if (mouseState.RightButton == ButtonState.Pressed)
            {
                CheckShootingRocky();
            }


            if (rFiringTimer > rRate)
                rString = "Yes";
            else
                rString = "No";

            if(keyboard.IsKeyDown(Keys.R) && ammo != 32|| ammo == 0)
            {
                reloading = true;
            }

            CheckReload();
            Shad();

            //~~~~Pointtowards

            rotation = PointDirection(position.X, position.Y, mouseState.X, mouseState.Y);

            previousKeyboard = keyboard;
            PreviousMouseState = mouseState;

            base.Update(gameTime);
        }

        private void CheckReload()
        {
            if (reloading)
            {
                ammo = 0;
                reloadTimer++;
                if (reloadTimer > reloadTime)
                {
                    ammo = maxAmmo;
                    reloadTimer = 0;
                    reloading = false;
                }
            }
        }

        private void CheckShootingRocky()
        {
            if (rFiringTimer > rRate)
            {
                rFiringTimer = 0;
                ShootyRocky();
            }
        }
        private void CheckShooting()
        {
            if(firingTimer > rate && ammo > 0)
            {
                firingTimer = 0;
                Shooty();
            }
        }

        private void Shooty()
        {
            ammo--;

            foreach(CrabSpriteLoader o in Items.objectList)
            {
                if (o.GetType() == typeof(Bullets) && !o.alive)
                {
                    recoilDelayTime = 30;
                    recoil += 1;
                    
                    o.position = position;
                    o.UpdateArea();
                    o.rotation = rotation + offSet;
                    o.speed = bSpeed;
                    o.alive = true;
                    break;
                }
            }
        }

        private void ShootyRocky()
        {

            foreach (CrabSpriteLoader o in Items.objectList)
            {
                if (o.GetType() == typeof(Rocket) && !o.alive)
                {
                    o.position = position;
                    o.UpdateArea();
                    o.rotation = rotation;
                    o.speed = (bSpeed - 20);
                    o.alive = true;
                    break;
                }
            }
        }

        private void Shad()
        {

            foreach (CrabSpriteLoader o in Items.objectList)
            {
                if (o.GetType() == typeof(Shadow))
                {

                    o.position = position;
                    o.rotation = rotation;

                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Game1.font, "Ammo " + ammo, new Vector2(0, 90), Color.White);
            spriteBatch.DrawString(Game1.font, "Rocket Cooldown " + rString, new Vector2(100, 90), Color.White);
            spriteBatch.DrawString(Game1.font, "Recoil " + recoil, new Vector2(0, 60), Color.White);
            spriteBatch.DrawString(Game1.font, "HEALTH: " + health, new Vector2(60, 60), Color.White);
            spriteBatch.DrawString(Game1.font, "/ " + healthMax, new Vector2(135, 60), Color.White);
            

            if (reloading)
            {
                spriteBatch.DrawString(Game1.font, "RELOADING", new Vector2(position.X - 40, position.Y + 10), Color.Green);
            }

            base.Draw(spriteBatch);
        }

        private float PointDirection (float x, float y, float x2, float y2)//finds the angle between two points based on two coords
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

        public void Damaged(int dmg)
        {
            health -= dmg;
        }

    }
}
