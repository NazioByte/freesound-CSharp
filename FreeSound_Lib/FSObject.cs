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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeSoundLib
{
    public class FSObject
    {
        String _analysis_stats;
        String _analysis_frames;
        String _preview_hq_mp3;
        String _original_filename;
        String[] _tags;
        String _url;
        String _similarity;
        String _username;
        String _username_url;
        String _username_ref;
        String _serve;
        String _spectral_m;
        String _spectral_l;
        String _type;
        String _duration;
        String _ref_;
        String _id;
        String _waveform_l;
        String _waveform_m;

        public FSObject()
        {
        }

        public String analysis_stats
        {
            get { return _analysis_stats; }
            set {_analysis_stats = value; }
        }
        public String analysis_frames
        {
            get { return _analysis_frames; }
            set { _analysis_frames = value; }
        }
        public String preview_hq_mp3
        {
            get { return _preview_hq_mp3; }
            set { _preview_hq_mp3 = value; }
        }
        public String original_filename
        {
            get { return _original_filename; }
            set { _original_filename = value; }
        }
        public String[] tags
        {
            get { return _tags; }
            set { _tags = value; }
        }
        public String url
        {
            get { return _url; }
            set { _url = value; }
        }
        public String similarity
        {
            get { return _similarity; }
            set { _similarity = value; }
        }
        public String username
        {
            get { return _username; }
            set { _username = value; }
        }
        public String username_url
        {
            get { return _username_url; }
            set { _username_url = value; }
        }
        public String username_ref
        {
            get { return _username_ref; }
            set { _username_ref = value; }
        }
        public String serve
        {
            get { return _serve; }
            set { _serve = value; }
        }
        public String spectral_m
        {
            get { return _spectral_m; }
            set { _spectral_m = value; }
        }
        public String spectral_l
        {
            get { return _spectral_l; }
            set { _spectral_l = value; }
        }
        public String type
        {
            get { return _type; }
            set { _type = value; }
        }
        public String duration
        {
            get { return _duration; }
            set { _duration = value; }
        }
        public String ref_
        {
            get { return _ref_; }
            set { _ref_ = value; }
        }
        public String id
        {
            get { return _id; }
            set { _id = value; }
        }
        public String waveform_l
        {
            get { return _waveform_l; }
            set { _waveform_l = value; }
        }
        public String waveform_m
        {
            get { return _waveform_m; }
            set { _waveform_m = value; }
        }

    }
}
