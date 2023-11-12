using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using TF2.Utills;
using TF2.Assets;

namespace TF2.ClassItems
{
    public class HeavyIdentifier : ModItem
    {
        public override string Texture => Mod.Name + "/Assets/Textures/Heavy/" + Name;
        public override void SetDefaults(){
            Item.accessory = true;
        }
    }
    internal class HeavyClassBag : ModItem
    {
        public override string Texture => $"{nameof(TF2)}/Assets/Textures/Heavy/HeavyClassBag";
        public override void SetDefaults(){
            Item.useStyle = 4;
            Item.consumable = true;
            Item.width = 32;
            Item.useTime = 1;
            Item.useAnimation = 1;
            Item.height = 32;
        }
        public override bool? UseItem(Player player){
            Helper.Loadout<Minigun, Shotgun, Fists, HeavyIdentifier>(player);
            return base.UseItem(player);
        }
    }
    internal class Minigun : TF2Weapon
    {
        public override string Texture => Mod.Name + "/Assets/Textures/Heavy/" + Name;
        public override void SetDefaults(){

            Item.shoot = ProjectileID.Bullet;
            Item.useStyle = ItemUseStyleID.Shoot;
            WeaponData(200, -1, 0.125f, -1, Sounds.minigun_shoot);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if(!CanShoot(player)) return false;

            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
    }
    internal class Fists : TF2Weapon
    {
        public override string Texture => Mod.Name + "/Assets/Textures/Heavy/" + Name;
        public override void SetDefaults(){
            MeleeWeapon(.8f);
        }
    }
}
