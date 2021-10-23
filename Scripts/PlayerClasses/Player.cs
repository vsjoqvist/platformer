using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using InputWrapperClass;
using System.Diagnostics;
using platformer.Scripts.SpriteClasses;
using Collisions;


namespace platformer.Scripts.PlayerClasses
{
    public class Player : CollisionSprite
    {
        public InputWrapper input;
        public float speed = 1;
        bool pressedThisFrame = false;
        private Vector2 gravity = new Vector2(0, 1);
        public float gravityScale = 3;
        public bool ignoreGravity = false;
        private bool isGrounded = false;
        public float jumpForce = 1.5f;

        public Player(Texture2D _texture, int _collisionBoxWidth, int _collisionBoxHeight, GraphicsDevice graphicsDevice) : base(_texture, _collisionBoxWidth, _collisionBoxHeight, graphicsDevice)
        {
            
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites, List<CollisionSprite> collisionSprites)
        {
            bool collided = false;
            GetNewVelocity();
            var velocitaaay = velocity;
            if (!ignoreGravity)
            {
                velocity += (gravity * gravityScale) * Game1.deltaTime;

               
            }
            
            if (isCollsionActive)
            {
                foreach (var c in collisionSprites)
                {
                    if (c.isCollsionActive)
                    {
                        collide(c.collisonBox);
                    }
                }

                if (collided)
                {
                    isGrounded = true;
                } 
                else
                {
                    isGrounded = false;
                }
            }
            

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
                    #region input
                    if (Keyboard.GetState().IsKeyDown(input.Up))
                    {
                        if (ignoreGravity)
                        {
 
                            velocity.Y = -speed * Game1.deltaTime;
                        }
                        else if (isGrounded)
                        {
                            velocity.Y = -jumpForce;
                        }

                        

                    }

                    if (Keyboard.GetState().IsKeyDown(input.Down))
                    {
                        if (ignoreGravity)
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
                    #endregion

                }
            }

            void collide(AABB otherBox)
            {
                Sweep _sweep = this.collisonBox.sweepAABB(otherBox, velocity);

                if (_sweep.hit != null)
                {
                    collided = true;
                    
                    if (_sweep.hit.normal.X == -1 || _sweep.hit.normal.X == 1)
                    {
                        velocity.X = 0;

                    }

                    if (_sweep.hit.normal.Y == -1 || _sweep.hit.normal.Y == 1)
                    {
                        velocity.Y = 0;
                        
 
                    }
                    position += _sweep.hit.delta;
                }
                
                
                
            }


        }
    }
}