// Copyright (c) FutureAI. All rights reserved.  
// Contains confidential and proprietary information and programs which may not be distributed without a separate license
//
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Xml;
using System.Linq;
using Pluralize.NET;
using UKS;

namespace BrainSimulatorAvalonia.Modules
{
    public class ModuleOnlineInfo : ModuleBase
    {
        // This contains access methods for getting information from
        //   chatGPT, wikidata, wiktionary, conceptnet, kidsWortsmyth dictionary,
        //   webseters elementary dictionary, dictionaryAPI, CSKG (common sense knowledge graph)
        //   Only the kids actually works

        public string Output = "";

        // Set size parameters as needed in the constructor
        // Set max to be -1 if unlimited
        public ModuleOnlineInfo()
        {
        }

        // Fill this method in with code which will execute
        // once for each cycle of the engine
        public override void Fire()
        {
            Init();  //be sure to leave this here
            // TODO: Implement Avalonia-compatible logic for word lookup and output
        }

        // TODO: Port additional methods and logic as needed from WPF version
    }
}
