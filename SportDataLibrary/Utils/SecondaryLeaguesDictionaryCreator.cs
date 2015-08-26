﻿using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using SoccerDataLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Reflection.Emit;

namespace SoccerDataLibrary.Utils
{
    /// <summary>
    /// static class for parsing the less relevant leagues of europe
    /// </summary>
    static class SecondaryLeaguesDictionaryCreator
    {
        /// <summary>
        /// Scraping the secondary league names of the API web site
        /// </summary>
        /// <returns></returns>
        static public Dictionary<String,String> GetLeagueNames ()
        {
            Dictionary<String, String> leagueNames = new Dictionary<String, String>();
              string url = "http://api.football-data.org/docs/latest/index.html";
            var Webget = new HtmlWeb();
            var doc = Webget.Load(url);
            foreach (HtmlNode node in doc.DocumentNode.SelectNodes("/html/body/div[2]/div[8]/div[2]/table/tbody/tr"))
            {
                leagueNames.Add(node.ChildNodes[1].InnerText, node.ChildNodes[3].InnerText);
            }
            return leagueNames;
        }

        /// <summary>
        /// Scraping the secondary league IDs of the API web site
        /// </summary>
        /// <returns></returns>
        static public Dictionary<String, int> GetLeagueIds()
        {
            Dictionary<String, int> leagueIds = new Dictionary<String, int>();
            String stringJson = DataExtractor.GetInstance().GetJsonStringFromUrl(DataType.LEAGUENAMES, 0);
            JArray jsonArray = JArray.Parse(stringJson);
            foreach(JObject json in jsonArray)
            {
                leagueIds.Add((string)json["league"],Int32.Parse(((string)json["_links"]["self"]["href"]).Replace("http://api.football-data.org/alpha/soccerseasons/","")));
            }
            return leagueIds;
        }
        /// <summary>
        /// Using GetLeagueIds and GetLeagueNames in order to build a dictionary of league name and thier
        /// id in API
        /// </summary>
        /// <returns></returns>
        static public Dictionary<String, int> GetLeagueList()
        {
            Dictionary<String, int> leageList = new Dictionary<String, int>();
            Dictionary<String, String>  leagueNames = GetLeagueNames();
            Dictionary<String, int> leagueIds = GetLeagueIds();

            foreach (String key in leagueNames.Keys)
            {
                int i = 2;
                if (leagueIds.ContainsKey(key))
                {
                    String tempKey = leagueNames[key];
                    if (leageList.ContainsKey(tempKey))
                    {
                        while (leageList.ContainsKey(tempKey))
                            tempKey = leagueNames[key] + i++;
                    }
                    leageList.Add(tempKey, leagueIds[key]);
                }
            }
            return leageList;
        }

    }

}
