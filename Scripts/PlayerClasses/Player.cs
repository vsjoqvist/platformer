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
        private Vector2 gravity = new Vector2(0, 1);
        public float gravityScale = 5;
        public bool ignoreGravity = false;
        private bool isGrounded = false;
        public float jumpForce = 1.5f;

        public Player(Texture2D _texture, int _collisionBoxWidth, int _collisionBoxHeight)
        {
            texture = _texture;
            origin = scale / 2;
            rectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            collisionBoxWidth = _collisionBoxWidth;
            collisionBoxHeight = _collisionBoxHeight;
            velocity = Vector2.Zero;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites, List<CollisionSprite> collisionSprites)
        {
            GetNewVelocity();

            
            if (!ignoreGravity)
            {
                velocity += (gravity * gravityScale) * Game1.deltaTime;

               
            }
            

            collide();
            position += velocity;
            velocity.X = 0;
            if (ignoreGravity)
            {
                velocity.Y = 0;
            }
            

            void GetNewVelocity()
            {
                if (input == null)
                {

                }
                else
                {
                    if (Keyboard.GetState().IsKeyDown(input.Up))
                    {
                        if (ignoreGravity)
                        {
                            velocity.Y = -speed * Game1.deltaTime;
                        }
                        else if (isGrounded)
                        {

                            velocity.Y = -jumpForce;
                            isGrounded = false;
                        }


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
                        if (!pressedThisFrame)
                        {
                            if (ignoreGravity)
                            {
                                ignoreGravity = false;
                            }
                            else
                            {
                                ignoreGravity = true;
                            }

                            if (isCollsionActive)
                            {
                                isCollsionActive = false;
                                color.A = 100;
                            }
                            else
                            {
                                isCollsionActive = true;
                                color.A = 255;
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

            void collide()
            {
                if (isCollsionActive)
                {
                    foreach (var sprite in collisionSprites)
                    {
                        if (sprite == this)
                            continue;

                        if (this.velocity.X > 0 && this.IsTouchingLeft(sprite))
                        {
                            this.velocity.X = 0;
                            this.intPosition.X = sprite.CollisonBox.Left - this.collisionBoxWidth;
                            isGrounded = true;
                            if (Game1.Debugging)
                            {
                                Debug.WriteLine("colluided right");
                            }

                        }
                        if (this.velocity.X < 0 & this.IsTouchingRight(sprite))
                        {
                            this.velocity.X = 0;
                            this.intPosition.X = sprite.CollisonBox.Right;
                            isGrounded = true;
                            if (Game1.Debugging)
                            {
                                Debug.WriteLine("colluided left");
                            }

                        }
                        if (this.velocity.Y > 0 && this.IsTouchingTop(sprite))
                        {
                            this.velocity.Y = 0;
                            this.intPosition.Y = sprite.CollisonBox.Top - this.collisionBoxHeight;
                            isGrounded = true;
                            if (Game1.Debugging)
                            {
                                Debug.WriteLine("colluided down");
                            }

                        }
                        if (this.velocity.Y < 0 && this.IsTouchingBottom(sprite))
                        {
                            this.velocity.Y = 0;
                            this.intPosition.Y = sprite.CollisonBox.Bottom;
                            if (Game1.Debugging)
                            {
                                Debug.WriteLine("colluided up");
                            }

                        }



                    }
                }
            }


        }
    }
}