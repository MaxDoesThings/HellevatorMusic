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
        [OptionStrings(["1", "2", "3", "4", "5", "6", "7"])]
        [DefaultValue("1")]
        public string MusicSelect;

        [BackgroundColor(170, 110, 230, 1)]
        [Range(60, 900)]
        [Increment(60)]
        [Slider]
        [DefaultValue(300)]
        public int timeBeforePlay;



        [Header("Extras")]

        [BackgroundColor(170, 110, 230, 1)]
        [DefaultValue(true)]
        public bool ElevatorChime;

        [BackgroundColor(170, 110, 230, 1)]
        [DefaultValue(true)]
        public bool RareElevatorIntercom;
    }
}