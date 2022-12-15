namespace Script.Managers
{
    public class BattleStarter : Spawner
    {
        void Start()
        {
            SpawnOnBattleStart();
            controller.Initialize(this);
            ui.Initialize(this);
            turn.Initialize( this);
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
