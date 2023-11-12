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
        public void Loadout<Primary, Secondary, Melee, Identifier>() 
            where Primary : ModItem 
            where Secondary : ModItem 
            where Melee : ModItem 
            where Identifier : ModItem {
            //Clear Inventory
            for (int i = 0; i < Player.inventory.Length; i++) {
                Player.inventory[i] = new Item();
            }
            Player.armor[3] = new Item(ModContent.ItemType<Identifier>(), 1);

            Player.inventory[0] = new Item(ModContent.ItemType<Primary>(), 1);
            Player.inventory[1] = new Item(ModContent.ItemType<Secondary>(), 1);
            Player.inventory[2] = new Item(ModContent.ItemType<Melee>(), 1);   
        }
        public void GiveItem(Item i, int slot = 1) { Player.inventory[slot] = i; }
        public void GiveItem<T>(int slot = 0) where T : ModItem { Player.inventory[slot] = new Item(ModContent.ItemType<T>(), 1); }
        public void GiveEquipment(Item i, int slot = 3) { Player.armor[slot] = i; }
        public void GiveEquipment<T>(int slot = 3) where T : ModItem { Player.armor[slot] = new Item(ModContent.ItemType<T>(), 1); }
        public void DeleteItem(int slot) { Player.inventory[slot] = new Item(); }
        public void GiveArmor(int item) {
            Player.bank.AddItemToShop(new Item(item));
            Player.armor[0] = Player.bank.item[0];
        }
        public void ClearInvetory() {
            for (int i = 0; i < 50; i++) {
                Player.inventory[i] = new Item();
            }
        }
        public override void PostItemCheck()
        {
            if (Player.HeldItem.Name != heldItem) {
                heldItem = Player.HeldItem.Name;
                
            }

            base.PostItemCheck();
        }
        public void ClearHotbar() {
            for (int i = 0; i < 10; i++) {
                Player.inventory[i] = new Item();

            }
        }
        public void SpyInit() {
            GiveItem(new Item(ModContent.ItemType<Revolver>()), 0);
            GiveItem(new Item(ModContent.ItemType<DisguiseKit>()), 0);
        }

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

            GiveItem<ScoutClassBag>(1);
            GiveItem<SoldierClassBag>(2);
            GiveItem<PyroClassBag>(3);
            GiveItem<DemomanClassBag>(4);
            GiveItem<HeavyClassBag>(5);
            GiveItem<EngineerClassBag>(6);
            GiveItem<MedicClassBag>(7);
            GiveItem<SniperClassBag>(8);
            GiveItem<SpyClassBag>(9);


            base.OnEnterWorld();
        }
        public override void OnEnterWorld(){
            PlayerJoin();
        }
        public override void PostUpdate(){
            base.PostUpdate();
        }
        public override void PreUpdate(){
           
            //115

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
