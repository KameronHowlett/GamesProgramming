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
    class EnemySpawner : CrabSpriteLoader
    {
        float eSpeed = 1;
        float eSpeedR = 0;

        float positionY;
        int spawnRate = 100;
        int spawnTime = 0;
        int RandomStore = 0;

        int spawnRateS = 0;

        bool displayStats = false;

        Random randomSpawnTime;
        Random randomES;
        Random randomY;

        KeyboardState keyboard;

        public int Wave = 1;

        public EnemySpawner(Vector2 Position)
            : base(Position)
        {
            Position = position;
            spriteName = "Shadow";
        }


        public override void Update(GameTime gameTime)
        {
            eSpeed = eSpeedR;

            randomSpawnTime = new Random();
            randomES = new Random();
            randomY = new Random();

            if (spawnRate == 80)
            {
                Wave += 1;
                spawnRate = 100;
            }

            spawnTime++;
            if (spawnTime == spawnRate)
            {
                spawnRateS++;
                for (int i = 0; i < Wave; i++)
                {

                    positionY = (int)(randomY.Next(380));
                    RandomStore = randomSpawnTime.Next(2);

                     if(RandomStore == 0)
                         SpawnyRight();
                     else
                         SpawnyLeft();

                     eSpeedR = (int)(randomES.Next(3, 5 + Wave));

                    
                }

                if (Wave > 1 && spawnRateS >= 5)
                {
                    SpawnyStalker();
                    spawnRateS = 0;
                }

                if (Wave > 2 && spawnRateS >= 5)
                {
                    SpawnyStalker2();
                    spawnRateS = 0;
                }

                spawnTime = 0;             
                spawnRate -= RandomStore;
            }

            keyboard = Keyboard.GetState();//Get the current state of the keyboard at that frame

            if (keyboard.IsKeyDown(Keys.X))
            {
                displayStats = true;
                return;
            }

            if (keyboard.IsKeyDown(Keys.Z))
            {
                displayStats = false;
                   return;
             }
        }


        private void SpawnyRight()
        {

            foreach (CrabSpriteLoader o in Items.objectList)
            {
                if (o.GetType() == typeof(Enemy) && !o.alive)
                {
                    o.position = new Vector2 (900, positionY + 64) ;
                    o.UpdateArea();
                    o.rotation = 180;
                    o.speed = (eSpeed);
                    o.alive = true;
                    break;
                }
            }
        }

        private void SpawnyLeft()
        {

            foreach (CrabSpriteLoader o in Items.objectList)
            {
                if (o.GetType() == typeof(Enemy) && !o.alive)
                {
                    o.position = new Vector2(-100, positionY + 64);
                    o.UpdateArea();
                    o.rotation = 0;
                    o.speed = (eSpeed);
                    o.alive = true;
                    break;
                }
            }
        }

        private void SpawnyStalker()
        {

            foreach (CrabSpriteLoader o in Items.objectList)
            {
                if (o.GetType() == typeof(Enemy2) && !o.alive)
                {
                    o.position = new Vector2(-100, positionY + 64);
                    o.UpdateArea();
                    o.rotation = 0;
                    o.speed = (eSpeed - 2);
                    o.alive = true;
                    break;
                }
            }
        }

        private void SpawnyStalker2()
        {

            foreach (CrabSpriteLoader o in Items.objectList)
            {
                if (o.GetType() == typeof(Enemy2) && !o.alive)
                {
                    o.position = new Vector2(900, positionY + 64);
                    o.UpdateArea();
                    o.rotation = 0;
                    o.speed = (eSpeed - 2);
                    o.alive = true;
                    break;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (displayStats)
            {
                spriteBatch.DrawString(Game1.font, "RandomStore " + RandomStore, new Vector2(0, 360), Color.White);
                spriteBatch.DrawString(Game1.font, "SpawnTime " + spawnTime, new Vector2(0, 300), Color.White);
                spriteBatch.DrawString(Game1.font, "SpawnRate " + spawnRate, new Vector2(0, 330), Color.White);
                spriteBatch.DrawString(Game1.font, "ESpeed " + eSpeed, new Vector2(0, 390), Color.White);
                spriteBatch.DrawString(Game1.font, "Wave " + Wave, new Vector2(0, 420), Color.White);
                spriteBatch.DrawString(Game1.font, "SpawnStalker " + spawnRateS, new Vector2(100, 420), Color.White);
                spriteBatch.DrawString(Game1.font, "Y? " + positionY, new Vector2(0, 450), Color.White);
            }

            base.Draw(spriteBatch);
        }
    }
}
