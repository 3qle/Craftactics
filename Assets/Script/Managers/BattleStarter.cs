namespace Script.Managers
{
    public class BattleStarter : Spawner
    {
        void Start()
        {
            controller.InjectDependency();
            ui.InjectDependency(turn);
            turn.InjectDependency(field,ui,pool);
            SpawnOnBattleStart();
        }

        // Update is called once per frame
        void Update()
        {
            controller.Click();
        }
    }
}
