using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace platformer.Scripts.SpriteClasses
{
    public class CollisionSprite: Sprite
    {
        public bool isCollsionActive = true;
        protected int collisionBoxWidth, collisionBoxHeight;
        public Vector2 velocity;

        protected Texture2D _rectangleTexture;
        public bool showCollisionBox = false;
        public Rectangle CollisonBox { get { return new Rectangle((int)intPosition.X - 1, (int)intPosition.Y - 1,  collisionBoxWidth, collisionBoxHeight); } }
        public CollisionSprite(Texture2D _texture, int _collisionBoxWidth, int _collisionBoxHeight, GraphicsDevice graphicsDevice)
        {
            
            texture = _texture;
            origin = scale / 2;
            rectangle = new Rectangle(0, 0, texture.Width, texture.Height);
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
                    spriteBatch.Draw(_rectangleTexture, intPosition, Color.Black);
            }
        }



        private void SetCollisionBoxTexture(GraphicsDevice graphicsDevice, Texture2D texture)
        {
            var colours = new List<Color>();

            for (int y = 0; y < texture.Height; y++)
            {
                for (int x = 0; x < texture.Width; x++)
                {
                    if (y == 0 || // On the top
                        x == 0 || // On the left
                        y == texture.Height - 1 || // on the bottom
                        x == texture.Width - 1) // on the right
                    {
                        colours.Add(new Color(255, 255, 255, 255)); // white
                    }
                    else
                    {
                        colours.Add(new Color(255, 255, 255, 50)); // transparent 
                    }
                }
            }

            _rectangleTexture = new Texture2D(graphicsDevice, texture.Width, texture.Height);
            _rectangleTexture.SetData<Color>(colours.ToArray());
        }



        #region Colloision
        protected bool IsTouchingLeft(CollisionSprite sprite)
        {
            return this.CollisonBox.Right + this.velocity.X > sprite.CollisonBox.Left &&
              this.CollisonBox.Left + this.velocity.X < sprite.CollisonBox.Left &&
              this.CollisonBox.Bottom + this.velocity.Y > sprite.CollisonBox.Top &&
              this.CollisonBox.Top + this.velocity.Y < sprite.CollisonBox.Bottom;
        }

        protected bool IsTouchingRight(CollisionSprite sprite)
        {
            return this.CollisonBox.Left + this.velocity.X < sprite.CollisonBox.Right &&
              this.CollisonBox.Right + this.velocity.X > sprite.CollisonBox.Right &&
              this.CollisonBox.Bottom + this.velocity.Y > sprite.CollisonBox.Top &&
              this.CollisonBox.Top + this.velocity.Y < sprite.CollisonBox.Bottom;
        }

        protected bool IsTouchingTop(CollisionSprite sprite)
        {
            return this.CollisonBox.Bottom + this.velocity.Y > sprite.CollisonBox.Top &&
              this.CollisonBox.Top + this.velocity.Y < sprite.CollisonBox.Top &&
              this.CollisonBox.Right + this.velocity.X > sprite.CollisonBox.Left &&
              this.CollisonBox.Left + this.velocity.X < sprite.CollisonBox.Right;
        }

        protected bool IsTouchingBottom(CollisionSprite sprite)
        {
            return this.CollisonBox.Top + this.velocity.Y < sprite.CollisonBox.Bottom &&
              this.CollisonBox.Bottom + this.velocity.Y > sprite.CollisonBox.Bottom &&
              this.CollisonBox.Right + this.velocity.X > sprite.CollisonBox.Left &&
              this.CollisonBox.Left + this.velocity.X < sprite.CollisonBox.Right;
        }

        #endregion
    }
}
