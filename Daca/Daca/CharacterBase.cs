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
    class CharacterBase : CrabSpriteLoader //to Inherit
    {
        KeyboardState keyboard;
        KeyboardState previousKeyboard;

        MouseState mouseState;
        MouseState PreviousMouseState;

        public float RotationAngle { get; set; }

        private float spd;


        public CharacterBase(Vector2 Position)
            : base(Position)
        {
            Position = position;
            spd = 1.0f;

            spriteName = "CrabBaseStill";
        }

        public override void Update(GameTime gameTime)
        {
            keyboard = Keyboard.GetState();//Get the current state of the keyboard at that frame
            mouseState = Mouse.GetState();



            if (keyboard.IsKeyDown(Keys.Up))
            {
                position.X += (rotation / 100);
                position.Y += (rotation / 100);
            }
            if (keyboard.IsKeyDown(Keys.Right))
            {
                rotation += MathHelper.ToRadians(45);
            }
            if (keyboard.IsKeyDown(Keys.Down))
            {
                position.X -= spd;
            }
            if (keyboard.IsKeyDown(Keys.Left))
            {
                rotation += MathHelper.ToRadians(-45);
            }

            //~~~~Pointtowards

            //rotation = PointDirection(position.X, position.Y, mouseState.X, mouseState.Y);

            previousKeyboard = keyboard;
            PreviousMouseState = mouseState;

            base.Update(gameTime);
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
