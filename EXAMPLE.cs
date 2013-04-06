/*Little example to show how the library work*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FreeSoundLib;

namespace fslib_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            FreeSound Sound = new FreeSound("Your API Key");
            FSObject[] fso = Sound.quickSearch("bug"); //Here we do a quick search, the result will be always put in an array of FSObject
            WMPLib.WindowsMediaPlayer a = new WMPLib.WindowsMediaPlayer();
            a.URL = fso[0].preview_hq_mp3; //Every attribute is stored as a string except for tags which is an array of String
            a.controls.play();
            Console.ReadLine();
        }
    }
}
