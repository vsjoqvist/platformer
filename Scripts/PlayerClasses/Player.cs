using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using platformer.Scripts.InputWrapperClass;
using System.Diagnostics;
using platformer.Scripts.SpriteClasses;

namespace platformer.Scripts.PlayerClasses
{
    public class Player : CollisionSprite
    {
        public InputWapper input;
        public float speed = 1;
        bool pressedThisFrame = false;

        public Player(Texture2D _texture, Vector2 _collisionBoxSize)
        {
            texture = _texture;
            origin = scale / 2;
            rectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            collisionBoxSize = _collisionBoxSize;
            velocity = Vector2.Zero;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites, List<CollisionSprite> collisionSprites)
        {
            GetNewVelocity();

            if (isCollsionActive)
            {
                foreach (var sprite in collisionSprites)
                {
                    if (sprite == this)
                        continue;

                    if (this.velocity.X >= 0 && this.IsTouchingLeft(sprite))
                    {
                        this.velocity.X = 0;
                        this.position.X = sprite.CollisonBox.Left - this.collisionBoxSize.X;
     
                    }
                    if (this.velocity.X <= 0 & this.IsTouchingRight(sprite))
                    {
                        this.velocity.X = 0;
                        this.position.X = sprite.CollisonBox.Right;

                    }
                    if (this.velocity.Y >= 0 && this.IsTouchingTop(sprite))
                    {
                        this.velocity.Y = 0;
                        this.position.Y = sprite.CollisonBox.Top - this.collisionBoxSize.Y;
   
                    }
                    if (this.velocity.Y <= 0 && this.IsTouchingBottom(sprite))
                    {
                        this.velocity.Y = 0;
                        this.position.Y = sprite.CollisonBox.Bottom;
                 
                    }



                }
            }

            position += velocity;

            velocity = Vector2.Zero;

            void GetNewVelocity()
            {
                if (input == null)
                {

                }
                else
                {
                    if (Keyboard.GetState().IsKeyDown(input.Up))
                    {
                        velocity.Y = -speed * Game1.deltaTime;
                    }

                    if (Keyboard.GetState().IsKeyDown(input.Down))
                    {
                        velocity.Y = speed * Game1.deltaTime;
                    }

                    if (Keyboard.GetState().IsKeyDown(input.Right))
                    {
                        velocity.X = speed * Game1.deltaTime;
                    }

                    if (Keyboard.GetState().IsKeyDown(input.Left))
                    {
                        velocity.X = -speed * Game1.deltaTime;
                    }
                    
                    if (Keyboard.GetState().IsKeyDown(input.Toggle))
                    {
                        if (false)
                        {
                            if (isCollsionActive)
                            {
                                isCollsionActive = false;
                            }
                            else
                            {
                                isCollsionActive = true;
                            }

                            pressedThisFrame = true;
                        }
                    }
                    else
                    {
                        pressedThisFrame = false;
                    }

                }
            }


        }
    }
}