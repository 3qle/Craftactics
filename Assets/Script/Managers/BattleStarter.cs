using System;

namespace Script.Managers
{
    public class BattleStarter : Spawner
    {
        private void Awake()
        {
            SpawnOnBattleStart();
           ui.InitOnAwake();
        }

        void Start()
        {
            controller = new Controller(this);
            
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
