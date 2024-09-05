using TaleWorlds.MountAndBlade;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.ObjectSystem;


namespace TestModeOne
{
    public class ArtisanMissionBehavior : MissionBehavior
    {
        public override MissionBehaviorType BehaviorType => MissionBehaviorType.Other;

        public ArtisanMissionBehavior()
        {
            InformationManager.DisplayMessage(
                new InformationMessage($"WOW"));
        }


        public override void OnMissionTick(float dt)
        {
            base.OnMissionTick(dt);

            if (Input.IsKeyPressed(InputKey.Q))
            {
                DrinkBeer();
            }
        }


        private void DrinkBeer()
        {
            if (Mission.Mode is not MissionMode.Battle or MissionMode.Stealth) return;

            if (Mission.MainAgent == null)
            {
                InformationManager.DisplayMessage(
                    new InformationMessage($"You ded lol."));
                return;
            }

            var itemRoster = MobileParty.MainParty.ItemRoster;
            var artisanBeer = MBObjectManager.Instance.GetObject<ItemObject>("artisan_beer");
            var count = itemRoster.GetItemNumber(artisanBeer);
            if (count > 0)
            {
                if (Mission.MainAgent.HealthLimit != Mission.MainAgent.Health)
                {
                    itemRoster.AddToCounts(artisanBeer, -1);
                    if (Mission.MainAgent.Health + 20 > Mission.MainAgent.HealthLimit)
                    {
                        Mission.MainAgent.Health = Mission.MainAgent.HealthLimit;
                    }
                    else
                    {
                        Mission.MainAgent.Health += 20;
                    }

                    InformationManager.DisplayMessage(
                    new InformationMessage($"+{Mission.MainAgent.Health} hp!"));
                }
                else
                {
                    InformationManager.DisplayMessage(
                    new InformationMessage($"You are full Health!"));
                }
            }
        }
    }
}