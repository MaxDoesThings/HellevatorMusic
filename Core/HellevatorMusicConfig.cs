using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace HellevatorMusic.Core
{
    [BackgroundColor(120, 60, 230, 1)]
    public class HellevatorMusicConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;
        public static HellevatorMusicConfig Instance => ModContent.GetInstance<HellevatorMusicConfig>();

        [Header("Main")]

        [BackgroundColor(170, 110, 230, 1)]
        [DefaultValue(true)]
        public bool EnableMod;

        [BackgroundColor(170, 110, 230, 1)]
        [DefaultValue(true)]
        public bool RandomSongs;

        [BackgroundColor(170, 110, 230, 1)]
        [DefaultValue(MusicSelection.Elevator)]
        public MusicSelection MusicSelect;

        [BackgroundColor(170, 110, 230, 1)]
        public Dictionary<MusicSelection, bool> MusicPlaylist = new Dictionary<MusicSelection, bool>()
        {
            { MusicSelection.ACupOfLiberTea, true },
            { MusicSelection.AzureMemory, true },
            { MusicSelection.CanYouReallyCallThisAHotelIDidntReceiveAMintOnMyPillowOrAnything, true },
            { MusicSelection.DreamOn, true },
            { MusicSelection.Elevator, true },
            { MusicSelection.Elevatorstuck, true },
            { MusicSelection.FlowerShop, true },
            { MusicSelection.FreeBird, true },
            { MusicSelection.FreeFallin, true },
            { MusicSelection.FridayNight, true },
            { MusicSelection.GarotaDeImpanema, true },
            { MusicSelection.GlassStructureClear, true },
            { MusicSelection.Hotel, true },
            { MusicSelection.Infrustration, true },
            { MusicSelection.IwatodaiDorm, true },
            { MusicSelection.KiraElevator, true },
            { MusicSelection.LayerCake, true },
            { MusicSelection.LeftBankTwo, true },
            { MusicSelection.LiquidLatin, true },
            { MusicSelection.LocalForecast, true },
            { MusicSelection.MallOfTheDead, true },
            { MusicSelection.MapMuzak, true },
            { MusicSelection.MikuMikuBongo, true },
            { MusicSelection.NoMoreWhatIfs, true },
            { MusicSelection.OnceUponATime, true },
            { MusicSelection.PiaoZhe, true },
            { MusicSelection.PleaseHold, true },
            { MusicSelection.PrettyNiceDayHuh, true },
            { MusicSelection.SnakeEater, true },
            { MusicSelection.SpanishWaltz, true },
            { MusicSelection.TakeCare, true },
            { MusicSelection.TerrariaUndergroundThemeReal, true },
            { MusicSelection.TheFireIsGone, true },
            { MusicSelection.ThrashMachine, true },
            { MusicSelection.TrophyGallery, true },
            { MusicSelection.UpgradeStation, true },
            { MusicSelection.WavedashDotPpt, true },
            { MusicSelection.WelcomeToBoosterTower, true },
            { MusicSelection.WhatTheHeck, true },
            { MusicSelection.WiiShopChannel, true },
            { MusicSelection.YouAreNowLegallyABird, true }
        };


        [Header("Functionality")]

        [BackgroundColor(170, 110, 230, 1)]
        [DefaultValue(false)]
        public bool unHellevator;

        [BackgroundColor(170, 110, 230, 1)]
        [DefaultValue(true)]
        public bool snakeEaterOnly;

        [BackgroundColor(170, 110, 230, 1)]
        [Range(60, 900)]
        [Increment(60)]
        [Slider]
        [DefaultValue(300)]
        public int timeBeforePlay;

        [BackgroundColor(170, 110, 230, 1)]
        [DrawTicks]
        [OptionStrings(["1", "2", "3"])]
        [DefaultValue("3")]
        public string FallVelocityThreshold;

        [BackgroundColor(170, 110, 230, 1)]
        [Range(0f, 7f)]
        [Increment(0.1f)]
        [Slider]
        [DefaultValue(0.5f)]
        public float HorizontalLeniency;


        [Header("Extras")]

        [BackgroundColor(170, 110, 230, 1)]
        [DefaultValue(true)]
        public bool ElevatorChime;

        [BackgroundColor(170, 110, 230, 1)]
        [DefaultValue(true)]
        public bool ElevatorIntercom;

        [BackgroundColor(170, 110, 230, 1)]
        [Range(1, 20)]
        [Increment(1)]
        [Slider]
        [DefaultValue(20)]
        public int IntercomChance;


        [Header("Debug")]

        [BackgroundColor(170, 110, 230, 1)]
        [DefaultValue(false)]
        public bool FallTimerToggle;

        [BackgroundColor(170, 110, 230, 1)]
        [DefaultValue(false)]
        public bool PlayerYToggle;

        [BackgroundColor(170, 110, 230, 1)]
        [DefaultValue(false)]
        public bool PlayerXToggle;
    }
}