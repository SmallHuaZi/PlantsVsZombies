using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class LoadingPanel : UIObject
{
    [SerializeField] private Slider m_ProgressBar;
    [SerializeField] private float m_Speed;

    protected override void OnEnable()
    {
        base.OnEnable();
        this.Progress();
    }

    public override void RefreshUI()
    {
        this.m_ProgressBar.value = 0;
    }


    public async void Progress()
    {
        while (this.m_ProgressBar.value <= 0.9f)
        {
            await Task.Yield();
            this.m_ProgressBar.value += Time.deltaTime * this.m_Speed;
        }

        /* 场景切换回调 */
        EventManager.Broadcast(EventType.UI_Loaded);
    }
}
