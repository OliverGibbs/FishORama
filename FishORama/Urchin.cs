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
    /// CLASS: Urchin - this class is structured as a FishORama engine Token class
    /// It contains all the elements required to draw a token to screen and update it (for movement etc)
    class Urchin : IDraw
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

        Random rand;             // Declares a random instance of rand

        int xSpeed;              // Declares an integer called xSpeed which will be used to hold the value of the xSpeed of the urchin
        int urchinWidth = 180;   // declares an integer called urchinWidth which is set to the value of 180
        int initialXSpeed;       // declares an integer called initialXSpeed


        /// CONSTRUCTOR: Urchin Constructor
        /// The elements in the brackets are PARAMETERS
        public Urchin(string pTextureID, float pXpos, float pYpos, Screen pScreen, ITokenManager pTokenManager, Random pRand)   // This allows rand to be used in the token generation
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

            xSpeed = rand.Next(1, 4);      // This sets xSpeed to be a random value between 1 and 3 
            initialXSpeed = xSpeed;        // this sets initialXSpeed to be the same value as xSpeed
        }

        /// METHOD: Update - will be called repeatedly by the Update loop in Simulation
        public void Update()
        {
            // Movement code
            if (xPosition > screen.width / 2 - urchinWidth / 2)     // if the xPosition of the urchin is greater than the upper bounds of the x side of the screen
            {
                xDirection = -1;                                    // the xDirection of the urchin is reversed so that it remains on screen
                xSpeed = initialXSpeed;                             // the urchin's xSpeed is set to it's initialXSpeed
            }
            if (xPosition < screen.width / -2 + urchinWidth / 2)    // if the xPosition of the urchin is less than the lower bounds of the x side of the screen
            {
                xDirection = 1;                                     // the xDirection of the urchin is reversed so that it remains on screen
                xSpeed = initialXSpeed;                             // the urchin's xSpeed is set to it's initialXSpeed
            }   
            if (tokenManager.ChickenLeg != null && yPosition >= tokenManager.ChickenLeg.Position.Y - 20 && yPosition <= tokenManager.ChickenLeg.Position.Y + 20)  // if a chicken leg is on screen and the urchin's yPosition is greater than or equal to the chickenleg's yPosition - 20 and the urchin's yPosition is less than or equal to the chickenleg's yPosition + 20
            {
                xSpeed = 6;                                                     // the urchin's xSpeed is set to 6
                if (xPosition > tokenManager.ChickenLeg.Position.X)             // if the urchin's xPosition is greater than the chickenLeg's xPosition
                {
                    xDirection = 1;                                             // the urchin's xDirection is reversed
                    if (xPosition > screen.width / 2 - urchinWidth / 2)         // if the urchin's xPosition is greater than the upper bounds of the screen width
                    {
                        xDirection = -1;                                        // the urchin's xDirection is reversed so that it remains on the screen
                        xSpeed = initialXSpeed;                                 // the urchin's xSpeed is set to it's initialXSpeed
                    }
                }
                if (xPosition < tokenManager.ChickenLeg.Position.X)             // if the urchin's xPosition is less than the chickenLeg's xPosition
                {
                    xDirection = -1;                                            // the urchin's xDirection is reversed
                    if (xPosition < screen.width / -2 + urchinWidth / 2)        // if the urchin's xPosition is less than the lower bounds of the screen width
                    {
                        xDirection = 1;                                         // the urchin's xDirection is reversed so that it remains on the screen
                        xSpeed = initialXSpeed;                                 // the urchin's xSpeed is set to it's initialXSpeed                       
                    }
                }
            }  
            
            xPosition += xSpeed * xDirection;   // xPosition which is the xPosition of the Urchin is calculated by using its xSpeed * its xDirection this is a += as xDirection can be negative
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
