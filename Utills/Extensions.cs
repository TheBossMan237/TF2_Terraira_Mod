using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using TF2.Assets;

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
        public SoundStyle ShootSound = Assets.Sounds.shotgun_shoot;
        public override void HoldStyle(Player player, Rectangle heldItemFrame){
            player.itemRotation += .1f;
            base.HoldStyle(player, heldItemFrame);
        }
        public void MeleeWeapon(float AttackInterval) {
            this.AttackInterval = (int)Math.Round(AttackInterval * 60);
            Item.useTime = this.AttackInterval;
            Item.useAnimation = this.AttackInterval;
            AmmoHeld = -1;
            AmmoInGun = -1;
            MaxAmmoHeld = -1;
            MaxAmmoInGun = -1;
        }
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

        public void WeaponData(int AmmoInGun, int AmmoHeld, float AttackInterval, float ReloadTime, SoundStyle ShootSound) {
            this.ShootSound = ShootSound;
            this.AttackInterval = (int)Math.Round(AttackInterval * 60);
            this.ReloadTime = (int)Math.Round(ReloadTime * 60);
            this.AmmoHeld = AmmoHeld;
            this.AmmoInGun = AmmoInGun;
            Item.noMelee = true;
            Item.shootSpeed = 5;
            Item.useTime = 1;
            Item.autoReuse = true;
            Item.useAnimation = 1;
            Item.useStyle = 5;
            MaxAmmoHeld = AmmoHeld;
            MaxAmmoInGun = AmmoInGun;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        public bool CanShootNormal(Player p) {
            if (dt < AttackInterval) {
                dt++;
                return false;
            } else if (AmmoInGun > 0) {
                dt = 0;
                SoundEngine.PlaySound(ShootSound, p.position);
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
        /// <summary>This method is for when the weapon being fired is similer 
        /// to a minigun or flamethrower in the way that reload time is 0 and no ammo is held
        /// no ammo is held
        /// </summary>
        public bool CanShootNoneHeld() {
            if (dt < AttackInterval) {
                dt++;
                return false;
            } else if (AmmoInGun > 0) {
                dt = 0;
                AmmoInGun--;
                return true;
            }
            return false;
        }
        public bool CanShoot(Player p) {
            if (AmmoHeld == -1) {
                if (AmmoInGun == -1) {
                    return true;
                }
                return CanShootNoneHeld();
            } else {
                return CanShootNormal(p);
            }

        }

    }
    public class Reload : ModCommand {
        public override string Command => "Reload";
        public override CommandType Type => CommandType.Chat;
        public override void Action(CommandCaller caller, string input, string[] args) {

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
        public static readonly Item empty = new Item();
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
        public static void ClearAllItems(Player p) {
            for (int i = 0; i < p.inventory.Length; i++) {
                p.inventory[i] = new Item();
            }
            for (int i = 0; i < 3; i++) {
                for (int j = 0; j < p.Loadouts[i].Armor.Length; j++) {
                    p.Loadouts[i].Armor[j] = new Item();
                }
            }

        }
        public static void Loadout<Primary, Secondary, Melee, Identifier>(Player p, int Helmet = 271, int Chesplate = 269, int Leggings = 270)
        where Primary : ModItem where Secondary : ModItem where Melee : ModItem where Identifier : ModItem {
            Loadout(p, ModContent.ItemType<Identifier>(), Helmet, Chesplate, Leggings,
                ModContent.ItemType<Primary>(),
                ModContent.ItemType<Secondary>(),
                ModContent.ItemType<Melee>()
            );
        }
        public static void Loadout<Primary, Secondary, Melee, PDA1, PDA2, Identifier>(Player p, int Helmet = 271, int Chesplate = 269, int Leggings = 270)
        where Primary : ModItem where Secondary : ModItem where Melee : ModItem
        where PDA1 : ModItem where PDA2 : ModItem
        where Identifier : ModItem {
            Loadout(p, ModContent.ItemType<Identifier>(), Helmet, Chesplate, Leggings,
                ModContent.ItemType<Primary>(),
                ModContent.ItemType<Secondary>(),
                ModContent.ItemType<Melee>(),
                ModContent.ItemType<PDA1>(),
                ModContent.ItemType<PDA2>()
            );
        }

        public static void Loadout<Primary, Secondary, Melee, PDA1, Identifier>(Player p, int Helmet = 271, int Chesplate = 269, int Leggings=270)
        where Primary : ModItem where Secondary : ModItem where Melee : ModItem
        where PDA1 : ModItem where Identifier : ModItem {
            Loadout(p, ModContent.ItemType<Identifier>(), Helmet, Chesplate, Leggings, 
                ModContent.ItemType<Primary>(),
                ModContent.ItemType<Secondary>(),
                ModContent.ItemType<Melee>(),
                ModContent.ItemType<PDA1>()
                
            );
        }
        

        private static void Loadout(Player p, int Identifier, int Helemt = 271, int Chesplate = 269, int Leggings = 270, params int[] items) {
            Array.Fill(p.inventory, empty);
            for (int i = 0; i < items.Length; i++) {
                p.inventory[i] = new Item(items[i]);
            }
            p.armor[0] = new Item(Helemt);
            p.armor[1] = new Item(Chesplate);
            p.armor[2] = new Item(Leggings);
            p.armor[3] = new Item(Identifier);
            for (int i = 0; i < 3; i++) {
                p.Loadouts[i].Armor[0] = p.armor[0];
                p.Loadouts[i].Armor[1] = p.armor[1];
                p.Loadouts[i].Armor[2] = p.armor[2];
                p.Loadouts[i].Armor[3] = p.armor[3];
            }


        }
        
    }


}