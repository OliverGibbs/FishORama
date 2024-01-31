using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FishORamaEngineLibrary;

namespace FishORama
{
    /// CLASS: Piranha - this class is structured as a FishORama engine Token class
    /// It contains all the elements required to draw a token to screen and update it (for movement etc)
    class Piranha1 : IDraw
    {
        // CLASS VARIABLES
        // Variables hold the information for the class
        // NOTE - these variables must be present for the class to act as a TOKEN for the FishORama engine
        private string textureID;               // Holds a string to identify asset used for this token
        private float xPosition;                // Holds the X coordinate for token position on screen
        private float yPosition;                // Holds the Y coordinate for token position on screen
        private int xDirection;                 // Holds the direction the token is currently moving - X value should be either -1 (left) or 1 (right)
        private int yDirection;                 // Holds the direction the token is currently moving - Y value should be either -1 (down) or 1 (up)
        private Screen screen;                  // Holds a reference to the screen dimansions (width and height)
        private ITokenManager tokenManager;     // Holds a reference to the TokenManager - for access to ChickenLeg variable

        // Class variables

        Random rand;              // Declares a random instance of rand

        int xSpeed;               // Declares an integer called xSpeed which will be used to hold the xSpeed of the Piranha
        int ySpeed;               // Declares an integer called ySpeed which will be used to hold the ySpeed of the Piranha
        int initialXSpeed;        // Declares an integer called initialXSpeed which is used to hold the initialXSpeed of the Piranha
        int piranhaWidth = 132;   // Declares an integer called piranhaWidth which is then assigned a value of 132
        int full;                 // declares an integer called full
        float tempXPos;           // declares a float called tempXPos


        /// CONSTRUCTOR: Piranha Constructor
        /// The elements in the brackets are PARAMETERS
        public Piranha1(string pTextureID, float pXpos, float pYpos, Screen pScreen, ITokenManager pTokenManager, Random pRand)   // This allows rand to be used in the token generation
        {
            // State initialisation (setup) for the object
            textureID = pTextureID;
            xPosition = pXpos;
            yPosition = pYpos;
            xDirection = 1;
            yDirection = 1;
            screen = pScreen;
            tokenManager = pTokenManager;
            rand = pRand;       // This allows rand to be used in the token generation so that only one declaration of rand is needed for the code 

            // Class setup
            
            xSpeed = rand.Next(2, 6);   // xSpeed is set a random value between 2 and 5
            initialXSpeed = xSpeed;     // initialXSpeed is set as the same value as xSpeed
            ySpeed = 0;                 // ySpeed is set to a value of 0

        }

        /// METHOD: Update - will be called repeatedly by the Update loop in Simulation
        public void Update()
        {
            // Movement code
            if (xPosition > screen.width / 2 - piranhaWidth / 2)      // if the piranha's xPosition is greater than the upper bounds of the screen
            {
                xDirection = -1;                                      // it's xDirection is reversed so that it remains on the screen
            }
            if (xPosition < screen.width / -2 + piranhaWidth / 2)     // if the piranha's xPosition is less than the lower bounds of the screen
            {
                xDirection = 1;                                       // it's xDirection is reversed so that it remains on the screen
            }

            if (tokenManager.ChickenLeg != null)        // if a ChickenLeg is present on the screen and full (extra element) is not equal to 1
            {
                xSpeed = 6;                                           // the piranha's xSpeed is set to 6
                ySpeed = 6;                                           // the piranha's ySpeed is set to 6
                if (xPosition > tokenManager.ChickenLeg.Position.X)   // if the piranha's xPosiiton is greater than the chickenLeg's xPosition 
                {
                    xDirection = -1;                                  // the piranha's xDirection is reversed 
                }
                if (xPosition < tokenManager.ChickenLeg.Position.X)   // if the piranha's xPosition is less than the ChickenLeg's xPosition
                {
                    xDirection = 1;                                   // the piranha's xDirection is reversed
                }
                if (xPosition >= tokenManager.ChickenLeg.Position.X - 10 && xPosition <= tokenManager.ChickenLeg.Position.X + 10)  // if the piranha's xPosition is greater than or equal to the chickenLeg's xPosition - 10 and if xPosition is less than or equal to the chickenLeg's xPosition + 10
                {
                    xSpeed = 0;                                       // the piranha's xSpeed is set to 0 so that it stops moving along the x axis
                }
                if (yPosition > tokenManager.ChickenLeg.Position.Y)   // if the piranha's yPosition is greater than the chickenLeg's yPosition
                {
                    yDirection = -1;                                  // the piranha's yDirection is reversed
                }
                if (yPosition < tokenManager.ChickenLeg.Position.Y)   // if the piranha's yPosition is less than the chickenLeg's yPosition
                {
                    yDirection = 1;                                   // the piranha's yDirection is reversed
                }
                if (yPosition >= tokenManager.ChickenLeg.Position.Y - 10 && yPosition <= tokenManager.ChickenLeg.Position.Y + 10)  // if the piranha's yPosition is greater than or equal to the chickenLeg's yPosition -10 and if the piranah's yPosition is less than or equal to the chickenLeg's yPosition + 10
                {
                    ySpeed = 0;                                       // the piranha's ySpeed is set to 0 so that it stops moving along the y axis
                }
              

                if (xPosition >= tokenManager.ChickenLeg.Position.X - 10 && xPosition <= tokenManager.ChickenLeg.Position.X + 10 && yPosition >= tokenManager.ChickenLeg.Position.Y - 10 && yPosition <= tokenManager.ChickenLeg.Position.Y + 10) // if the piranha's xPosition is greater than or equal to the chickenLeg's xPosition - 10 and the piranha's xPosition is less than or equal to the chickenLeg's xPosition + 10 and the piranha's yPosition is greater than or equal to the chickenLeg's yPosition - 10 and the pirnaha's yPosition is less than or equal to the chickenLeg's yPosition + 10
                    
                {
                    tokenManager.ChickenLeg.Remove();                 // the chickenLeg is removed from the screen                                           
                    ySpeed = 0;                                       // the piranha's ySpeed is set to 0 returning it to it's original behaviour
                    full = 1;                                         // full is set to 1   
                    tempXPos = xPosition;                             // tempXPos is set to the piranha's xPosition
                }

            }

            if (full == 1)                                            // if full is equal to 1  
            {
                xSpeed = 1;                                           // the piranha's xSpeed is set to 1
                if (xPosition == tempXPos + 100)                      // if the piranha's xPosition is equal to tempXPos + 100
                {
                    xSpeed = initialXSpeed;                           // the piranha's xSpeed returns to its initialXSpeed
                    full = 0;                                         // full is set to 0
                }
                if (xPosition == tempXPos - 100)                      // if the piranha's xPosition is equal to tempXpos - 100
                {
                    xSpeed = initialXSpeed;                           // the piranha's xSpeed returns to its initialXSpeed
                    full = 0;                                         // full is set to 0
                }
            }

            xPosition += xSpeed * xDirection;   // xPosition which is the xPosition of the piranha is calculated by using its xSpeed * its xDirection this is a += so that it can constantly update
            yPosition += ySpeed * yDirection;   // yPosition which is the yPosition of the piranha is calculated by using its ySpeed * its yDirection this is a += so that it can constantly update

        }

        /// METHOD: Draw - Called repeatedly by FishORama engine to draw token on screen
        /// DO NOT ALTER, and ensure this Draw method is in each token (fish) class
        public void Draw(IGetAsset pAssetManager, SpriteBatch pSpriteBatch)
        {
            Asset currentAsset = pAssetManager.GetAssetByID(textureID); // Get this token's asset from the AssetManager

            SpriteEffects horizontalDirection; // Stores whether the texture should be flipped horizontally

            if (xDirection < 0)
            {
                // If the token's horizontal direction is negative, draw it reversed
                horizontalDirection = SpriteEffects.FlipHorizontally;
            }
            else
            {
                // If the token's horizontal direction is positive, draw it regularly
                horizontalDirection = SpriteEffects.None;
            }

            // Draw an image centered at the token's position, using the associated texture / position
            pSpriteBatch.Draw(currentAsset.Texture,                                             // Texture
                              new Vector2(xPosition, yPosition * -1),                                // Position
                              null,                                                             // Source rectangle (null)
                              Color.White,                                                      // Background colour
                              0f,                                                               // Rotation (radians)
                              new Vector2(currentAsset.Size.X / 2, currentAsset.Size.Y / 2),    // Origin (places token position at centre of sprite)
                              new Vector2(1, 1),                                                // scale (resizes sprite)
                              horizontalDirection,                                              // Sprite effect (used to reverse image - see above)
                              1);                                                               // Layer depth
        }
    }
}
