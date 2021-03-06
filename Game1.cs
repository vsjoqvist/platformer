using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using platformer.Scripts.SpriteClasses;
using platformer.Scripts.PlayerClasses;
using InputWrapperClass;
using System.Diagnostics;
using fpsCounter;
using System;


namespace platformer
{
    public class Game1 : Game
    {

        public static float deltaTime;
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private RenderTarget2D renderTarget;
        private int gameWidth = 320;
        private int gameHeight = 180;
        private List<Player> _players;
        private List<Sprite> _sprites;
        private List<CollisionSprite> _collisionSprites;
        private Rectangle rect = new Rectangle(0, 0, 1920, 1080);
        private SimpleFps fps = new SimpleFps();
        public static bool Debugging = true;
        private SpriteFont font;
        private KeyboardState _currentKey;
        private KeyboardState _previousKey;
        private bool _showCollisionBox = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.ApplyChanges();
            renderTarget = new RenderTarget2D(GraphicsDevice, gameWidth, gameHeight);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            var test = Content.Load<Texture2D>("pice");
            var box = Content.Load<Texture2D>("Sprite-0001");
            font = Content.Load<SpriteFont>("File");


            _players = new List<Player>()
            {
                new Player(test, 4, 4, GraphicsDevice) {input = new InputWrapper()
                {
                    Up = Keys.W, Down = Keys.S, Right = Keys.D, Left = Keys.A, Toggle = Keys.Enter},
                    position = new Vector2(100, 100), speed = 25, color = new Color(255, 0, 0),
                    isCollsionActive = true, ignoreGravity = false
                }
                 
            };

            _collisionSprites = new List<CollisionSprite>();
            for (int j = 0; j < 3; j++)
            {
                for (int i = 0; i < 48; i++)
                {
                    _collisionSprites.Add(
                    new CollisionSprite(box, 8, 8, GraphicsDevice)
                    {
                        isCollsionActive = true,
                        position = new Vector2(0 + i * 8, 100 + j * 8),
                        scale = new Vector2(1, 1)

                    });
                }
            }

            _collisionSprites.Add(
                   new CollisionSprite(box, 8, 8, GraphicsDevice)
                   {
                       isCollsionActive = true,
                       position = new Vector2(100, 92),
                       scale = new Vector2(1, 1)

                   });


            _sprites = new List<Sprite>();
           
            
        }

        protected override void Update(GameTime gameTime)
        {



            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _previousKey = _currentKey;
            _currentKey = Keyboard.GetState();

            if (_currentKey.IsKeyUp(Keys.F1) && _previousKey.IsKeyDown(Keys.F1))
                _showCollisionBox = !_showCollisionBox;

            deltaTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000;

            foreach (var player in _players)
            {
                player.Update(gameTime, _sprites, _collisionSprites);
              
            }

       
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            fps.Update(gameTime);

            GraphicsDevice.SetRenderTarget(renderTarget);
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            foreach (var sprite in _sprites)
            {
                sprite.Draw(spriteBatch, _sprites);
            }

            foreach (var collisionSprite in _collisionSprites)
            {
                collisionSprite.Draw(spriteBatch, _sprites);
                collisionSprite.showCollisionBox = _showCollisionBox;
            }

            foreach (var player in _players)
            {
                player.Draw(spriteBatch, _sprites);
                player.showCollisionBox = _showCollisionBox;

            }


            spriteBatch.End();
            GraphicsDevice.SetRenderTarget(null);
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            spriteBatch.Draw(renderTarget, rect, Color.White);
            spriteBatch.DrawString(font, fps.msg.ToString(), new Vector2(50, 0), Color.White);
            spriteBatch.End();
            // TODO: Add your drawing code here


            base.Draw(gameTime);
        }
    }
}
