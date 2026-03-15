using System;
using System.Linq;
using HellevatorMusic.Core;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace HellevatorMusic.Systems
{
	public class HellevatorMusicModPlayer : ModPlayer
	{
        private MusicSelection chosenMusic = MusicSelection.Elevator;
        private bool hasSelectedRandom = false;

		private int timer = 0;
		private int musicThreshold => HellevatorMusicConfig.Instance.timeBeforePlay;

        private int intercomTimer = 0;
        private const int intercomLength = 1500;

        private int intercomChance => HellevatorMusicConfig.Instance.IntercomChance;
        private static readonly int exclusiveChance = 5;

        public static readonly SoundStyle elevatorChime = new SoundStyle("HellevatorMusic/Assets/Sounds/elevator_chime");

        public string selectedIntercomTrack = "reg_1";

        private bool hasElevatorChimed = false;
        private bool hasIntercomRolled = false;
        private bool playingIntercom = false;

        private bool IsYFastFall()
        {
            return Player.velocity.Y > GetFallVelocityThreshold();
        }

        private bool IsYFastRise()
        {
            return Player.velocity.Y < -5f;
        }

        private bool IsMovingX()
        {
            float xMoveThreshold = HellevatorMusicConfig.Instance.HorizontalLeniency;
            return Math.Abs(Player.velocity.X) >= xMoveThreshold && xMoveThreshold > 0f;
        }

        public bool IsFalling()
        {
            return timer >= musicThreshold;
        }

        public bool IsInValidZone()
        {
            return !Player.ZoneUnderworldHeight && !Player.ZoneSkyHeight && !Player.ZoneOverworldHeight;
        }

        public bool IsIntercomActive()
        {
            return playingIntercom;
        }

        public static float GetFallVelocityThreshold()
        {
            return HellevatorMusicConfig.Instance.FallVelocityThreshold switch
            {
                "1" => 3f, "2" => 7f, "3" => 10f, _ => 10f
            };
        }

        public string GetCurrentMusic()
        {
            return chosenMusic.ToString();
        }

        private void SelectRandomTrack()
        {
            var playlist = HellevatorMusicConfig.Instance.MusicPlaylist;
            var enabledTracks = playlist.Where(x => x.Value).Select(x => x.Key).ToList();

            if (enabledTracks.Count > 0)
            {
                chosenMusic = enabledTracks[Main.rand.Next(enabledTracks.Count)];
            }
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

        private void FallReset()
        {
            timer = 0;
            hasIntercomRolled = false;
            hasSelectedRandom = false;
        }

        public override void PostUpdate()
        {
            if (HellevatorMusicConfig.Instance.EnableMod)
            {
                if (IsYFastFall() && !IsMovingX()) // timer ticking.
                {
                    timer++;

                    if (HellevatorMusicConfig.Instance.RandomSongs && !hasSelectedRandom) // Music selection logic.
                    {
                        SelectRandomTrack();
                        hasSelectedRandom = true;
                    }
                    else if (!HellevatorMusicConfig.Instance.RandomSongs)
                    {
                        chosenMusic = HellevatorMusicConfig.Instance.MusicSelect;
                    }
                }
                else if (IsYFastRise() && !IsMovingX())
                {
                    timer++;

                    if (HellevatorMusicConfig.Instance.unHellevator && HellevatorMusicConfig.Instance.snakeEaterOnly)
                    {
                        chosenMusic = MusicSelection.SnakeEater;
                    }
                    else if (HellevatorMusicConfig.Instance.unHellevator && !HellevatorMusicConfig.Instance.snakeEaterOnly)
                    {
                        if (HellevatorMusicConfig.Instance.RandomSongs && !hasSelectedRandom) // stolen from above...
                        {
                            SelectRandomTrack();
                            hasSelectedRandom = true;
                        }
                        else if (!HellevatorMusicConfig.Instance.RandomSongs)
                        {
                            chosenMusic = HellevatorMusicConfig.Instance.MusicSelect;
                        }
                    }
                }
                else
                {
                    FallReset();
                }

                if (HellevatorMusicConfig.Instance.ElevatorIntercom) // Intercom checks.
                {
                    if (!hasIntercomRolled && timer >= musicThreshold + 300 && !playingIntercom)
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

                if (HellevatorMusicConfig.Instance.ElevatorChime) // Elevator Chime checks.
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
            else // Good ol' safety reset.
            {
                FallReset();
            }

            // Debug Corner.
            if (HellevatorMusicConfig.Instance.FallTimerToggle) // fallTimer toggle.
            {
                Main.NewText("Fall Timer: " + timer);
            }

            if (HellevatorMusicConfig.Instance.PlayerYToggle)
            {
                Main.NewText("Player Y Velocity: " + Player.velocity.Y);
            }

            if (HellevatorMusicConfig.Instance.PlayerXToggle)
            {
                Main.NewText("Player X Velocity: " + Player.velocity.X);
            }
        }
    }
}