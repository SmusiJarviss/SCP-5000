﻿using Exiled.API.Features;
using System;
using ServerEvents = Exiled.Events.Handlers.Server;

namespace SCP5000
{
    public class SCP5000 : Plugin<Config>
    {
        public override string Prefix => "SCP-5000";
        public override string Name => "SCP-5000";
        public override string Author => "Smusi Jarvis";
        public override Version Version => new Version(2, 0, 1);
        public override Version RequiredExiledVersion => new Version(7, 0, 0);

        public Handlers Handlers { get; private set; }

        public static SCP5000 Singleton;

        public override void OnEnabled()
        {
            Singleton = this;

            RegisterEvents();
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            UnregisterEvents();
            base.OnDisabled();
        }

        private void RegisterEvents()
        {
            Handlers = new Handlers();
            ServerEvents.RoundStarted += Handlers.RoundStarted;
        }

        private void UnregisterEvents()
        {
            ServerEvents.RoundStarted -= Handlers.RoundStarted;
            Handlers = null;
        }
    }
}