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
    class ReqObject
    {
        String _type;
        String _value;

        public ReqObject()
        {
            _type = "";
            _value = "";
        }
        
        public ReqObject(String typ, String val)
        {
            _type = typ;
            _value = val;
        }

        public String type
        {
            get{ return _type; }
            set { _type = value; }
        }

        public String valuet
        {
            get { return _value; }
            set { _value = value; }
        }

    }
}
