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
    class Cursor : CrabSpriteLoader
    {
        MouseState mouseState;

        public Cursor(Vector2 Position) :base(Position)
        {
            Position = position;

            spriteName = "Cursor";
        }

        public override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();

            position = new Vector2(mouseState.X, mouseState.Y);

            base.Update(gameTime);
        }
    }
}
