/**
 *    Copyright 2013 Eric Schayes
 *
 *  Licensed under the Apache License, Version 2.0 (the "License 
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at
 *  
 *      http://www.apache.org/licenses/LICENSE-2.0
 *      
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using System.Xml;
using System.Xml.XPath;

namespace FreeSoundLib
{
    public class FreeSound
    {
        WebClient client;
        String apiKey = "";
        String BASE_URI = "http://freesound.org/api";
        String _URI_SOUNDS = "/sounds";
        String _URI_SOUNDS_SEARCH = "/sounds/search";
        String _URI_SOUNDS_CONTENT_SEARCH = "/sounds/content_search";
        String _URI_SOUNDS_GEOTAG = "/sounds/geotag";
        String _URI_SOUND = "/sounds/<sound_id>";
        String _URI_SOUND_ANALYSIS = "/sounds/<sound_id>/analysis/<filter>";
        String _URI_SOUND_SIMILAR = "/sounds/<sound_id>/similar";
        String _URI_USERS = "/people";
        String _URI_USER = "/people/<username>";
        String _URI_USER_SOUNDS = "/people/<username>/sounds";
        String _URI_USER_PACKS = "/people/<username>/packs";
        String _URI_USER_BOOKMARKS = "/people/<username>/bookmark_categories";
        String _URI_BOOKMARK_CATEGORY_SOUNDS = "/people/<username>/bookmark_categories/<category_id>/sounds";
        String _URI_PACKS = "/packs";
        String _URI_PACK = "/packs/<pack_id>";
        String _URI_PACK_SOUNDS = "/packs/<pack_id>/sounds";


        public FreeSound(String apikey)
        {
            this.apiKey = apikey;
            client = new WebClient();
            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
        }

        private FSObject[] ParseXMltoFSObject(String PathXml)
        {
            FSObject FSO = new FSObject();
            ArrayList list = new ArrayList();
            ArrayList tag;
            XPathNodeIterator iter2;
            XPathDocument freesound = new XPathDocument(PathXml);
            XPathNavigator nav = freesound.CreateNavigator();
            XPathNodeIterator iter = nav.Select("response/sounds/resource");
            while (iter.MoveNext())
            {
                FSO.analysis_frames = iter.Current.SelectSingleNode("analysis_frames").Value;
                FSO.analysis_stats = iter.Current.SelectSingleNode("analysis_stats").Value;
                FSO.duration = iter.Current.SelectSingleNode("duration").Value;
                FSO.id = iter.Current.SelectSingleNode("id").Value;
                FSO.original_filename = iter.Current.SelectSingleNode("original_filename").Value;
                FSO.preview_hq_mp3 = iter.Current.SelectSingleNode("preview-hq-mp3").Value;
                FSO.ref_ = iter.Current.SelectSingleNode("ref").Value;
                FSO.serve = iter.Current.SelectSingleNode("serve").Value;
                FSO.similarity = iter.Current.SelectSingleNode("similarity").Value;
                FSO.spectral_l = iter.Current.SelectSingleNode("spectral_l").Value;
                FSO.spectral_m = iter.Current.SelectSingleNode("spectral_m").Value;
                FSO.type = iter.Current.SelectSingleNode("type").Value;
                FSO.url = iter.Current.SelectSingleNode("url").Value;
                FSO.username = iter.Current.SelectSingleNode("user/username").Value;
                FSO.username_ref = iter.Current.SelectSingleNode("user/ref").Value;
                FSO.username_url = iter.Current.SelectSingleNode("user/url").Value;
                FSO.waveform_l = iter.Current.SelectSingleNode("waveform_l").Value;
                FSO.waveform_m = iter.Current.SelectSingleNode("waveform_m").Value;
                iter2 = iter.Current.Select("tags/resource");
                tag = new ArrayList();
                while (iter2.MoveNext())
                {
                    tag.Add(iter2.Current.Value);
                }
                FSO.tags = (String[])tag.ToArray(typeof(String));
                list.Add(FSO);
            }
            FSObject[] ret = (FSObject[])list.ToArray(typeof(FSObject));
            return ret;
        }

        private String _makeUri(String uri, params String[] args)
        {
            Regex Myregex = new Regex(@"<[\w_]+>");
            foreach (String arg in args)
            {
                uri = Myregex.Replace(uri, arg);
            }
            String uri_final = BASE_URI + uri;
            return uri_final;
        }

        private void _makeRequest(String uri, String XMLpath, params ReqObject[] args)
        {
            uri = uri + "?&api_key=" + this.apiKey;
            foreach (ReqObject arg in args)
            {
                uri = uri + "&" + arg.type + "=" + arg.valuet;
            }
            uri = uri + "&format=xml";
            try
            {
                client.DownloadFile(uri, XMLpath);
            }
            catch (System.Net.HttpListenerException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public FSObject[] getFromRef(String reference)
        {
            this._makeRequest(reference, "fsxml.xml");
            FSObject[] ret = this.ParseXMltoFSObject("fsxml.xml");
            try
            {
                System.IO.File.Delete("fsxml.xml");
            }
            catch (System.IO.IOException e)
            {
                Console.WriteLine(e.Message);
            }
            return ret;
        }

        public FSObject[] get_sound(String SoundID)
        {
            this._makeRequest(this._makeUri(this._URI_SOUND, SoundID), "fsxml.xml");
            FSObject[] ret = this.ParseXMltoFSObject("fsxml.xml");
            try
            {
                System.IO.File.Delete("fsxml.xml");
            }
            catch (System.IO.IOException e)
            {
                Console.WriteLine(e.Message);
            }
            return ret;
        }

        public FSObject[] get_user(String username)
        {
            this._makeRequest(this._makeUri(this._URI_USER, username), "fsxml.xml");
            FSObject[] ret = this.ParseXMltoFSObject("fsxml.xml");
            try
            {
                System.IO.File.Delete("fsxml.xml");
            }
            catch (System.IO.IOException e)
            {
                Console.WriteLine(e.Message);
            }
            return ret;
        }

        public FSObject[] get_pack(String PackID)
        {
            this._makeRequest(this._makeUri(this._URI_PACK, PackID), "fsxml.xml");
            FSObject[] ret = this.ParseXMltoFSObject("fsxml.xml");
            try
            {
                System.IO.File.Delete("fsxml.xml");
            }
            catch (System.IO.IOException e)
            {
                Console.WriteLine(e.Message);
            }
            return ret;
        }

        public FSObject[] search(String query, String page, String filter, String sort, String num_results, String fields, String sounds_per_page)
        {
            ArrayList req = new ArrayList();
            ReqObject reqobj = new ReqObject();
            reqobj.type = "q";
            reqobj.valuet = query;
            req.Add(reqobj);
            if (page != "" && page!=null)
            {
                ReqObject reqobj2 = new ReqObject();
                reqobj2.type = "p";
                reqobj2.valuet = page;
                req.Add(reqobj2);
            }
            if (filter != "" && filter != null)
            {
                ReqObject reqobj3 = new ReqObject();
                reqobj3.type = "f";
                reqobj3.valuet = filter;
                req.Add(reqobj3);
            }
            if (sort != "" && sort != null)
            {
                ReqObject reqobj4 = new ReqObject();
                reqobj4.type = "s";
                reqobj4.valuet = sort;
                req.Add(reqobj4);
            }
            if (num_results != "" && num_results != null)
            {
                ReqObject reqobj5 = new ReqObject();
                reqobj5.type = "num_results";
                reqobj5.valuet = num_results;
                req.Add(reqobj5);
            }
            if (fields != "" && fields != null)
            {
                ReqObject reqobj6 = new ReqObject();
                reqobj6.type = "fields";
                reqobj6.valuet = fields;
                req.Add(reqobj6);
            }
            if (sounds_per_page != "" && sounds_per_page != null)
            {
                ReqObject reqobj7 = new ReqObject();
                reqobj7.type = "sounds_per_page";
                reqobj7.valuet = sounds_per_page;
                req.Add(reqobj7);
            }
            ReqObject[] reqArray = (ReqObject[])req.ToArray(typeof(ReqObject));
            this._makeRequest(this._makeUri(this._URI_SOUNDS_SEARCH), "fsxml.xml", reqArray);
            FSObject[] ret = this.ParseXMltoFSObject("fsxml.xml");
            try
            {
                System.IO.File.Delete("fsxml.xml");
            }
            catch (System.IO.IOException e)
            {
                Console.WriteLine(e.Message);
            }
            return ret;
        }

        public FSObject[] quickSearch(String query)
        {
            return this.search(query, null, null, null, null, null, null);
        }

        public FSObject[] searchFromUser(String user, String query)
        {
            ReqObject[] reqArray = new ReqObject[1];
            reqArray[0] = new ReqObject();
            reqArray[0].type = "q";
            reqArray[0].valuet = query;
            this._makeRequest(this._makeUri(this._URI_USER_SOUNDS, user), "fsxml.xml", reqArray);
            FSObject[] ret = this.ParseXMltoFSObject("fsxml.xml");
            try
            {
                System.IO.File.Delete("fsxml.xml");
            }
            catch (System.IO.IOException e)
            {
                Console.WriteLine(e.Message);
            }
            return ret;
        }

        public FSObject[] contentBasedSearch(String target, String filter, String max_results, String fields, String page, String sounds_per_page)
        {
            ArrayList req = new ArrayList();
            if (page != "" && page != null)
            {
                ReqObject reqobj = new ReqObject();
                reqobj.type = "p";
                reqobj.valuet = page;
                req.Add(reqobj);
            }
            if (filter != "" && filter != null)
            {
                ReqObject reqobj2 = new ReqObject();
                reqobj2.type = "f";
                reqobj2.valuet = filter;
                req.Add(reqobj2);
            }
            if (target != "" && target != null)
            {
                ReqObject reqobj3 = new ReqObject();
                reqobj3.type = "t";
                reqobj3.valuet = target;
                req.Add(reqobj3);
            }
            if (max_results != "" && max_results != null)
            {
                ReqObject reqobj4 = new ReqObject();
                reqobj4.type = "max_results";
                reqobj4.valuet = max_results;
                req.Add(reqobj4);
            }
            if (fields != "" && fields != null)
            {
                ReqObject reqobj5 = new ReqObject();
                reqobj5.type = "fields";
                reqobj5.valuet = fields;
                req.Add(reqobj5);
            }
            if (sounds_per_page != "" && sounds_per_page != null)
            {
                ReqObject reqobj6 = new ReqObject();
                reqobj6.type = "sounds_per_page";
                reqobj6.valuet = sounds_per_page;
                req.Add(reqobj6);
            }
            ReqObject[] reqArray = (ReqObject[])req.ToArray(typeof(ReqObject));
            this._makeRequest(this._makeUri(this._URI_SOUNDS_CONTENT_SEARCH), "fsxml.xml", reqArray);
            FSObject[] ret = this.ParseXMltoFSObject("fsxml.xml");
            try
            {
                System.IO.File.Delete("fsxml.xml");
            }
            catch (System.IO.IOException e)
            {
                Console.WriteLine(e.Message);
            }
            return ret;
        }

        public FSObject[] geotag(String min_lat, String max_lat, String min_lon, String max_lon, String page, String fields, String sounds_per_page)
        {
            ArrayList req = new ArrayList();
            if (page != "" && page != null)
            {
                ReqObject reqobj = new ReqObject();
                reqobj.type = "p";
                reqobj.valuet = page;
                req.Add(reqobj);
            }
            if (min_lat != "" && min_lat != null)
            {
                ReqObject reqobj2 = new ReqObject();
                reqobj2.type = "min_lat";
                reqobj2.valuet = min_lat;
                req.Add(reqobj2);
            }
            if (max_lat != "" && max_lat != null)
            {
                ReqObject reqobj3 = new ReqObject();
                reqobj3.type = "max_lat";
                reqobj3.valuet = max_lat;
                req.Add(reqobj3);
            }
            if (min_lon != "" && min_lon != null)
            {
                ReqObject reqobj4 = new ReqObject();
                reqobj4.type = "min_lon";
                reqobj4.valuet = min_lon;
                req.Add(reqobj4);
            }
            if (max_lon != "" && max_lon != null)
            {
                ReqObject reqobj5 = new ReqObject();
                reqobj5.type = "max_lon";
                reqobj5.valuet = max_lon;
                req.Add(reqobj5);
            }
            if (fields != "" && fields != null)
            {
                ReqObject reqobj6 = new ReqObject();
                reqobj6.type = "fields";
                reqobj6.valuet = fields;
                req.Add(reqobj6);
            }
            if (sounds_per_page != "" && sounds_per_page != null)
            {
                ReqObject reqobj7 = new ReqObject();
                reqobj7.type = "sounds_per_page";
                reqobj7.valuet = sounds_per_page;
                req.Add(reqobj7);
            }
            ReqObject[] reqArray = (ReqObject[])req.ToArray(typeof(ReqObject));
            this._makeRequest(this._makeUri(this._URI_SOUNDS_GEOTAG), "fsxml.xml", reqArray);
            FSObject[] ret = this.ParseXMltoFSObject("fsxml.xml");
            try
            {
                System.IO.File.Delete("fsxml.xml");
            }
            catch (System.IO.IOException e)
            {
                Console.WriteLine(e.Message);
            }
            return ret;
        }       
    }
}
