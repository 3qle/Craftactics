namespace Script.Managers
{
    public class BattleStarter : Spawner
    {
        void Start()
        {
            SpawnOnBattleStart();
            controller.Initialize(turn,pool);
            ui.Initialize(turn,pool,controller);
            turn.Initialize(field,ui,pool);
          
        }

        // Update is called once per frame
        void Update()
        {
            controller.WaitForInput();
            controller.ShowSelectedCharInfo();
            ui.ShowHeroesButtons();
        }
    }
}
