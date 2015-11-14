using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace CheeseBot
{
    class Program
    {

        private static IrcClient irc = new IrcClient("irc.twitch.tv", 6667, "USERNAME", "PASSWORD");

        static void Main(string[] args)
        {
            Thread connectReadChat = new Thread(new ThreadStart(ConnectReadChat));
            Thread rollChannelAdverts = new Thread(new ThreadStart(RollChannelAdverts));

            connectReadChat.Start();
            rollChannelAdverts.Start();

            Console.WriteLine("To stop program and kill task, type 'quitprog'");
            while (true)
            {
                string keyPressed = Console.ReadLine();
                if (keyPressed.ToLower() == "quitprog")
                {
                    Console.WriteLine("Terminating all threads...");
                    connectReadChat.Abort();
                    rollChannelAdverts.Abort();
                    break;
                }
                irc.sendChatMessage(keyPressed);
                
            }             
    

        }

        private static void ConnectReadChat()
        {

            irc.joinRoom("cheesekake");
            Console.WriteLine("Connected and entered room...");
            irc.sendChatMessage("I am connected and ready to go...");
            while (true)
            {
                string message = irc.readMessage();
                Console.WriteLine(message);
                if (message.Contains("!hello"))
                {
                    irc.sendChatMessage("Yo yo!");
                }
                if (message.Contains("!website"))
                {
                    irc.sendChatMessage("You can visit CheeseKake online at http://www.fragfestgaming.com or his YouTube channel at https://www.youtube.com/channel/UCco9Nl7lAtyoB9wkyAKo2gQ");
                }
                if (message.Contains("!donate"))
                {
                    irc.sendChatMessage("You may donate by visiting http://www.fragfestgaming.com and clicking on the donate link at the top of the page.");
                }
            }

        }

        private static void RollChannelAdverts()
        {

            while (true)
            {
                int repNumber = 1;
                Thread.Sleep(240000);
                irc.sendChatMessage("Testing #: " + repNumber);
                Console.WriteLine("This is a test number" + repNumber);
                repNumber++;

            }
            
        }
    }
}
