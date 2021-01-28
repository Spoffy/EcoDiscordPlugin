﻿using DSharpPlus.Entities;
using Eco.Plugins.DiscordLink.Events;
using Eco.Plugins.DiscordLink.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eco.Plugins.DiscordLink.Modules
{
    public class ServerInfoDisplay : Display
    {
        protected override int TimerUpdateIntervalMS { get { return 60000; } }
        protected override string BaseTag { get { return "[Server Info]"; } }
        protected override int TimerStartDelayMS { get { return 0; } }

        public override string ToString()
        {
            return "Server Info Display";
        }

        protected override DLEventType GetTriggers()
        {
            return DLEventType.Startup | DLEventType.Timer | DLEventType.Login | DLEventType.StartElection | DLEventType.StopElection | DLEventType.Vote;
        }

        protected override List<ChannelLink> GetChannelLinks()
        {
            return DLConfig.Data.ServerInfoChannels.Cast<ChannelLink>().ToList();
        }

        protected override void GetDisplayContent(ChannelLink link, out List<Tuple<string, DiscordEmbed>> tagAndContent)
        {
            DiscordEmbed content = MessageBuilder.Discord.GetServerInfo(GetServerInfoFlagForChannel(link as ServerInfoChannel));

            tagAndContent = new List<Tuple<string, DiscordEmbed>>();
            tagAndContent.Add(new Tuple<string, DiscordEmbed>(BaseTag, content));
        }

        private static MessageBuilder.ServerInfoComponentFlag GetServerInfoFlagForChannel(ServerInfoChannel statusChannel)
        {
            MessageBuilder.ServerInfoComponentFlag statusFlag = 0;
            if (statusChannel.UseName)
                statusFlag |= MessageBuilder.ServerInfoComponentFlag.Name;
            if (statusChannel.UseDescription)
                statusFlag |= MessageBuilder.ServerInfoComponentFlag.Description;
            if (statusChannel.UseLogo)
                statusFlag |= MessageBuilder.ServerInfoComponentFlag.Logo;
            if (statusChannel.UseAddress)
                statusFlag |= MessageBuilder.ServerInfoComponentFlag.ConnectionInfo;
            if (statusChannel.UsePlayerCount)
                statusFlag |= MessageBuilder.ServerInfoComponentFlag.PlayerCount;
            if (statusChannel.UsePlayerList)
                statusFlag |= MessageBuilder.ServerInfoComponentFlag.PlayerList;
            if (statusChannel.UsePlayerListLoggedInTime)
                statusFlag |= MessageBuilder.ServerInfoComponentFlag.PlayerListLoginTime;
            if (statusChannel.UseCurrentTime)
                statusFlag |= MessageBuilder.ServerInfoComponentFlag.CurrentTime;
            if (statusChannel.UseTimeRemaining)
                statusFlag |= MessageBuilder.ServerInfoComponentFlag.TimeRemaining;
            if (statusChannel.UseMeteorHasHit)
                statusFlag |= MessageBuilder.ServerInfoComponentFlag.MeteorHasHit;
            if (statusChannel.UseElectionCount)
                statusFlag |= MessageBuilder.ServerInfoComponentFlag.ActiveElectionCount;
            if (statusChannel.UseElectionList)
                statusFlag |= MessageBuilder.ServerInfoComponentFlag.ActiveElectionList;
            if (statusChannel.UseLawCount)
                statusFlag |= MessageBuilder.ServerInfoComponentFlag.LawCount;
            if (statusChannel.UseLawList)
                statusFlag |= MessageBuilder.ServerInfoComponentFlag.LawList;

            return statusFlag;
        }
    }
}