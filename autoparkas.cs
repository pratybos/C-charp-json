using System;
using System.Collections.Generic;
using System.Text;

namespace gelmius_5
{
    class autoparkas
    {
        public List<automobilis> list { get; set; }

        public List<string> getStringList()
        {
            List<string> modeliai = new List<string>();
            foreach(automobilis auto in list)
            {
                modeliai.Add(auto.name);
            }
            return modeliai;
        }
    }
}
