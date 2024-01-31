using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using FishORamaEngineLibrary;

namespace FishORama
{
    /// CLASS: Simulation class - the main class users code in to set up a FishORama simulation
    /// All tokens to be displayed in the scene are added here
    public class Simulation : IUpdate, ILoadContent
    {
        // CLASS VARIABLES
        // Variables store the information for the class
        private IKernel kernel;                 // Holds a reference to the game engine kernel which calls the draw method for every token you add to it
        private Screen screen;                  // Holds a reference to the screeen dimensions (width, height)
        private ITokenManager tokenManager;     // Holds a reference to the TokenManager - for access to ChickenLeg variable

        /// PROPERTIES
        public ITokenManager TokenManager      // Property to access chickenLeg variable
        {
            set { tokenManager = value; }
        }

        // Variables to hold fish will be declared here
        OrangeFish orangeFish1; // creates a new orangeFish called orangeFish1
        BlueFish blueFish1;
        Piranha1 piranhaOne;
        Urchin[] urchinArray = new Urchin[3];
        Seahorse[] seahorseArray = new Seahorse[5];
        Piranha1[] piranhaArray = new Piranha1[6];

        int orangeFishWidth = 128;   // creates a new integer called orangeFishWidth and is assigned the value of 128
        int orangeFishHeight = 64;   // creates a new integer called orangeFishHeight and is assigned the value of 64
        int blueFishWidth = 128;     // creates a new integer called blueFishWidth and is assigned the value of 128 
        int blueFishHeight = 64;     // creates a new integer called blueFishHeight and is assigned the value of 64 
        int urchinWidth = 180;       // declares a new integer called urchinWidth
        int urchinHeight = 112;      // declares a new integer called urchinHeight
        int seahorseWidth = 74;      // declares a new integer called seahorseWidth
        int seahorseHeight = 128;    // declares a new integer called seahorseHeight
        int piranhaWidth = 132;      // declares a new integer called piranhaWidth
        int piranhaHeight = 128;     // declares a new integer called piranhaHeight

        Random rand;   // Declares a random instance of rand




        /// CONSTRUCTOR - for the Simulation class - run once only when an object of the Simulation class is INSTANTIATED (created)
        /// Use constructors to set up the state of a class
        public Simulation(IKernel pKernel)
        {
            kernel = pKernel;                   // Stores the game engine kernel which is passed to the constructor when this class is created
            screen = kernel.Screen;             // Sets the screen variable in Simulation so the screen dimensions are accessible

            // Class setup
            rand = new Random();     // sets rand to be a new random class 



        }

        /// METHOD: LoadContent - called once at start of program
        /// Create all token objects and 'insert' them into the FishORama engine
        public void LoadContent(IGetAsset pAssetManager)
        {
            // Code to create fish tokens and assign to their variables goes here
            int initXpos;     // a new integer is declared called initXpos for the purpose of randomising the starting xPosition of the fish
            int initYpos;     // a new integer is declared called initYpos for the purpose of randomising the starting yPosition of the fish

            initXpos = rand.Next(-399 + (orangeFishWidth / 2), 400 - (orangeFishWidth / 2));   // OrangeFish's initXPos is assigned a random value between -335 and 336
            initYpos = rand.Next(-300 + (orangeFishHeight / 2), 301 - (orangeFishHeight / 2)); // OrangeFish's initYPos is assigned a random value between -268 and 268

            orangeFish1 = new OrangeFish("OrangeFish", initXpos, initYpos, screen, tokenManager, rand);  // a new orangeFish is created called OrangeFish1 with a xPosition and a yPosition
            kernel.InsertToken(orangeFish1);             // this orangeFish is then inserted into the kernel

            initXpos = rand.Next(-399 + (blueFishWidth / 2), 400 - (blueFishWidth / 2));       // BlueFish's initXpos is assigned a random value between -335 and 336 
            initYpos = rand.Next(-300 + (blueFishHeight / 2), 301 - (blueFishHeight / 2));     // BlueFish's initYpos is assigned a random value between -268 and 268 

            blueFish1 = new BlueFish("BlueFish", initXpos, initYpos, screen, tokenManager, rand);   // a new BlueFish is created called blueFish1 with a xPosition and a yPosition 
            kernel.InsertToken(blueFish1);             // this blueFish is then inserted into the kernel   

            for (int i = 0; i < piranhaArray.Length; i++)
            {
                initXpos = rand.Next(-399 + (piranhaWidth / 2), 400 - (piranhaWidth / 2));      // Urchin's initXpos is assigned a random value between -309 and 309
                initYpos = rand.Next(-300 + (piranhaHeight / 2), -151 - (piranhaHeight / 2));   // Urchin's initYpos is assigned a random value between -244 and -207
                Piranha1 tempPiranha = new Piranha1("Piranha1", initXpos, initYpos, screen, tokenManager, rand);  // a temporary Urchin is created with a xPosition and a yPosition
                piranhaArray[i] = tempPiranha;       // urchinArray[i] is set to tempUrchin
                kernel.InsertToken(tempPiranha);
            }

            for (int i = 0; i < urchinArray.Length; i++)     // while i = 0 and i is less than urchinArray.Length
            {
                initXpos = rand.Next(-399 + (urchinWidth / 2), 400 - (urchinWidth / 2));      // Urchin's initXpos is assigned a random value between -309 and 309
                initYpos = rand.Next(-300 + (urchinHeight / 2), -151 - (urchinHeight / 2));   // Urchin's initYpos is assigned a random value between -244 and -207
                Urchin tempUrchin = new Urchin("Urchin", initXpos, initYpos, screen, tokenManager, rand);  // a temporary Urchin is created with a xPosition and a yPosition
                urchinArray[i] = tempUrchin;       // urchinArray[i] is set to tempUrchin
                kernel.InsertToken(tempUrchin);    // tempUrchin is inserted into the kernel
            }

            for (int i = 0; i < seahorseArray.Length; i++)   // while i = 0 and i is less than seahorseArray.Length
            {
                initXpos = rand.Next(-399 + (seahorseWidth / 2), 400 - (seahorseWidth / 2));     // Seahorse's initXpos is assigned a random value between -362 and 362
                initYpos = rand.Next(-300 + (seahorseHeight / 2), 301 - (seahorseHeight / 2));   // Seahorse's initYpos is assigned a random value between -244 and 244
                Seahorse tempSeahorse = new Seahorse("Seahorse", initXpos, initYpos, screen, tokenManager, rand);  // a temporary Seahorse is created with a xPosition and a yPosition
                seahorseArray[i] = tempSeahorse;      // seahorseArray[i] is set to tempSeahorse
                kernel.InsertToken(tempSeahorse);     // tempSeahorse is inserted into the kernel
                 
            }
            initXpos = rand.Next(-399 + (piranhaWidth / 2), 400 - (piranhaWidth / 2));     // Piranha's initXpos is assigned a random value between -333 and 333
            initYpos = rand.Next(-100 + (piranhaHeight / 2), 301 - (piranhaHeight / 2));   // Piranha's initYpos is assigned a random value between -36 and 236
            piranhaOne = new Piranha1("Piranha1", initXpos, initYpos, screen, tokenManager, rand);  // a new Piranha is created called Piranha1 with a xPosition and a yPosition
            kernel.InsertToken(piranhaOne);          // piranhaOne is inserted into the kernel
            


        }

        /// METHOD: Update - called 60 times a second by the FishORama engine when the program is running
        /// Add all tokens so Update is called on them regularly
        public void Update(GameTime gameTime)
        {
            // Each fish object (sitting in a variable) must have Update() called on it here

            orangeFish1.Update();    // orangeFish 1 is updated so that its movement can be changed 
            blueFish1.Update();      // blueFish1 is updated so that its movement can be changed
            foreach (Urchin fish in urchinArray)   // for every urchin in the urchinArray
            {
              fish.Update();        // Urchin's are updated so that their movement can be changed
            }
            
            foreach (Seahorse fish in seahorseArray)     // for every seahorse in the seahorseArray
            {
                fish.Update();      // Seahorse's are updated so that their movement can be changed
            }
            piranhaOne.Update();    // piranhaOne is updated so that its movement can be changed

            foreach (Piranha1 fish in piranhaArray)
            {
                fish.Update();
            }
            Console.WriteLine(seahorseArray);
        }
    }
}
