using Rocket.API;
using Rocket.Core.Logging;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System;
using System.Collections.Generic;

namespace Teyhota.PrivateMessages
{
    public class Command_Reply : IRocketCommand
    {
        #region Main
        public AllowedCaller AllowedCaller
        {
            get { return AllowedCaller.Player; }
        }

        public string Name
        {
            get { return "reply"; }
        }

        public string Help
        {
            get { return "Reply to a player's recent message."; }
        }

        public string Syntax
        {
            get { return "<message>"; }
        }

        public List<string> Aliases
        {
            get
            {
                return new List<string>() { "r" };
            }
        }

        public List<string> Permissions
        {
            get
            {
                return new List<string>
                {
                    "PrivateMessages.reply"
                };

            }
        }
        #endregion

        #region Execute
        public void Execute(IRocketPlayer caller, string[] para)
        {
            UnturnedPlayer plyr = (UnturnedPlayer)caller;
            UnturnedPlayer Target = null;
            String Message = "";
            if (!PrivateMessages.Instance.PlayerList.ContainsKey(plyr.CSteamID.m_SteamID)) { Logger.Log("Sorry, you haven't received a message!"); return; } // No record available.. (no Reply or Message Target) Display Help
            if (para.Length < 1) { Logger.Log("Reply Para too small!"); return; } // Display Help
            if (PrivateMessages.Instance.PlayerList[plyr.CSteamID.m_SteamID].MessageTo != 0)
                Target = UnturnedPlayer.FromCSteamID(new Steamworks.CSteamID(PrivateMessages.Instance.PlayerList[plyr.CSteamID.m_SteamID].MessageTo));
            if (PrivateMessages.Instance.PlayerList[plyr.CSteamID.m_SteamID].ReplyTo != 0)
                Target = UnturnedPlayer.FromCSteamID(new Steamworks.CSteamID(PrivateMessages.Instance.PlayerList[plyr.CSteamID.m_SteamID].ReplyTo));
            if (Target == null) { Logger.Log("An Unexpected Error Has Occured!"); return; }// Some unexpected Error
            foreach (var s in para)
            {
                Message = Message + " " + s;
            }
            UnturnedChat.Say(Target, String.Format("From:{0} {1}", plyr.DisplayName, Message), UnityEngine.Color.magenta);
            UnturnedChat.Say(plyr, String.Format("To:{0} {1}", Target.DisplayName, Message), UnityEngine.Color.magenta);
            PrivateMessages.Instance.SetPlayerData(plyr, Target);
        }
        #endregion
    }
}