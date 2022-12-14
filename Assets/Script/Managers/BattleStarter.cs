namespace Script.Managers
{
    public class BattleStarter : Spawner
    {
        void Start()
        {
            SpawnOnBattleStart();
            controller.Initialize(this);
            ui.Initialize(turn,pool,controller,shop.UIShop);
            turn.Initialize(field,ui,pool,controller, this);
            shop.Initialize(this);
        }

        // Update is called once per frame
        void Update()
        {
            controller.WaitForInput();
            ui.ShowInfoOnUpdate(controller._selectable);
        }
    }
}
