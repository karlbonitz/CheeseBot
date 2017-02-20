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

        private static IrcClient irc = new IrcClient("irc.twitch.tv", 6667, "cheesekakebot", "oauth:bvjru8q8ke2kyn67cuy4fo7vmlyv70");

        static void Main(string[] args)
        {
            // Initialize new threads to run core components of Twitch Bot
            Thread connectReadChat = new Thread(new ThreadStart(ConnectReadChat));
            Thread rollChannelAdverts = new Thread(new ThreadStart(RollChannelAdverts));

            // Start core threads
            connectReadChat.Start();
            // rollChannelAdverts.Start(); ENABLE LATER

            // Code below will close program and end loop once user types predetermined "Code word"
            Console.WriteLine("To stop program and kill task, type 'quitprog'");
            while (true)
            {
                string keyPressed = Console.ReadLine();
                if (keyPressed.ToLower() == "quitprog")
                {
                    // Terminate threads on program quit
                    Console.WriteLine("Terminating all threads...");
                    connectReadChat.Abort();
                    rollChannelAdverts.Abort();
                    break;
                }

                // Send console commands to Twitch chat
                irc.sendChatMessage(keyPressed);
                
            }             
    
        }

        private static void ConnectReadChat()
        {
            irc.joinRoom("vkill");
            Console.WriteLine("Connected and entered the room...");

            // Initial chat message indicating that the bot is connected and in the Twitch Channel. Printed to Twitch Chat
            irc.sendChatMessage("I am connected and ready to go...");

            // While connected
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
            int repNumber = 1;

            // Can advertise within chat every time sleep time has passed. This can be useful for advertising or rules
            while (true)
            {
                Thread.Sleep(400000);
                irc.sendChatMessage("Testing #: " + repNumber);
                Console.WriteLine("This is test number" + repNumber);
                repNumber = repNumber + 1;

            }
            
        }
    }
}
