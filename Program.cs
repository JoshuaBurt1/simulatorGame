using System;
using System.Linq;
namespace GameExample;
class Program
{
    //part of rubric (10 marks)
    static void Main(string[] args)
    {
        bool keepPlaying = true;

        //call game related methods from here ie. Game.cs
        Game myGame = new Game();
        myGame.StartGame();
        while(true) {
            myGame.MainMenu();
            keepPlaying = false;
        } 
        myGame.AwaitNGame();
    }
}