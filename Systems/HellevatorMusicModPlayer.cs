using HellevatorMusic.Core;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace HellevatorMusic.Systems
{
	public class HellevatorMusicModPlayer : ModPlayer
	{
        private string chosenMusic = "1";
        private bool hasSelectedRandom = false;

		private int fallTimer = 0;
		private int musicThreshold => HellevatorMusicConfig.Instance.timeBeforePlay;

        private int intercomTimer = 0;
        private const int intercomLength = 1500;

        private static readonly int intercomChance = 20;
        private static readonly int exclusiveChance = 5;

        public static readonly SoundStyle elevatorChime = new SoundStyle("HellevatorMusic/Assets/Sounds/elevator_chime");

        public string selectedIntercomTrack = "reg_1";

        private bool hasElevatorChimed = false;
        private bool hasIntercomRolled = false;
        private bool playingIntercom = false;

        public bool IsFalling()
        {
            return fallTimer >= musicThreshold;
        }

        public bool IsInValidZone()
        {
            return !Player.ZoneUnderworldHeight && !Player.ZoneSkyHeight && !Player.ZoneOverworldHeight;
        }

        public bool IsIntercomActive()
        {
            return playingIntercom;
        }

        public string GetCurrentMusic()
        {
            return chosenMusic;
        }

        private void StartIntercom()
        {
            playingIntercom = true;
            intercomTimer = 0;

            if (Main.hardMode && Main.rand.NextBool(exclusiveChance))
            {
                selectedIntercomTrack = "hard_1";
            }
            else if (!Main.hardMode && Main.rand.NextBool(exclusiveChance))
            {
                selectedIntercomTrack = "pre_1";
            }
            else
            {
                selectedIntercomTrack = "reg_1";
            }
        }

        private void StopIntercom()
        {
            playingIntercom = false;
            intercomTimer = 0;
        }

        public override void PostUpdate()
        {
            if (HellevatorMusicConfig.Instance.EnableMod)
            {
                if (Player.velocity.Y > 4f)
                {
                    fallTimer++;

                    if (HellevatorMusicConfig.Instance.RandomSongs && !hasSelectedRandom)
                    {
                        chosenMusic = Main.rand.Next(1, 8).ToString();
                        hasSelectedRandom = true;
                    }
                    else if (!HellevatorMusicConfig.Instance.RandomSongs)
                    {
                        chosenMusic = HellevatorMusicConfig.Instance.MusicSelect;
                    }
                }
                else
                {
                    fallTimer = 0;
                    hasIntercomRolled = false;
                    hasSelectedRandom = false;
                }

                if (HellevatorMusicConfig.Instance.RareElevatorIntercom)
                {
                    if (!hasIntercomRolled && fallTimer >= musicThreshold + 300 && !playingIntercom)
                    {
                        hasIntercomRolled = true;

                        if (Main.rand.NextBool(intercomChance))
                        {
                            StartIntercom();
                        }
                    }

                    if (playingIntercom)
                    {
                        intercomTimer++;

                        if (intercomTimer >= intercomLength || Player.ZoneUnderworldHeight)
                        {
                            StopIntercom();
                        }
                    }
                }

                if (HellevatorMusicConfig.Instance.ElevatorChime)
                {
                    if (!hasElevatorChimed && Player.ZoneUnderworldHeight && IsFalling())
                    {
                        SoundEngine.PlaySound(elevatorChime, Player.Center);
                        hasElevatorChimed = true;
                    }

                    if (!Player.ZoneUnderworldHeight)
                    {
                        hasElevatorChimed = false;
                    }
                }
            }
            else
            {
                fallTimer = 0;
                hasIntercomRolled = false;
                hasSelectedRandom = false;
            }
        }
    }
}