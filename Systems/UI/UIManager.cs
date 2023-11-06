using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;
using System.Collections.Generic;


namespace TF2.Systems.UI {
	// This custom UI will show whenever the player is holding the ExampleCustomResourceWeapon item and will display the player's custom resource amounts that are tracked in ExampleResourcePlayer
	internal class TF2Bar : UIState {
		// For this bar we'll be using a frame texture and then a gradient inside bar, as it's one of the more simpler approaches while still looking decent.
		// Once this is all set up make sure to go and do the required stuff for most UI's in the ModSystem class.
		private UIText AmmoText;
		private UIText UberChargePercent;
		private UIElement area;

		public override void OnInitialize() {
			//The Area where the UI elemets are drawn. 
			area = new UIElement();
			area.Left.Set(0, 0f); // Place the resource bar to the left of the hearts.
			area.Top.Set(-80, 1f); // Placing it just a bit below the top of the screen.
			area.Width.Set(1920, 0f); // We will be placing the following 2 UIElements within this 182x60 area.
			area.Height.Set(60, 0f);
			//AmmoText
			AmmoText = new UIText("{0}/{1}", .6f,true); // text to show stat
			AmmoText.Width.Set(138, 0f);
			AmmoText.Height.Set(60, 0f);
			AmmoText.HAlign = 0.9f;
			AmmoText.VAlign = 0.5f;
			//UberchargeText
			UberChargePercent = new UIText("", .6f, true);
			UberChargePercent.Width.Set(132, 0f);
			UberChargePercent.Height.Set(60, 0f);
			UberChargePercent.HAlign =.05f;
			//UberchargeTextBackground
			
			
			area.Append(AmmoText);
			area.Append(UberChargePercent);
			Append(area);
		}	

		public override void Update(GameTime gameTime) {
			TF2Player modPlayer = Main.LocalPlayer.GetModPlayer<TF2Player>();
			AmmoText.SetText(modPlayer.HeldItemData.formated);
			base.Update(gameTime);
		}
	}

	// This class will only be autoloaded/registered if we're not loading on a server
	[Autoload(Side = ModSide.Client)]
	internal class UISystem : ModSystem{
		private UserInterface ExampleResourceBarUserInterface;

		internal TF2Bar tf2bar;



		public override void Load(){
			tf2bar = new();
			ExampleResourceBarUserInterface = new();
			ExampleResourceBarUserInterface.SetState(tf2bar);
		}

		public override void UpdateUI(GameTime gameTime){
			ExampleResourceBarUserInterface?.Update(gameTime);
		}

		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers){
			int resourceBarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
			if (resourceBarIndex != -1){
				layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
					"ExampleMod: Example Resource Bar",
					delegate {
						ExampleResourceBarUserInterface.Draw(Main.spriteBatch, new GameTime());
						return true;
					},
					InterfaceScaleType.UI)
				);
			
			}
			}
		}
	}
