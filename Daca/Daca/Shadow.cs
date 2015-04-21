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


    class Shadow : CrabSpriteLoader
    {

        public Shadow(Vector2 Position)
            : base(Position)
        {
            Position = position;
            spriteName = "Shadow";
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

    }
}
