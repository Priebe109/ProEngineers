using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace GUI_eksamen_web.Models
{
    public class JokeModels
    {

        public string NewJoke { get; set; }
        public DateTime Date{ get; set;}
        public string Source { get; set; }
        public string TagString { get; set; }

        List<string> Tags
        {
            get { return Tags; }
            set { }
        }
    }
}