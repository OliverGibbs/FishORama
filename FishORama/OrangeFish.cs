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
    /// CLASS: OrangeFish - this class is structured as a FishORama engine Token class
    /// It contains all the elements required to draw a token to screen and update it (for movement etc)
    class OrangeFish : IDraw
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

        // CLass Variables

        Random rand;                 // Declares a random instance of rand

        float xSpeed;                // xSpeed is declared as a float so that it can store decimal values
        float ySpeed;                // ySpeed is declared as a float so that it can store decimal values
        int flipChance;              // An integer called flipChance is declared
        int orangeFishWidth = 128;   // An integer called orangeFishWidth is declared and assigned the value of 128
        int orangeFishHeight = 86;   // An integer called orangeFishHeight is declared and assigned the value of 86
        


        /// CONSTRUCTOR: OrangeFish Constructor
        /// The elements in the brackets are PARAMETERS
        public OrangeFish(string pTextureID, float pXpos, float pYpos, Screen pScreen, ITokenManager pTokenManager, Random pRand)   // This allows rand to be used in the token generation
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

            // CLass setup code

            xSpeed = rand.Next(2, 6);      // This sets xSpeed to be a random number between 2 and 5 when the code is intitialised 

            ySpeed = xSpeed / 2;           // This sets ySpeed to be half of whatever xSpeed is set to

            flipChance = 1;                // flipChance is the chance that the fish will flip direction when hitting an edge of the screen

        }

        /// METHOD: Update - will be called repeatedly by the Update loop in Simulation
        public void Update()
        {
            // Movement behaviour code
            if (xPosition > screen.width / 2 - orangeFishWidth / 2)    // if the orangeFish's xPosition is greater than the upper bounds of the x side of the screen
            {
                xDirection = -1;                              // xDirection is reversed so that it remains on the screen       
                flipChance = rand.Next(-1, 3);                // flipChance is set a random number between -1 and 2 
                if (flipChance == -1)                         // if flipChance is set to -1 which is a 25% chance of occuring                
                {
                    yDirection = -1;                          // the orangeFish's yDirection is reversed
                }
                else
                {
                    yDirection = 1;                           // if flipchance is set to anything other than -1, then the orangeFish's yDirection remains the same
                }
            
            }
            else if (xPosition < screen.width / -2 + orangeFishWidth / 2)    // if the orangeFish's xPosition is less than the lower bounds of the x side of the screen
            {
                xDirection = 1;                              // xDirection is reversed so that it remains on the screen
                flipChance = rand.Next(-1, 3);               // flipChance is set a random number between -1 and 2
                if (flipChance == -1)                        // if flipChance is set to -1 which is a 25% chance of occuring
                {
                    yDirection = -1;                         // the orangeFish's yDirection is reversed
                }
                else
                {
                    yDirection = 1;                          // if flipChance is set to anything other than -1, then the orangeFish's yDirection remains the same                  
                }

            }

            if (yPosition > screen.height / 2 - orangeFishHeight / 2)  // if the orangeFish's yPosition is greater than the upper bounds of the y side of the screen
            {
                yDirection = -1;                            // yDirection is reversed so that it remains on the screen
                flipChance = rand.Next(-1, 1);              // flipChance is set a random number between -1 and 0
                if (flipChance == -1)                       // if flipChance is set to -1 which is a 50% chance of occuring
                {
                    xDirection = -1;                        // the orangeFish's xDirection is reversed
                }
                else
                {
                    xDirection = 1;                         // if flipChance is set to anything other than -1, then the orangeFish's xDirection remains the same
                }
            }

            else if (yPosition < screen.height / -2 + orangeFishHeight / 2)  // if the orangeFish's yPosition is less than the lower bounds of the y side of the screen
            {
                yDirection = 1;                             // yDirection is reversed so that it reamins on the screen
                flipChance = rand.Next(-1, 1);              // flipChance is set a random number between -1 and 0
                if (flipChance == -1)                       // if flipChance is set to -1 which is a 50% chance of occuring
                {
                    xDirection = -1;                        // the orangeFish's xDirection is reversed
                }
                else
                {
                    xDirection = 1;                         // if flipChance is set to anything other than -1, then the orangeFish's xDirection remains the same
                }
            }

            xPosition += xSpeed * xDirection;   // xPosition which is the xPosition of the orangeFish is calculated by using its xSpeed * its xDirection this is a += as xDirection can be negative
            yPosition += ySpeed * yDirection;   // yPosition which is the yPosition of the orangeFish is calculated by using its ySpeed * its yDirection this is a += as yDirection can be negative


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
