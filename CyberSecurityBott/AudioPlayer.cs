using System.Media;

namespace CyberSecurityBott.Classes
{
    public static class AudioPlayer
    {
        public static void PlayGreeting()
        {
            try
            {
                SoundPlayer player = new SoundPlayer("Assets/Cybersecurity bot.wav");
                player.Play();
            }
            catch
            {

            }
        }
    }
}