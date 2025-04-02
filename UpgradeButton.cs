using UnityEngine;
using UnityEngine.Video;

public class UpgradeButton : MonoBehaviour
{
    [System.Serializable]
    public struct VideoData
    {
        public int purchaseCount;
        public UnityEngine.Video.VideoPlayer videoPlayer;
    }

    public VideoData[] videos;
    public Clicker clicker;
    public int upgradeCost = 10;
    public int clickValueIncrease = 1;
    private int upgradeCount = 0;
    private bool[] videosPlayed;

    void Start()
    {
        videosPlayed = new bool[videos.Length];
        foreach (VideoData videoData in videos)
        {
            if (videoData.videoPlayer != null && videoData.videoPlayer.gameObject != null)
            {
                videoData.videoPlayer.gameObject.SetActive(false);
            }
        }
        foreach (VideoData videoData in videos)
        {
            if (videoData.videoPlayer != null)
            {
                videoData.videoPlayer.loopPointReached += OnVideoFinished;
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (VideoData videoData in videos)
            {
                if (videoData.videoPlayer != null && videoData.videoPlayer.isPlaying)
                {
                    videoData.videoPlayer.Stop();
                    videoData.videoPlayer.gameObject.SetActive(false);
                    clicker.isVideoPlaying = false;
                }
            }
        }
    }

    void OnMouseDown()
    {
        if (clicker != null)
        {
            if (clicker.currency >= upgradeCost)
            {
                clicker.currency -= upgradeCost;
                clicker.clickValue += clickValueIncrease;
                clicker.UpdateCurrencyText();
                upgradeCost *= 2;
                upgradeCount++;

                if (upgradeCount == 4)
                {
                    clicker.ChangeCoinModel();
                }

                foreach (VideoData videoData in videos)
                {
                    int index = System.Array.IndexOf(videos, videoData);
                    if (upgradeCount >= videoData.purchaseCount && videoData.videoPlayer != null &&
                        videoData.videoPlayer.gameObject != null && !videosPlayed[index])
                    {
                        videoData.videoPlayer.gameObject.SetActive(true);
                        videoData.videoPlayer.Play();
                        videosPlayed[index] = true;
                        clicker.isVideoPlaying = true;
                    }
                }
            }
            else
            {
                Debug.Log("Not enough currency for upgrade!");
            }
        }
        else
        {
            Debug.Log("Error: Clicker not assigned to UpgradeButton!");
        }
    }

    void OnVideoFinished(UnityEngine.Video.VideoPlayer vp)
    {
        if (vp != null && vp.gameObject != null)
        {
            vp.gameObject.SetActive(false);
            clicker.isVideoPlaying = false;
        }
    }
}