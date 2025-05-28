using System.ComponentModel;
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
        [DrawTicks]
        [OptionStrings(["1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18"])]
        [DefaultValue("1")]
        public string MusicSelect;

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

        [BackgroundColor(170, 110, 230, 1)]
        [DefaultValue(true)]
        public bool Jumpscare;


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