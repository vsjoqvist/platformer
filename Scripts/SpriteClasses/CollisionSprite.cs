using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended;


namespace platformer.Scripts.SpriteClasses
{
    public class CollisionSprite: Sprite
    {
        public bool isCollsionActive = false;
        protected int collisionBoxWidth, collisionBoxHeight;
        public Vector2 velocity;
        public RectangleF CollisonBox { get { return new RectangleF(position.X, position.Y,  collisionBoxWidth, collisionBoxHeight); } }
        public CollisionSprite(Texture2D _texture, int _collisionBoxWidth, int _collisionBoxHeight)
        {
            
            texture = _texture;
            origin = scale / 2;
            rectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            collisionBoxWidth = _collisionBoxWidth;
            collisionBoxHeight = _collisionBoxHeight;
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
