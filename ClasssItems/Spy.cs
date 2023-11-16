using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using TF2.Utills;
using TF2.Assets;

namespace TF2.ClassItems
{
    public class DisguiseKit : TF2Weapon{
        public override string Texture => Mod.Name + "/Assets/Textures/Spy/" + Name;
        public int counter = 0;
        public override void SetDefaults(){
            Item.CloneDefaults(ItemID.JimsDrone);
            MeleeWeapon(1);
        }
        public override bool? UseItem(Player player)
        {

            return base.UseItem(player);
        }
    }
    internal class Knife : TF2Weapon
    {
        public override string Texture => Mod.Name + "/Assets/Textures/Spy/" + Name;
        public override void SetDefaults(){
            Item.CloneDefaults(ItemID.CopperBroadsword);
            MeleeWeapon(.8f);
        }
    }
    internal class Revolver : TF2Weapon
    {
        public override string Texture => Mod.Name + "/Assets/Textures/Spy/" + Name;
        public override void SetDefaults()
        {
            WeaponData(6, 24, .5f, 1.113f, Sounds.revolver_shoot);
            Item.shoot = ProjectileID.Bullet;
            Item.shootSpeed = 5f;

        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(3, 1)
            .Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback){
            if (!CanShoot(player)) return false;
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
    }
    internal class SpyClassBag : ModItem
    {
        public override string Texture => Mod.Name + "/Assets/Textures/Spy/" + Name;
        public override void SetDefaults()
        {
            Item.useStyle = 4;
            Item.consumable = true;
            Item.width = 32;
            Item.height = 32;
        }
        public override bool? UseItem(Player player){
            Helper.Loadout<Revolver, Knife, DisguiseKit, SpyIdentifier>(
                player,
                Chesplate:ItemID.TuxedoShirt,
                Leggings:ItemID.TuxedoPants
            );
            return base.UseItem(player);
        }
        public override bool CanRightClick()
        {
            return true;
        }
    }
    internal class SpyIdentifier : ModItem
    {
        public override string Texture => Mod.Name + "/Assets/Textures/Spy/" + Name;
        public override void SetDefaults()
        {
            Item.accessory = true;

        }
    }
}
