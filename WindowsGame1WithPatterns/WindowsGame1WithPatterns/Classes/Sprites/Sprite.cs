using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Jump.Classes.Sprites
{
    abstract class Sprite : DrawableGameComponent
    {
        #region Variables&Properties
        /// <summary>
        /// Origin for rotation
        /// </summary>
        protected Vector2 origin; 
        /// <summary>
        /// Position for sprite
        /// </summary>
        protected Vector2 position;
        /// <summary>
        /// Color for sprite
        /// </summary>
        protected Color color;
        /// <summary>
        /// Rotate for sprite
        /// </summary>
        protected float rotate;
        /// <summary>
        /// Scale for sprite
        /// </summary>
        protected float scale;
        /// <summary>
        /// Effect for sprite
        /// </summary>
        protected SpriteEffects spriteEffects;
        /// <summary>
        /// Pixels outside of texture to not collide with
        /// </summary>
        protected int collisionOffset; 
        /// <summary>
        /// Index (x,y) for current frame in spritesheet/sprite
        /// </summary>
        protected Point frameCurrent;
        /// <summary>
        /// Size for each individual frame in spritesheet/sprite
        /// </summary>
        protected Point frameSize;
        /// <summary>
        /// Milliseconds between framechanges
        /// </summary>
        protected int MillisecondsPerFrame; 
        /// <summary>
        /// The texture (image/sprite)
        /// </summary>
        protected Texture2D texture;
        /// <summary>
        /// Number of columns/rows in spritesheet
        /// </summary>
        protected Point sheetSize; 
        /// <summary>
        /// Velocity for sprite
        /// </summary>
        protected Vector2 velocity; 
        /// <summary>
        /// Milliseconds since last frame was drawn
        /// </summary>
        protected int TimeSinceLastFrame = 0; 
        /// <summary>
        /// Minimum direction in x one sprite can be
        /// </summary>
        protected const int MinimumX = 0;
        /// <summary>
        /// Minimum direction in y one sprite can be
        /// </summary>
        protected const int MinimumY = 0;
        /// <summary>
        /// Default ms per frame. 60 times per secondis default.
        /// </summary>
        protected const int DefaultMillisecondsPerFrame = 16; 
        #region Properties

        /// <summary>
        /// Get/set position
        /// </summary>
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        /// <summary>
        /// Get/set color
        /// </summary>
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }
        /// <summary>
        /// Get/set velocity
        /// </summary>
        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }
        /// <summary>
        /// Get/set texture
        /// </summary>
        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }
        /// <summary>
        /// Rectangle used for collsion detection
        /// </summary>
        public Rectangle CollisionRectangle
        {
            get
            {
                return new Rectangle
                    (
                    (int)position.X + collisionOffset,
                    (int)position.Y + collisionOffset,
                    frameSize.X - (collisionOffset * 2),
                    frameSize.Y - (collisionOffset * 2)
                    );
            }
        }
        #endregion
        #endregion
        protected Sprite(Game game) : base(game) {}
        /// <summary>
        ///  Standard constructor used. Calls the other one.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="texture"></param>
        /// <param name="position"></param>
        /// <param name="frameSize"></param>
        /// <param name="frameCurrent"></param>
        /// <param name="sheetSize"></param>
        /// <param name="rotate"></param>
        /// <param name="origin"></param>
        /// <param name="scale"></param>
        /// <param name="spriteEffects"></param>
        /// <param name="velocity"></param>
        /// <param name="collisionOffset"></param>
        /// <param name="timeSinceLastFrame"></param>
        protected Sprite(Game game, Texture2D texture, Vector2 position, Point frameSize, Point frameCurrent,
                                   Point sheetSize,
                                   float rotate, Vector2 origin, float scale, SpriteEffects spriteEffects, Vector2 velocity,
                                   int collisionOffset, int timeSinceLastFrame)
            : this(game,
                texture, position, frameSize, frameCurrent, sheetSize, rotate, origin, scale, spriteEffects, velocity,
                collisionOffset, timeSinceLastFrame, DefaultMillisecondsPerFrame){}

        /// <summary>
        /// Used when you want to have slower/faster animations by adjusting milliseconds per frame.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="texture"></param>
        /// <param name="position"></param>
        /// <param name="frameSize"></param>
        /// <param name="frameCurrent"></param>
        /// <param name="sheetSize"></param>
        /// <param name="rotate"></param>
        /// <param name="origin"></param>
        /// <param name="scale"></param>
        /// <param name="spriteEffects"></param>
        /// <param name="velocity"></param>
        /// <param name="collisionOffset"></param>
        /// <param name="millisecondsPerFrame"></param>
        /// <param name="timeSinceLastFrame"></param>
        protected Sprite(Game game, Texture2D texture, Vector2 position, Point frameSize, Point frameCurrent,
                                   Point sheetSize,
                                   float rotate, Vector2 origin, float scale, SpriteEffects spriteEffects, Vector2 velocity,
                                   int collisionOffset, int millisecondsPerFrame, int timeSinceLastFrame) : base(game)
        {
            this.collisionOffset = collisionOffset;
            this.frameCurrent = frameCurrent;
            this.frameSize = frameSize;
            MillisecondsPerFrame = millisecondsPerFrame;
            this.position = position;
            this.sheetSize = sheetSize;
            this.velocity = velocity;
            this.texture = texture;
            TimeSinceLastFrame = timeSinceLastFrame;
            this.spriteEffects = spriteEffects;
            this.scale = scale;
            this.rotate = rotate;
            this.origin = origin;
         
        }

        /// <summary>
        /// Handles update of animation of sprites
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="clientBounds"></param>
        public virtual void Update(GameTime gameTime, Rectangle clientBounds)
        {
            //Count time since last frame
            TimeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;

            //If its not time to update frame, return
            if (TimeSinceLastFrame <= MillisecondsPerFrame) return;

            TimeSinceLastFrame -= MillisecondsPerFrame;

            //Logic for choosing frames from spritesheets
            ++frameCurrent.X;
            if (frameCurrent.X < sheetSize.X) return;

            frameCurrent.X = 0;
            ++frameCurrent.Y;
            if (frameCurrent.Y >= sheetSize.Y)
                frameCurrent.Y = 0;
        }

        /// <summary>
        /// Draws sprites
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                texture,
                position,
                new Rectangle(frameCurrent.X * frameSize.X, frameCurrent.Y * frameSize.Y, frameSize.X, frameSize.Y),
                Color.White, rotate, origin,
                scale, spriteEffects, 0);
        }
    }
}
