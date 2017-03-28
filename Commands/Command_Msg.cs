using Rocket.API;
using Rocket.Core.Logging;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System;
using System.Collections.Generic;

namespace Teyhota.PrivateMessages
{
    public class Command_Msg : IRocketCommand
    {
        #region Main
        public AllowedCaller AllowedCaller
        {
            get { return AllowedCaller.Player; }
        }

        public string Name
        {
            get { return "pm"; }
        }

        public string Help
        {
            get { return "Private message a player."; }
        }

        public string Syntax
        {
            get { return "<player> <message>"; }
        }

        public List<string> Aliases
        {
            get
            {
                return new List<string>() { "msg", "dm", "w", "whipser" };
            }
        }

        public List<string> Permissions
        {
            get
            {
                return new List<string>
                {
                    "PrivateMessages.msg"
                };

            }
        }
        #endregion

        #region Execute
        public void Execute(IRocketPlayer caller, string[] para)
        {
            String Message = "";
            UnturnedPlayer plyr = (UnturnedPlayer)caller;
            if (para.Length == 0) { Logger.Log("Message Para too small!"); return; } // Display Help
            UnturnedPlayer Target = UnturnedPlayer.FromName(para[0]);
            if (Target == null)
            {
                if (PrivateMessages.Instance.PlayerList.ContainsKey(plyr.CSteamID.m_SteamID))
                {
                    Target = UnturnedPlayer.FromCSteamID(new Steamworks.CSteamID(PrivateMessages.Instance.PlayerList[plyr.CSteamID.m_SteamID].MessageTo));
                    if (Target == null)
                        Target = UnturnedPlayer.FromCSteamID(new Steamworks.CSteamID(PrivateMessages.Instance.PlayerList[plyr.CSteamID.m_SteamID].ReplyTo));
                }
                if (Target == null) { Logger.Log("No Target!"); return; }// Display Help
                foreach (var s in para)
                {
                    Message = Message + s;
                }
            }
            else
            {
                int i = 0;
                foreach (var s in para)
                {
                    i++;
                    if (i == 1) continue;
                    Message = Message + " " + s;
                }
            }
            UnturnedChat.Say(Target, String.Format("From:{0} {1}", plyr.CharacterName, Message), UnityEngine.Color.magenta);
            UnturnedChat.Say(plyr, String.Format("To:{0} {1}", Target.DisplayName, Message), UnityEngine.Color.magenta);

            PrivateMessages.Instance.SetPlayerData(plyr, Target);
        }
        #endregion
    }
}