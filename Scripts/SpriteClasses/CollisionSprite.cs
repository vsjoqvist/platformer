using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Collisions;



namespace platformer.Scripts.SpriteClasses
{
    public class CollisionSprite: Sprite
    {

        public bool isCollsionActive = true;
        protected int collisionBoxWidth, collisionBoxHeight;
        public Vector2 velocity;

        protected Texture2D _rectangleTexture;
        public bool showCollisionBox = false;
        public AABB collisonBox { get { return new AABB(new Vector2(position.X - origin.X + collisionBoxWidth / 2, position.Y - origin.Y + collisionBoxHeight / 2), new Vector2(collisionBoxWidth / 2, collisionBoxHeight / 2)); } }
        public CollisionSprite(Texture2D _texture, int _collisionBoxWidth, int _collisionBoxHeight, GraphicsDevice graphicsDevice) : base(_texture)
        {
           
            collisionBoxWidth = _collisionBoxWidth;
            collisionBoxHeight = _collisionBoxHeight;
            velocity = Vector2.Zero;
            SetCollisionBoxTexture(graphicsDevice, _texture);
        }

        public override void Draw(SpriteBatch spriteBatch, List<Sprite> sprites)
        {
            base.Draw(spriteBatch, sprites);

            if (showCollisionBox)
            {
                if (_rectangleTexture != null)
                {
                    spriteBatch.Draw(_rectangleTexture, position - origin, Color.Green);
                    
                }
                    
            }
        }



        private void SetCollisionBoxTexture(GraphicsDevice graphicsDevice, Texture2D texture)
        {
            var colours = new List<Color>();

            for (int y = 0; y < collisonBox.half.Y * 2; y++)
            {
                for (int x = 0; x < collisonBox.half.X * 2; x++)
                {
                    if (y == 0 || // On the top
                        x == 0 || // On the left
                        y == collisionBoxHeight - 1 || // on the bottom
                        x == collisionBoxWidth - 1) // on the right
                    {
                        colours.Add(new Color(255, 255, 255, 255)); // white
                    }
                    else
                    {
                        colours.Add(new Color(0, 0, 0, 0)); // transparent 
                    }
                }
            }

            _rectangleTexture = new Texture2D(graphicsDevice, collisionBoxWidth, collisionBoxHeight);
            _rectangleTexture.SetData<Color>(colours.ToArray());
        }
    }
}
