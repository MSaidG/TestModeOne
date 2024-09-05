using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Workshops;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;


namespace TestModeOne
{
    public class SubModule : MBSubModuleBase
    {
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();

        }

        protected override void OnSubModuleUnloaded()
        {
            base.OnSubModuleUnloaded();

        }

        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            base.OnBeforeInitialModuleScreenSetAsRoot();

        }

        public override void OnMissionBehaviorInitialize(Mission mission)
        {
            base.OnMissionBehaviorInitialize(mission);

            mission.AddMissionBehavior(new ArtisanMissionBehavior());
        }

        protected override void InitializeGameStarter(Game game, IGameStarter starterObject)
        {
            base.InitializeGameStarter(game, starterObject);

            if (starterObject is CampaignGameStarter starter)
            {
                starter.AddBehavior(new ArtisanCampaignBehavior());
            }

        }

        protected override void OnApplicationTick(float dt)
        {
            base.OnApplicationTick(dt);
        }
    }

    public class ArtisanCampaignBehavior : CampaignBehaviorBase
    {
        public override void RegisterEvents()
        {
            CampaignEvents.WorkshopOwnerChangedEvent.AddNonSerializedListener(this, OnWorkShopOwnerChanged);
            CampaignEvents.DailyTickTownEvent.AddNonSerializedListener(this, OnDailyTickTownEvent);
        }

        private void OnDailyTickTownEvent(Town town)
        {
            foreach (var workshop in town.Workshops)
            {
                InformationManager.DisplayMessage(
                    new InformationMessage($"{town.Name} has workshop {workshop.Name}"));
                if (workshop.WorkshopType.StringId == "wool_weavery")
                {
                    workshop.ChangeGold(MathF.Round(-workshop.Expense * 0.15f));
                }
                workshop.ChangeGold(-100);
            }
        }

        private void OnWorkShopOwnerChanged(Workshop workshop, Hero hero)
        {

        }

        public override void SyncData(IDataStore dataStore)
        {
        }
    }
}