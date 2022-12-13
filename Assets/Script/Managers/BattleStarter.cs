namespace Script.Managers
{
    public class BattleStarter : Spawner
    {
        void Start()
        {
            SpawnOnBattleStart();
            controller.Initialize(turn,pool,ui);
            ui.Initialize(turn,pool,controller);
            turn.Initialize(field,ui,pool,controller, this);
            shop.Initialize(pool);
        }

        // Update is called once per frame
        void Update()
        {
            controller.WaitForInput();
            ui.ShowInfoOnUpdate(controller._selectable);
        }
    }
}
