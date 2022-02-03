using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitizenFX.Core;
using FivePD.API;
using FivePD.API.Utils;
using CitizenFX.Core.Native;


namespace FivePD_Rogue_Military_Soldier
{
    [CalloutProperties("Rogue Soldier", "GGGDunlix", "0.0.2")]
    public class RogueSoldier : Callout
    {
        Ped suspect;

        private Vector3[] coordinates = {
            new Vector3(-2156.1f, 3240.397f, 32.81042f),
            new Vector3(-1963.363f, 3135.539f, 32.81038f),
            new Vector3(-1952.621f, 2986.537f, 32.81018f),
            new Vector3(-1809.317f, 2878.269f, 32.80949f),
            new Vector3(-1736.669f, 2898.073f, 32.8085f),
            new Vector3(-1825.416f, 2943.042f, 33.15864f),
            new Vector3(-2395.029f, 2989.533f, 32.86604f),
            new Vector3(-2449.016f, 2963.492f, 32.8145f),
            new Vector3(-2482.605f, 2934.713f, 32.81105f),
                
        };

        public RogueSoldier()
        {
            

            InitInfo(coordinates[RandomUtils.Random.Next(coordinates.Length)]);
            ShortName = "Rogue Military Soldier";
            CalloutDescription = "A Soldier at the army base has turned on their country and is killing people. Respond in Code 3 High.";
            ResponseCode = 3;
            StartDistance = 60f;
        }

        public override async Task OnAccept()
        {
            var suspects = new[]
          {
               PedHash.MilitaryBum,
               PedHash.Armymech01SMY,
               PedHash.ExArmy01,
               PedHash.Armoured01,
               PedHash.Pilot01SMM,
               PedHash.Pilot01SMY,
               PedHash.Pilot02SMM
           };

            var guns = new[]
          {
               WeaponHash.AdvancedRifle,
               WeaponHash.HeavySniperMk2,
               WeaponHash.HomingLauncher,
               WeaponHash.MarksmanRifleMk2,
               WeaponHash.Minigun,
               WeaponHash.Railgun,
               WeaponHash.SMGMk2,
               WeaponHash.SpecialCarbineMk2,
               WeaponHash.AssaultRifleMk2,
               WeaponHash.CombatMGMk2,
               WeaponHash.BullpupRifleMk2
           };

            base.InitBlip();
            suspect = await SpawnPed(suspects[RandomUtils.Random.Next(suspects.Length)], Location);
            suspect.Weapons.Give(guns[RandomUtils.Random.Next(guns.Length)], 9999, true, true);
        }

        public override void OnStart(Ped player)
        {
            base.OnStart(player);
            suspect.Accuracy = 98;
            suspect.FiringPattern = FiringPattern.FullAuto;
            suspect.ShootRate = 1000;
            suspect.Task.FightAgainst(player);
            ShowNetworkedNotification("The suspect should be considered armed and dangerous with military weaponry.", "CHAR_CALL911", "CHAR_CALL911", "Dispatch", "", 15f);
            ShowNetworkedNotification("Code 99 Backup requested.", "CHAR_CALL911", "CHAR_CALL911", "Dispatch", "", 15f);
            FivePD.API.Utilities.RequestBackup(Utilities.Backups.Code99);
        }
    }
}
