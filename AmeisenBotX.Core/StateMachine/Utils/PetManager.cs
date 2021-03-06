﻿using AmeisenBotX.Core.Common;
using System;

namespace AmeisenBotX.Core.Statemachine.Utils
{
    public class PetManager
    {
        public PetManager(WowInterface wowInterface, TimeSpan healPetCooldown, CastMendPetFunction castMendPetFunction, CastCallPetFunction castCallPetFunction, CastRevivePetFunction castRevivePetFunction)
        {
            WowInterface = wowInterface;
            HealPetCooldown = healPetCooldown;
            CastMendPet = castMendPetFunction;
            CastCallPet = castCallPetFunction;
            CastRevivePet = castRevivePetFunction;

            CallPetEvent = new TimegatedEvent(TimeSpan.FromSeconds(8));
        }

        public delegate bool CastCallPetFunction();

        public delegate bool CastMendPetFunction();

        public delegate bool CastRevivePetFunction();

        public CastCallPetFunction CastCallPet { get; set; }

        public CastMendPetFunction CastMendPet { get; set; }

        public CastRevivePetFunction CastRevivePet { get; set; }

        public TimeSpan HealPetCooldown { get; set; }

        public DateTime LastMendPetUsed { get; private set; }

        public WowInterface WowInterface { get; set; }

        private TimegatedEvent CallPetEvent { get; }

        private bool CallReviveToggle { get; set; }

        public bool Tick()
        {
            if (WowInterface.ObjectManager.Pet != null)
            {
                if (CastCallPet != null
                    && ((WowInterface.ObjectManager.Pet.Guid == 0
                        && CastCallPet.Invoke())
                    || CastRevivePet != null
                        && WowInterface.ObjectManager.Pet != null && (WowInterface.ObjectManager.Pet.Health == 0 || WowInterface.ObjectManager.Pet.IsDead)
                            && CastRevivePet()))
                {
                    return true;
                }

                if (WowInterface.ObjectManager.Pet == null || WowInterface.ObjectManager.Pet.Health == 0 || WowInterface.ObjectManager.Pet.IsDead)
                {
                    return true;
                }

                if (CastMendPet != null
                    && DateTime.Now - LastMendPetUsed > HealPetCooldown
                        && WowInterface.ObjectManager.Pet.HealthPercentage < 80
                        && CastMendPet.Invoke())
                {
                    LastMendPetUsed = DateTime.Now;
                    return true;
                }
            }
            else if (CastCallPet != null && CallPetEvent.Run() && !WowInterface.ObjectManager.Player.IsCasting)
            {
                if (CallReviveToggle)
                {
                    CastRevivePet();
                }
                else
                {
                    CastCallPet.Invoke();
                }

                CallReviveToggle = !CallReviveToggle;
            }

            return false;
        }
    }
}