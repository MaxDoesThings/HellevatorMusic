using HellevatorMusic.Core;
using HellevatorMusic.Systems;
using Terraria;
using Terraria.ModLoader;

namespace HellevatorMusic.Content.SceneEffects
{
    public class MusicPlayer : ModSceneEffect
    {
        public override int Music
        {
            get
            {
                var p = Main.LocalPlayer.GetModPlayer<HellevatorMusicModPlayer>();
                return MusicLoader.GetMusicSlot(Mod, "Assets/Music/tune_" + p.GetCurrentMusic());
            }
        }
        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;
        public override bool IsSceneEffectActive(Player player)
        {
            var p = player.GetModPlayer<HellevatorMusicModPlayer>();
            return p.IsFalling() && !p.IsIntercomActive() && p.IsInValidZone()
                && HellevatorMusicConfig.Instance.EnableMod;
        }
    }

    public class IntercomPlayer : ModSceneEffect
    {
        public override int Music
        {
            get
            {
                var p = Main.LocalPlayer.GetModPlayer<HellevatorMusicModPlayer>();
                return MusicLoader.GetMusicSlot(Mod, "Assets/Music/intercom_" + p.selectedIntercomTrack);
            }
        }
        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;
        public override bool IsSceneEffectActive(Player player)
        {
            var p = player.GetModPlayer<HellevatorMusicModPlayer>();
            return p.IsIntercomActive() && p.IsInValidZone()
                && HellevatorMusicConfig.Instance.EnableMod;
        }
    }
}