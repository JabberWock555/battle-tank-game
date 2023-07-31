using UnityEngine;

public class TankController
{
    private TankModel tankModel;
    private TankView tankView;

    public TankController(TankView _tankView, TankModel _tankModel)
    {
        tankView = _tankView;
        tankModel = _tankModel;

        tankView.SetTankController(this);
        tankModel.SetTankController(this);

        GameObject.Instantiate<TankView>(tankView);
    }
    
}
