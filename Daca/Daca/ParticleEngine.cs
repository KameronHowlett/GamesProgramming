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
    public class ParticleEngine
    {
        private Random random;//Random number generator
        public Vector2 EmitterLocation {get; set;}
        private List<Particle> particles;
        private List<Texture2D> textures;

        public ParticleEngine(List<Texture2D> textures, Vector2 location)
        {
            EmitterLocation = location;
            this.textures = textures;
            this.particles = new List<Particle>();
            random = new Random();
        }

        private Particle GenerateNewParticle()
        {
            Texture2D texture = textures[random.Next(textures.Count)];
            Vector2 position = EmitterLocation;
            Vector2 velocity = new Vector2(
                1f * (float)(random.NextDouble() * 2 -1),
                1f * (float)(random.NextDouble() * 2 -1));
            float angle = 0;
            float angularVelocity = 0.1f * (float)(random.NextDouble() * 2 - 1);
            float Ovcolor = (float)(random.NextDouble() + 0.3 );
            Color color = new Color(
                Ovcolor,Ovcolor,Ovcolor);
            float size = (float)random.NextDouble();

            int ttl = 5 + random.Next(20);

            return new Particle(texture, position, velocity, angle, angularVelocity, color, size, ttl);

        }

        private Particle GenerateNewBulletParticle()
        {
            Texture2D texture = textures[4];
            Vector2 position = EmitterLocation;
            Vector2 velocity = new Vector2(0, 0);
            float angle = 0;
            float angularVelocity = 0f;
            Color color = new Color(
                0, 0, 0);
            float size = 1;

            int ttl = 5 + random.Next(20);

            return new Particle(texture, position, velocity, angle, angularVelocity, color, size, ttl);

        }
        //~~~~~Update date the particles~~~~~~~
        public void Update()
        {

            int total = 1;//Max number of particals at a time
             for (int i = 0; i< total; i++)
             {
                 particles.Add(GenerateNewParticle());//Add new particle when less than 10 on screen
             }

            for (int particle = 0; particle < particles.Count; particle++)
            {
                particles[particle].Update();
                if(particles[particle].TTL <=0)
                {
                    particles.RemoveAt(particle);
                    particle--;//When dead remove from particle count
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();//Tells particles to draw ones self
            for (int index = 0; index < particles.Count; index++)
            {
                particles[index].Draw(spriteBatch);
            }
            spriteBatch.End();
        }
    }
}
