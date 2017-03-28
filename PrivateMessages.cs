using Rocket.Core.Plugins;
using Rocket.Unturned.Player;
using System.Collections.Generic;

namespace Teyhota.PrivateMessages
{
    #region Msg Targets
    public class MessageTargets
    {
        public ulong MessageTo;
        public ulong ReplyTo;
        public MessageTargets()
        {
            MessageTo = 0;
            ReplyTo = 0;
        }
    }
    #endregion

    public class PrivateMessages : RocketPlugin
    {
        #region Vars
        public static PrivateMessages Instance;
        public Dictionary<ulong, MessageTargets> PlayerList;
        #endregion

        #region Load
        protected override void Load()
        {
            Instance = this;
            base.Load();
            Rocket.Core.Logging.Logger.LogWarning(" ");
            Rocket.Core.Logging.Logger.LogWarning("Plugin by: LjMjollnir & Teyhota");
            Rocket.Core.Logging.Logger.LogWarning("Plugin version: 1.2");
            Rocket.Core.Logging.Logger.LogWarning("Made for Unturned version: 3.18.4.1");
            Rocket.Core.Logging.Logger.LogWarning("Made for RocketMod version: 4.9.3.0");
            Rocket.Core.Logging.Logger.LogWarning("Support: Plugins.4Unturned.tk");
            Rocket.Core.Logging.Logger.LogWarning(" ");
            Rocket.Core.Logging.Logger.LogWarning("------");
            Rocket.Core.Logging.Logger.LogWarning("PrivateMessages is up to date!");
            Rocket.Core.Logging.Logger.LogWarning("------");

            PlayerList = new Dictionary<ulong, MessageTargets>();
        }
        #endregion

        #region Set Player Data
        public void SetPlayerData(UnturnedPlayer plyr, UnturnedPlayer Target)
        {
            if (!PlayerList.ContainsKey(Target.CSteamID.m_SteamID))
                PlayerList[Target.CSteamID.m_SteamID] = new MessageTargets();
            if (!PlayerList.ContainsKey(plyr.CSteamID.m_SteamID))
                PlayerList[plyr.CSteamID.m_SteamID] = new MessageTargets();
            PlayerList[Target.CSteamID.m_SteamID].ReplyTo = plyr.CSteamID.m_SteamID;
            PlayerList[plyr.CSteamID.m_SteamID].MessageTo = Target.CSteamID.m_SteamID;
        }
        #endregion

        #region Unload
        protected override void Unload()
        {
            base.Unload();
        }
        #endregion
    }
}