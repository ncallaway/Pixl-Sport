using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Media;


namespace Pixl_Sport
{
    class AudioManager
    {
        Dictionary<String, SoundEffect> effectsLibrary = new Dictionary<String, SoundEffect>();
        List<SoundEffectInstance> currentSounds = new List<SoundEffectInstance>();
        List<Song> BGMList = new List<Song>();
        Song currentBGM;

        public AudioManager()
        {




        }


        public void Initialize()
        {



        }

        public void PlayEffect(String title)
        {   
            effectsLibrary[title].Play();

        }

        private void loadSound(ContentManager CM, String assetName, String callName){

            SoundEffect asset = CM.Load<SoundEffect>(assetName);
            effectsLibrary.Add(callName, asset);
            
        }

        public void PauseSounds()
        {
            foreach( SoundEffectInstance I in currentSounds) I.Pause();
        }

        public void ResumeSounds()
        {
            foreach(SoundEffectInstance I in currentSounds) I.Resume();
        }

        public void StopSounds()
        {
            foreach ( SoundEffectInstance I in currentSounds) I.Dispose();
        }

        public void Load(ContentManager CM)
        {
            loadSound(CM, "thump1", "Kick");
            loadSound(CM, "explosion6", "Bomb");
        }








    }
}
