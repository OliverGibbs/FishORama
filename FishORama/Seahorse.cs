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
    /// CLASS: Seahorse - this class is structured as a FishORama engine Token class
    /// It contains all the elements required to draw a token to screen and update it (for movement etc)
    class Seahorse : IDraw
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

        Random rand;   // Declares a random instance of rand

        int xSpeed;                 // Declares an integer called xSpeed which will be used to hold the value of the xSpeed of the seahorse
        int ySpeed;                 // Declares an integer called ySpeed which will be used to hold the value of the ySpeed of the seahorse
        int seahorseWidth = 74;     // declares an integer called seahorseWidth and sets it to 74
        int seahorseHeight = 128;   // declares an integer called seahorseHeight and sets it to 128      
        int sinkRise;               // declares an integer called sinkRise
        int initialXSpeed;          // declares an integer called initialXSpeed
        int initialYSpeed;          // declares an integer called initialYSpeed
        int originalBehaviour;      // declares an integer called originalBehaviour
        float initialYpos;          // declares a float called initialYpos
        float tempYpos;             // declares a float called tempYpos



        /// CONSTRUCTOR: Seahorse Constructor
        /// The elements in the brackets are PARAMETERS
        public Seahorse(string pTextureID, float pXpos, float pYpos, Screen pScreen, ITokenManager pTokenManager, Random pRand)   // This allows rand to be used in the token generation
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

            // Class setup code

            xSpeed = rand.Next(2, 6);      // This sets xSpeed to be a random number between 2 and 5 when the code is intitialised 
            ySpeed = xSpeed;               // This sets ySpeed to be equal to xSpeed
            initialYpos = yPosition;       // this sets initialYpos to be equal to yPosition
            sinkRise = 0;                  // this sets sinkRise to the value of 0
            originalBehaviour = 0;         // this sets originalBehaviour to the value of 0
            initialYSpeed = ySpeed;        // this sets initialYSpeed to be equal to ySpeed
            initialXSpeed = xSpeed;        // this sets initialXSpeed to be equal to xSpeed
            
        }

        /// METHOD: Update - will be called repeatedly by the Update loop in Simulation
        public void Update()
        {
            // Movement code
            if (sinkRise != 1)                       // if sinkRise is not equal to 1                        
            {
                sinkRise = rand.Next(-1, 1000);      // sinkRise is set a random value between -1 and 999
                tempYpos = yPosition;                // tempYpos is set to yPosition
                          
            }
            if (sinkRise == 1)                       // if sinkRise is equal to 1               
            {
                xSpeed = 0;                          // xSpeed is set to 0 so that the piranha stops moving horizontally 
                ySpeed = 1;                          // ySpeed is set to 1                   
                if (yPosition == tempYpos + 50)      // if the seahorse's yPosition is equal to tempYpos + 50
                {                   
                    originalBehaviour++;             // originalBehaviour is incremented by 1
                } 
                if (yPosition == tempYpos - 50)      // if the seahorse's yPosition is equal to tempYpos - 50
                {
                    originalBehaviour++;             // originalBehaviour is incremented by 1
                }
                if (originalBehaviour == 2)          // if originalBehaviour is equal to 2
                {
                    xSpeed = initialXSpeed;          // xSpeed is set to initialXSpeed
                    ySpeed = initialYSpeed;          // ySpeed is set to initialYSpeed
                    originalBehaviour = 0;           // originalBehaviour is set to 0
                    sinkRise = 0;                    // sinkRise is set to 0
                }
                
            }
            if (xPosition > screen.width / 2 - seahorseWidth / 2)     // if the seahorse's xPosition is greater than the screen width / 2 
            {
                xDirection = -1;                                      // the seahorse's xDirection is reversed so that it remains on the screen
            }       
            if (xPosition < screen.width / -2 + seahorseWidth / 2)    // if the seahorse's xPosition is less than the lower bounds of the screen
            {
                xDirection = 1;                                       // the seahorse's xDirection is reversed so that it remains on the screen
            }            
            if (yPosition > screen.height / 2 - seahorseHeight / 2)   // if the seahorse's yPosition is greater than the screen height / 2
            {
                yDirection = -1;                                      // the seahorse's yDirection is reversed so that it remains on the screen
            }         
            if (yPosition < screen.height / -2 + seahorseHeight / 2)  // if the seahorse's yPosition is less than the screen height / - 2
            {
                yDirection = 1;                                       // the seahorse's yDirection is reversed so that it remains on the screen
            }
            if (yPosition >= initialYpos + 50)                        // if the seahorse's yPosition is greater than or equal to initialYpos + 50
            {
                yDirection = -1;                                      // the seahorse's yDirection is reversed to create a zig zag movement
            }
            if (yPosition <= initialYpos - 50)                        // if the seahorse's yPosition is less than or equal to initialYpos - 50
            {
                yDirection = 1;                                       // the seahorse's yDirection is reversed to create a zig zag movement 
            }
        
            xPosition += xSpeed * xDirection;   // xPosition which is the xPosition of the Seahorse is calculated by using its xSpeed * its xDirection this is a += as xDirection can be negative
            yPosition += ySpeed * yDirection;   // yPosition which is the yPosition of the Seahorse is calculated by using its ySpeed * its yDirection this is a += as yDirection can be negative
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
