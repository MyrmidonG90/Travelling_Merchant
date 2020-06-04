using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Active
{
    static class SoundManager
    {
        static Song bgmCity;
        static Song bgmTravel;
        static bool playing;

        static public void LoadSounds(ContentManager content)
        {
            bgmCity = content.Load<Song>("Game_RPG_Music_2-AudioTrimmer.com");
            bgmTravel = content.Load<Song>("Game_RPG_Music_1-AudioTrimmer.com");
        }        
        static public void PlayMusic()
        {
            MediaPlayer.Play(bgmCity);
            MediaPlayer.IsRepeating = true;
            playing = true;
        }
        static public void PlayPauseMusic()
        {
            playing = !playing;
            if (playing)
            {
                MediaPlayer.Resume();
            }
            else
            {
                MediaPlayer.Pause();
            }
        }
    }
}
