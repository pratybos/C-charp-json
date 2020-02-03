using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace gelmius_5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("iveskite marke, iveskite 0 jei norite išeiti");
            string marke = Console.ReadLine();
            while (marke != "0")
            {
                if (isMarkeInMarkes(marke))
                {
                    if (displayJSON(marke) !=0)
                    {
                        autoparkas parkas = getModeliai(displayJSON(marke));
                        string[] markes = getMarkes(marke);
                        List<string> list6_1 = new List<string>();
                        List<string> list6_2 = new List<string>();

                        foreach (automobilis auto in parkas.list)
                        {
                            for( int i = 0; i < markes.Length; i++)
                            {
                                if (auto.name.Contains(markes[i]))
                                {
                                    list6_1.Add(auto.name);
                                }  
                            }
                        }

                        for (int i = 0; i < markes.Length; i++)
                        {
                            int j = 0;
                            foreach (automobilis auto in parkas.list)
                            {
                                
                                if (!markes[i].Contains(auto.name))
                                {
                                    
                                    j++;
                                    if (j== parkas.list.Count)
                                    {
                                        list6_2.Add(markes[i]);
                                    }
                                }
                            }
                        }

                        List<string> list6_3 = parkas.getStringList().Except(list6_1).ToList();
                        Console.WriteLine("6.1 sarasas: ");
                        for (int i = 0; i < list6_1.Count; i++)
                        {
                            Console.WriteLine(list6_1[i]);
                        }
                        Console.WriteLine("6.2 sarasas: ");
                        for (int i = 0; i < list6_2.Count; i++)
                        {
                            Console.WriteLine(list6_2[i]);
                        }
                        Console.WriteLine("6.3 sarasas: ");
                        for (int i = 0; i < list6_3.Count; i++)
                        {
                            Console.WriteLine(list6_3[i]);
                        }
                    }
                }
                Console.WriteLine("iveskite marke, iveskite 0 jei norite išeiti");
                marke = Console.ReadLine();
            }
        }    

        public static bool isMarkeInMarkes(string marke)
        {
            System.Net.WebClient wc = new System.Net.WebClient();
            byte[] raw = wc.DownloadData("https://backend.daviva.lt/public/Markes");

            string webData = System.Text.Encoding.UTF8.GetString(raw).Substring(1, raw.Length - 2);
            webData = webData.Replace("\"", string.Empty);

            string[] markes = webData.Split(',');

            for (int i = 0; i < markes.Length; i++)
            {
                if (markes[i].Equals(marke))
                {
                    return true;
                }
            }
            return false;
        }

        public static int displayJSON(string marke)
        {
            System.Net.WebClient wc = new System.Net.WebClient();
            byte[] raw = wc.DownloadData("https://backend.daviva.lt/API/GetBrandasFromRRR");
            string webData = System.Text.Encoding.UTF8.GetString(raw);
            // Look at object
            object newObj = JsonConvert.DeserializeObject(webData);

            autoparkas squad1 = JsonConvert.DeserializeObject<autoparkas>(newObj.ToString());

            foreach (automobilis auto in squad1.list)
            {
                if(auto.name == marke)
                { return auto.id; }                
            }
            return 0;
        }

        public static string[] getMarkes(string marke)
        {
            System.Net.WebClient wc = new System.Net.WebClient();
            byte[] raw = wc.DownloadData("https://backend.daviva.lt/public/Modeliai?Name=" + marke);

            string webData = System.Text.Encoding.UTF8.GetString(raw).Substring(1, raw.Length - 2);
            webData = webData.Replace("\"", string.Empty);

            string[] markes = webData.Split(',');

            return markes;
        }

        public static autoparkas getModeliai(int id)
        {
            System.Net.WebClient wc = new System.Net.WebClient();
            byte[] raw = wc.DownloadData("https://backend.daviva.lt/API/GetCarModelsFromRRR?BrandID=" + id);
            string webData = System.Text.Encoding.UTF8.GetString(raw);
            object newObj = JsonConvert.DeserializeObject(webData);

            autoparkas squad1 = JsonConvert.DeserializeObject<autoparkas>(newObj.ToString());

            return squad1;
        }
    }  
}
