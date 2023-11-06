using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using TF2.ClassItems;
namespace TF2.Utills {
    public abstract class TF2Weapon : ModItem {
        public int dt = 0;
        public int dt2 = 0;
        public int AmmoInGun = 0;
        public int AmmoHeld = 0;
        public int AttackInterval = 0;
        public int ReloadTime;
        public int MaxAmmoHeld;
        public int MaxAmmoInGun;

        public override void LoadData(TagCompound tag) {
            tag.Set("AmmoInGun", AmmoInGun, true);
            tag.Set("AmmoHeld", AmmoHeld, true);
            tag.Set("MaxAmmoHeld", MaxAmmoHeld, true);
            tag.Set("MaxAmmoInGun", MaxAmmoInGun, true);
            base.LoadData(tag);
        }
        public override void SaveData(TagCompound tag) {
            if (tag.ContainsKey("AmmoInGun") && tag.ContainsKey("AmmoHeld")) {
                AmmoInGun = tag.GetInt("AmmoInGun");
                AmmoHeld = tag.GetInt("AmmoHeld");
            }
        }

        public void WeaponData(int AmmoInGun, int AmmoHeld, float AttackInterval, float ReloadTime, bool Melee = false) {
            this.AttackInterval = (int)Math.Round(AttackInterval * 60);
            this.ReloadTime = (int)Math.Round(ReloadTime * 60);
            this.AmmoHeld = AmmoHeld;
            this.AmmoInGun = AmmoInGun;
            if (!Melee) {
                Item.noMelee = true;
                Item.shootSpeed = 5;
                Item.useTime = 1;
                Item.autoReuse = true;
                Item.useAnimation = 1;
                Item.useStyle = 5;
            } else{
                Item.useTime = this.AttackInterval;
                Item.useAnimation = this.AttackInterval;
            }


            MaxAmmoHeld = AmmoHeld;
            MaxAmmoInGun = AmmoInGun;

        }
        public bool CanShootNormal() {
            if (dt < AttackInterval) {
                dt++;
                return false;
            } else if (AmmoInGun > 0) {
                dt = 0;
                AmmoInGun--;
                return true;
            } else if (dt2 < ReloadTime) {
                dt2++;
                return false;
            } else {
                int diff = Math.Clamp(AmmoHeld, 0, MaxAmmoInGun - AmmoInGun);
                AmmoInGun += diff;
                AmmoHeld -= diff;
                dt2 = 0;
                return false;
            }
        }
        /// <summary>CanShoot method for when it is a weapon like the minigun or flamethrower</summary>
        public bool CanShootNoneHeld() {
            if (dt < AttackInterval) {
                dt++;
                return false;
            } else if(AmmoInGun > 0) {
                dt = 0;
                AmmoInGun--;
                return true;
            }
            return false;
        }
        public bool CanShoot() {
            if (AmmoHeld == -1) {
                if (AmmoInGun == -1) {
                    return true;
                }
                return CanShootNoneHeld();
            } else {
                return CanShootNormal();
            }

        }

    }
    public class Reload : ModCommand {
        public override string Command => "Reload";
        public override CommandType Type => CommandType.Chat;
        public override void Action(CommandCaller caller, string input, string[] args){

            for (int i = 0; i < 10; i++) {
                Item item = caller.Player.inventory[i];
                if (item.ModItem != null) {
                    TagCompound tag = new TagCompound();
                    item.ModItem.LoadData(tag);
                    tag["AmmoInGun"] = tag["MaxAmmoInGun"];
                    tag["AmmoHeld"] = tag["MaxAmmoHeld"];
                    item.ModItem.SaveData(tag);
                }
            }
            Main.NewText("Reloaded!");
        }

    }
    public class Helper {
        public static void Reload(Player p, int slot=-1) {
            TagCompound tag = new TagCompound();
            if (slot == -1) {
                for (int i = 0; i <= 10; i++){
                    Item item = p.inventory[i];
                    if (item.ModItem != null){
                        tag = new TagCompound();
                        item.ModItem.LoadData(tag);
                        tag["AmmoInGun"] = tag["MaxAmmoInGun"];
                        tag["AmmoHeld"] = tag["MaxAmmoHeld"];
                        item.ModItem.SaveData(tag);
                    }
                }
            } else {
                if (slot <= 0 || slot >= 10 || p.inventory[slot].ModItem == null) return;
                p.inventory[slot].ModItem.LoadData(tag);
                tag["AmmoInGun"] = tag["MaxAmmoInGun"];
                tag["AmmoHeld"] = tag["MaxAmmoHeld"];
                p.inventory[slot].ModItem.SaveData(tag);
            }

        }
        public static void ClearAllItem(Player p) {
            for (int i = 0; i < p.inventory.Length; i++) {
                p.inventory[i] = new Item();
            }
            for (int i = 0; i < 3; i++) {
                for (int j = 0; j < p.Loadouts[i].Armor.Length; j++) {
                    p.Loadouts[i].Armor[j] = new Item();
                }
            }

        }
    }


}