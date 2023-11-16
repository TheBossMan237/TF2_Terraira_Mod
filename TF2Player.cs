using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using TF2.ClassItems;
using System.Reflection;
using TF2.Utills;
using Terraria.ModLoader.IO;

namespace TF2
{
    internal class TF2Player : ModPlayer
    {
        public TagCompound AmmoData = new TagCompound();

        public string TF2Class = "";
        public string TF2Disguise = "";

        public bool IsDev = true;
        public bool isRed = false;
        public NPC MouseOver; //CHANGE TO PLAYER 
        public string heldItem;
        public string AmmoText= "";

        public void PlayerJoin() {

            if (Player.armor[3].Name != ""){
                TF2Class = Player.armor[3].Name.Split("Identifier")[0];
                return;
            }
            for (int i = 0; i < 10; i++){
                if (Player.inventory[i].Name != ""){
                    return;
                }
            }
            base.OnEnterWorld();
        }
        public override void OnEnterWorld(){
            PlayerJoin();
        }
        public override void PostUpdate(){
            base.PostUpdate();
        }
        public override void PreUpdate(){

            int x = (int)Main.MouseWorld.X;
            int y = (int)Main.MouseWorld.Y;
            MouseOver = null;
            
            for (int i = 0; i < Main.npc.Length; i++) {
                if (Main.npc[i].getRect().Contains(x, y)) {
                    MouseOver = Main.npc[i];
                    base.PreUpdate();
                    return;
                }
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

            
            base.PreUpdate();
        }
    }
}
