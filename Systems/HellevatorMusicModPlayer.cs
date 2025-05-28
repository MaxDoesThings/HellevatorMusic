using System;
using HellevatorMusic.Core;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
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

        private int jumpscareTimer = 0;
        private const int jumpscareLength = 180;

        private int intercomChance => HellevatorMusicConfig.Instance.IntercomChance;
        private static readonly int exclusiveChance = 5;
        private static readonly int jumpscareChance = 40;

        public static readonly SoundStyle elevatorChime = new SoundStyle("HellevatorMusic/Assets/Sounds/elevator_chime");

        public string selectedIntercomTrack = "reg_1";

        private bool hasElevatorChimed = false;
        private bool hasIntercomRolled = false;
        private bool playingIntercom = false;
        private bool hasJumpscareRolled = false;
        private bool playingJumpscare = false;

        private bool isYFast()
        {
            return Player.velocity.Y > GetFallVelocityThreshold();
        }

        private bool isMovingX()
        {
            float xMoveThreshold = HellevatorMusicConfig.Instance.HorizontalLeniency;
            return Math.Abs(Player.velocity.X) >= xMoveThreshold && xMoveThreshold > 0f;
        }

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

        public static float GetFallVelocityThreshold()
        {
            return HellevatorMusicConfig.Instance.FallVelocityThreshold switch
            {
                "1" => 3f, "2" => 7f, "3" => 10f, _ => 10f
            };
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

        private void FallReset()
        {
            fallTimer = 0;
            hasIntercomRolled = false;
            hasSelectedRandom = false;
            hasJumpscareRolled = false;
        }

        public override void PostUpdate()
        {    
            if (HellevatorMusicConfig.Instance.EnableMod)
            {
                if (isYFast() && !isMovingX()) // fallTimer ticking.
                {
                    fallTimer++;

                    if (HellevatorMusicConfig.Instance.RandomSongs && !hasSelectedRandom) // Music selection logic.
                    {
                        chosenMusic = Main.rand.Next(1, 19).ToString();
                        hasSelectedRandom = true;
                    }
                    else if (!HellevatorMusicConfig.Instance.RandomSongs)
                    {
                        chosenMusic = HellevatorMusicConfig.Instance.MusicSelect;
                    }
                }
                else
                {
                    FallReset();
                }

                if (HellevatorMusicConfig.Instance.ElevatorIntercom) // Intercom checks.
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

                /*if (HellevatorMusicConfig.Instance.Jumpscare) // Jumpscare checks (hehe, boo).
                {
                    if (!hasJumpscareRolled && hasIntercomRolled && !playingIntercom)
                    {
                        hasJumpscareRolled = true;

                        if (Main.rand.NextBool(jumpscareChance))
                        {
                            NPC.SpawnOnPlayer(Player.whoAmI, NPCID.DungeonGuardian);
                            Main.NewText($"FALL FOR YOUR LIFE!", 255, 0, 0); 
                            playingJumpscare = true;
                            jumpscareTimer = 0;
                        }
                    }

                    if (playingJumpscare)
                    {
                        jumpscareTimer++;

                        if (jumpscareTimer >= jumpscareLength)
                        {
                            Main.NewText($"hehe gotcha.", 0, 255, 255);
                            playingJumpscare = false;
                            jumpscareTimer = 0;
                        }
                    }
                }*/
            }
            else // Good ol' safety reset.
            {
                FallReset();
            }

            // Debug Corner.
            if (HellevatorMusicConfig.Instance.FallTimerToggle) // fallTimer toggle.
            {
                Main.NewText("Fall Timer: " + fallTimer);
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