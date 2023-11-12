using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using TF2.Utills;

namespace TF2.ClassItems {
    public class EngineerClassBag : ModItem
    {
        public override string Texture => Mod.Name + "/Assets/Textures/Engineer/" + Name;
        public override void SetDefaults()
        {
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
            p.GiveItem<Shotgun>(0);
            p.GiveItem<Pistol>(1);
            p.GiveItem<Wrench>(2);
            p.GiveEquipment(new Item(ItemID.EngineeringHelmet), 0);
            p.GiveEquipment(new Item(ItemID.FamiliarShirt), 1);
            p.GiveEquipment(new Item(ItemID.FamiliarPants), 2);
            p.GiveEquipment<EngineerIdentifier>();

            return base.UseItem(player);
        }

    }
    public class EngineerIdentifier : ModItem
    {
        public override string Texture => Mod.Name + "/Assets/Textures/Engineer/" + Name;

        public override void SetDefaults()
        {
            Item.accessory = true;
        }
    }
    public class Wrench : TF2Weapon
    {
        public override string Texture => Mod.Name + "/Assets/Textures/Engineer/" + Name;

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.CopperBroadsword);
            MeleeWeapon(.8f);
        }
    }
}