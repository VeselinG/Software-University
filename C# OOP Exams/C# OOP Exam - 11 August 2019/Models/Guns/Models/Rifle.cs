using System;
using System.Collections.Generic;
using System.Text;

namespace ViceCity.Models.Guns.Models
{
    public class Rifle : Gun
    {
        private const int InitialBulletsPerBarrel = 50;
        private const int InitialTotalBullets = 500;
        private const int ShootsBullet = 5;
        public Rifle(string name)
            : base(name, InitialBulletsPerBarrel, InitialTotalBullets)
        {

        }

        public override int Fire()
        {
            if (this.BulletsPerBarrel-ShootsBullet<=0 && this.TotalBullets>0)
            {
                this.TotalBullets -= InitialBulletsPerBarrel;
                this.BulletsPerBarrel = InitialBulletsPerBarrel;
                this.BulletsPerBarrel -= ShootsBullet;

                return ShootsBullet;
            }

            if (this.CanFire==true)
            {
                this.BulletsPerBarrel -= ShootsBullet;
                return ShootsBullet;
            }

            return 0;
        }
    }
}
