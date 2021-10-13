using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace platformer.Scripts.SpriteClasses
{
    public class CollisionSprite: Sprite
    {
        public bool isCollsionActive = false;

        public Vector2 velocity;
        protected Vector2 collisionBoxSize;
        public Rectangle CollisonBox { get { return new Rectangle((int)position.X, (int)position.Y,  (int)collisionBoxSize.X, (int)(collisionBoxSize.Y)); } }
        public CollisionSprite(Texture2D _texture, Vector2 _collisionBoxSize)
        {
            
            texture = _texture;
            origin = scale / 2;
            rectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            collisionBoxSize = _collisionBoxSize;
            velocity = Vector2.Zero;
        }

        public CollisionSprite()
        {
            
            
        }

        #region Colloision
        protected bool IsTouchingLeft(CollisionSprite sprite)
        {
            return this.CollisonBox.Right + this.velocity.X > sprite.CollisonBox.Left &&
              this.CollisonBox.Left < sprite.CollisonBox.Left &&
              this.CollisonBox.Bottom > sprite.CollisonBox.Top &&
              this.CollisonBox.Top < sprite.CollisonBox.Bottom;
        }

        protected bool IsTouchingRight(CollisionSprite sprite)
        {
            return this.CollisonBox.Left + this.velocity.X < sprite.CollisonBox.Right &&
              this.CollisonBox.Right > sprite.CollisonBox.Right &&
              this.CollisonBox.Bottom > sprite.CollisonBox.Top &&
              this.CollisonBox.Top < sprite.CollisonBox.Bottom;
        }

        protected bool IsTouchingTop(CollisionSprite sprite)
        {
            return this.CollisonBox.Bottom + this.velocity.Y > sprite.CollisonBox.Top &&
              this.CollisonBox.Top < sprite.CollisonBox.Top &&
              this.CollisonBox.Right > sprite.CollisonBox.Left &&
              this.CollisonBox.Left < sprite.CollisonBox.Right;
        }

        protected bool IsTouchingBottom(CollisionSprite sprite)
        {
            return this.CollisonBox.Top + this.velocity.Y < sprite.CollisonBox.Bottom &&
              this.CollisonBox.Bottom > sprite.CollisonBox.Bottom &&
              this.CollisonBox.Right > sprite.CollisonBox.Left &&
              this.CollisonBox.Left < sprite.CollisonBox.Right;
        }

        #endregion
    }
}
