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
    class Items
    {
       public static List<CrabSpriteLoader> objectList = new List<CrabSpriteLoader>();

       public static void Initilize()
       {
           int location = -16;
           int location2 = -16;

           for (int x = 0; x < 25; x++)
           {

               location2 += 32;
               objectList.Add(new Box2(new Vector2(0, location2)));
               objectList.Add(new Box2(new Vector2(800, location2)));
           }

           for (int i = 0; i < 64; i++ )
           {
               CrabSpriteLoader o = new Bullets(new Vector2(0, 0));
               o.alive = false;
               objectList.Add(o);
           }

           for (int i = 0; i < 100; i++)
           {
               CrabSpriteLoader o = new Enemy(new Vector2(0, 0));
               o.alive = false;
               objectList.Add(o);
           }

           for (int i = 0; i < 20; i++)
           {
               CrabSpriteLoader o = new Enemy2(new Vector2(0, 0));
               o.alive = false;
               objectList.Add(o);
           }

            for (int x = 0; x < 10; x++)
            {
                CrabSpriteLoader o = new Rocket(new Vector2(0, 0));
                o.alive = false;
                objectList.Add(o);
            }
            for (int x = 0; x < 25; x++ )
            {
                
                location += 32;
                objectList.Add(new Box(new Vector2(location, 0)));
                objectList.Add(new Box(new Vector2(location, 32)));
                objectList.Add(new Box(new Vector2(location, 480)));
            }

            objectList.Add(new EnemySpawner(new Vector2(0, 0)));
           objectList.Add(new Shadow(new Vector2(0, 0)));
           objectList.Add(new Character(new Vector2(400, 400)));
           objectList.Add(new Cursor(new Vector2(0, 0)));
       }

        public static void Reset() //Reset all objects in the object list
        {
            foreach(CrabSpriteLoader o in objectList)
            {
                o.alive = false; //loops through the objects to see whether they are dead
            }
        }
    }
}
