﻿namespace AmeisenBotX.Core.Statemachine.States
{
    public class StateQuesting : BasicState
    {
        public StateQuesting(AmeisenBotStateMachine stateMachine, AmeisenBotConfig config, WowInterface wowInterface) : base(stateMachine, config, wowInterface)
        {
        }

        public override void Enter()
        {
            WowInterface.QuestEngine.Start();
        }

        public override void Execute()
        {
            WowInterface.QuestEngine.Execute();
        }

        public override void Leave()
        {
        }
    }
}