using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using TF2.Proj;
using TF2.Utills;

namespace TF2.ClassItems
{
    internal class Fireaxe : TF2Weapon
    {
        public override string Texture => Mod.Name + "/Assets/Textures/Pyro/" + Name;
        public override void SetDefaults(){
            Item.CloneDefaults(ItemID.CopperBroadsword);
            Item.damage = 65;
            Item.width = 60;
            Item.height = 26;
            

            Item.useStyle = ItemUseStyleID.Swing;
            Item.autoReuse = true;
            WeaponData(-1, -1, .8f, -1, true);

        }
    }
    internal class Flamethrower : TF2Weapon{
        public bool hasToReload = false;
        public static int AmmoCap = 200;
        public int Ammo = 200;
        public int TimePassed = 0;
        public bool isShooting = false;
        public override string Texture => Mod.Name + "/Assets/Textures/Pyro/" + Name;
        public override void SetDefaults(){
            Item.damage = 14;
            Item.shoot = ModContent.ProjectileType<FlamethrowerProj>();
            WeaponData(200, -1, .075f, -1);

        }
        public override void HoldItem(Player player){
            if (!isShooting)
            {
                TimePassed = 0;
            }

            isShooting = false;
            base.HoldItem(player);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback){
            if (!CanShoot()) return false;

            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }

    }
    internal class PyroClassBag : ModItem{
        public override string Texture => Mod.Name + "/Assets/Textures/Pyro/" + Name;
        public override void SetDefaults(){
            Item.useStyle = 4;
            Item.consumable = true;
            Item.width = 32;
            Item.useTime = 1;
            Item.useAnimation = 1;
            Item.height = 32;


        }
        public override bool? UseItem(Player player)
        {
            TF2Player p = player.GetModPlayer<TF2Player>();
            p.ClearHotbar();
            p.GiveItem<Flamethrower>(0);
            p.GiveItem<Shotgun>(1);
            p.GiveItem<Fireaxe>(2);
            p.GiveEquipment<PyroIdentifier>();
            return base.UseItem(player);
        }
    }
    internal class PyroIdentifier : ModItem
    {
        public override string Texture => Mod.Name + "/Assets/Textures/Pyro/" + Name;
        public override void SetDefaults()
        {
            Item.accessory = true;
        }
    }
}
