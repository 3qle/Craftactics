namespace Script.Managers
{
    public class BattleStarter : Spawner
    {
        void Start()
        {
            controller.Initialize(turn);
            ui.Initialize(turn,pool);
            turn.Initialize(field,ui,pool);
            SpawnOnBattleStart();
        }

        // Update is called once per frame
        void Update()
        {
            controller.WaitForInput();
            controller.ShowSelectedCharInfo();
        }
    }
}
