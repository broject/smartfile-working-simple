using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartFileTest
{
    /*boroo: json data convert to model class*/
    public class SmartObj
    {
        public long id { get; set; }
        public string name { get; set; }
        public string path { get; set; }
        public string url { get; set; }
        public int links { get; set; }
        public object remote_status { get; set; }
        public int size { get; set; }
        public object items { get; set; }
        public string time { get; set; }
        public bool isfile { get; set; }
        public bool isdir { get; set; }
        public SmartOwner owner { get; set; }
        public SmartAcl acl { get; set; }
        public string extension { get; set; }
        public string mime { get; set; }
        public List<string> tags { get; set; }
        public object attributes { get; set; }
        public bool has_preview { get; set; }
        public int version { get; set; }
    }

    public class SmartOwner
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string url { get; set; }
    }

    public class SmartAcl
    {
        public bool read { get; set; }
        public bool write { get; set; }
        public bool remove { get; set; }
        public bool list { get; set; }
    }

    public class SmartUrl
    {
        public string url { get; set; }
    }
}
