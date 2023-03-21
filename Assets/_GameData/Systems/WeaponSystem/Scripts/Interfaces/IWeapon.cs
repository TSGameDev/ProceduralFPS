namespace TSGameDev.FPS.WeaponSystem
{
    public interface IWeapon
    {
        public void Fire();
        public void SetAim(bool _IsAiming);
        public void Reload();
    }
}
