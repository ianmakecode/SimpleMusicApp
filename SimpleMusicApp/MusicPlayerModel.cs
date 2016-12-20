using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Media;

namespace SimpleMusicApp
{
    public class MusicPlayerModel
    {
        private string _path;

        public string Path
        {
            get
            {
                return _path;
            }
            set
            {
                if(Directory.Exists(value))
                {
                    _path = value;
                }
                else
                {
                    throw new DirectoryNotFoundException();
                }
                if(_path[_path.Length-1] != '\\') { _path += '\\'; }
            }
        }

        public SoundPlayer ModelSoundPlayer { get; set; }

        public MusicPlayerModel() : this(@"C:\") { }

        public MusicPlayerModel(string path)
        {
            this.Path = path;
            ModelSoundPlayer = new SoundPlayer()
            {
                LoadTimeout = 3000 //10 sec. time out
            };
        }

        public void LoadSong(string v)
        {
            if (v[0] == '\\') { v = v.Substring(1); }
            ModelSoundPlayer.SoundLocation = Path + v;
            ModelSoundPlayer.LoadAsync();
        }

        public string[] GetFiles()
        {
            return (new DirectoryInfo(Path)).GetFiles().Select(x => x.Name).ToArray<string>();
        }
    }
}
