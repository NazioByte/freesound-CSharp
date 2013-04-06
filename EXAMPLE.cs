/*Little example to show how the library work*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using FreeSoundLib;

namespace fslib_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            WebClient client;
            client = new WebClient();
            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            FreeSound Sound = new FreeSound("Your API Key");
            FSObject[] fso = Sound.quickSearch("bug"); //Here we do a quick search, the result will be always put in an array of FSObject
            WMPLib.WindowsMediaPlayer a = new WMPLib.WindowsMediaPlayer();
            a.URL = fso[0].preview_hq_mp3; //Every attribute is stored as a string except for tags which is an array of String
            a.controls.play();
            Console.ReadLine();
        }
    }
}
