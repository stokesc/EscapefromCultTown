using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class ChannelBar : MonoBehaviour
{
    public bool debugMode = false;
    public Image channelBarBG;
    public Image ChannelBarFill;
    protected float currentProgress = 0;

    protected void Start()
    {
        ChannelBarFill.type = Image.Type.Filled;
        ChannelBarFill.fillMethod = Image.FillMethod.Horizontal;
        ChannelBarFill.fillOrigin = (int)Image.OriginHorizontal.Left;
    }

    // Update is called once per frame
    protected void Update()
    {
        ChannelBarFill.fillAmount = currentProgress;
    }

    public void setCurrentProgress(float newProgress)
    {
        currentProgress = newProgress;
    }

    public void toggleVisible(bool toggle)
    {
        channelBarBG.enabled = toggle;
        ChannelBarFill.enabled = toggle;
    }
}
