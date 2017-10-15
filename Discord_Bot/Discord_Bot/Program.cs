using Newtonsoft.Json.Linq;
using SharpCord;
using SharpCord.Objects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Oscuro
{
    class Program
    {
        public static bool isbot = true;
        public static string pf = BotSettings.botPrefix;

        static void Main(string[] args)
        {
            MakeIni.Create();


            Console.WriteLine("Defining variables");
            
            DiscordClient client = new DiscordClient(BotSettings.botToken, isbot);
            client.ClientPrivateInformation.Email = "ghost7654@mail.ru";
            client.ClientPrivateInformation.Password = "ermak1150gelsus86";


            Console.WriteLine("Defining Events");

            client.Connected += (sender, e) =>
            {
                Console.WriteLine("Connected! User: " + e.User.Username);
                
                //client.UpdateCurrentGame(BotSettings.botStatus, true, "https://github.com/NaamloosDT/DiscordSharp_Starter");

                if (client.Me.Username != BotSettings.botName)
                {
                    DiscordUserInformation info = new DiscordUserInformation();
                    info.Username = BotSettings.botName;
                    client.ChangeClientInformation(info);
                }

                if (File.Exists("avatar.jpg"))
                    client.ChangeClientAvatarFromFile("avatar.jpg");
            };


            client.PrivateMessageReceived += (sender, e) =>
            {
                if (e.Message == pf + "help")
                {
                    e.Author.SendMessage("This is a private message!");
                }
                if (e.Message == pf + "update avatar")
                {

                }
                if (e.Message.StartsWith(pf +"join"))
                {
                    if (!isbot)
                    {
                        string inviteID = e.Message.Substring(e.Message.LastIndexOf('/') + 1);

                        client.AcceptInvite(inviteID);
                        e.Author.SendMessage("Joined your discord server!");
                        Console.WriteLine("Got join request from " + inviteID);
                    }
                    else
                    {
                        e.Author.SendMessage("Please use this url instead!" +
                            "https://discordapp.com/oauth2/authorize?client_id=[CLIENT_ID]&scope=bot&permissions=0");
                    }
                }
            };


            client.MessageReceived += (sender, e) =>
            {
                if (e.MessageText == pf + "admin")
                {
                    bool isadmin = false;
                    List<DiscordRole> roles = e.Author.Roles;

                    foreach (DiscordRole role in roles)
                    {
                        if (role.Name.Contains("Administrator") || e.Author.HasPermission(DiscordSpecialPermissions.Administrator))
                        {
                            isadmin = true;
                            break;
                        }
                    }
                    if (isadmin)
                    {
                        e.Channel.SendMessage("Yes, you are! :D");
                    }
                    else
                    {
                        e.Channel.SendMessage("No, you aren't :c");
                    }
                }
                if (e.MessageText == pf + "help")
                {
                    e.Channel.SendMessage("This is a public message!");
                }
                if (e.MessageText == pf + "cat")
                {
                    //e.Channel.SendMessage("Meow :cat: " + randomcat());
                }
            };

            
            client.ChannelCreated += (sender, e) =>
            {
                if(e.ChannelCreated.Type == ChannelType.Text)
                {
                    e.ChannelCreated.SendMessage("Nice! a new channel has been created!");
                }
            };

            client.UserAddedToServer += (sender, e) =>
            {
                e.AddedMember.SendMessage("Welcome " + e.Guild.Name + ", please read the rules.\nEnjoy you're time here!");
            };


            /*client.MessageDeleted += (sender, e) =>
            {
                e.Channel.SendMessage("Removing messages has been disabled on this server!");
                e.Channel.SendMessage("<@" + e.DeletedMessage.Author.ID + "> sent: " +e.DeletedMessage.Content.ToString());
            };*/

            try
            { 
                Console.WriteLine("Sending login request");

                client.SendLoginRequest();

                Console.WriteLine("Connecting client in separate thread");

                client.Connect();

                Console.WriteLine("Client connected!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong!\n" + e.Message + "\nPress any key to close this window.");
            }

            Console.ReadKey(); // If the user presses a key, the bot will shut down.
            Environment.Exit(0); // Make sure all threads are closed.
        }

        public static string randomcat()
        {
            WebClient c = new WebClient();
            var data = c.DownloadString("http://random.cat/meow");

            JObject o = JObject.Parse(data);
            string response = o["file"].ToString();
            return response;
        }
    }
}
