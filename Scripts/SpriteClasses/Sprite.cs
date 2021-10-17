using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace platformer.Scripts.SpriteClasses
{
    public class Sprite
    {
        public Vector2 position = Vector2.Zero;
        
        public Vector2 scale = new Vector2(1, 1);
        protected Vector2 origin;
        public float rotation = 0;
        public int spriteLayer = 1;
        protected Texture2D texture;
        public Color color = Color.White;
        protected Rectangle rectangle;
        public Rectangle Rectangle { get => rectangle; }
        


        public virtual void Update(GameTime gameTime, List<Sprite> sprites, List<CollisionSprite> collisionSprites)
        {

        }

      

        public virtual void Draw(SpriteBatch spriteBatch, List<Sprite> sprites)
        {
           
            spriteBatch.Draw(texture, position, rectangle, color, rotation, origin, scale, SpriteEffects.None, spriteLayer);
        }

        




        public Sprite(Texture2D _texture)
        {
            texture = _texture;
            origin = scale / 2;
            rectangle = new Rectangle(0, 0, texture.Width, texture.Height);
        }

        public Sprite()
        {

        }



    }
}
