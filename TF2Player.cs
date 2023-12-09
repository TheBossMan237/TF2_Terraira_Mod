using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using TF2.Utills;
using Terraria.Graphics.Shaders;
namespace TF2 {

    public class PlayerStats : ModPlayer {
        public int critChancePercent = 2;
        
    }
    internal class TF2Player : ModPlayer
    {
        public TagCompound AmmoData = new TagCompound();

        public string TF2Class = "";
        public string TF2Disguise = "";

        public bool IsDev = true;
        public bool isRed = false;
        public string heldItem;
        public string AmmoText= "";
        public override void OnEnterWorld(){
            TF2Class = Helper.GiveClassBags(Player);
        }
        public override void PreUpdate(){
            if (Player.team == 0) {
                
            }
            if (Player.HeldItem.ModItem != null){
                Player.HeldItem.ModItem.LoadData(AmmoData);
                if (AmmoData.ContainsKey("AmmoInGun")) {
                    Player.HeldItem.ModItem.LoadData(AmmoData);
                    AmmoText = (AmmoData.GetInt("AmmoInGun") == -1 ? "-" : AmmoData.GetInt("AmmoInGun").ToString()) + "/" + (AmmoData.GetInt("AmmoHeld") == -1 ? "-" : AmmoData.GetInt("AmmoHeld").ToString());
                }
            } else {
                AmmoText = "";
            }
            if (Player.HasBuff(Terraria.ID.BuffID.AmmoBox)) {
                Helper.Reload(Player);
                Player.ClearBuff(Terraria.ID.BuffID.AmmoBox);
            }
            
            base.PreUpdate();
        }
    }
}
