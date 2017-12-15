using Armyknife.Business.Interfaces;
using Armyknife.Resources;
using System;
using System.Collections.Generic;

namespace Armyknife.Tools.Implementations
{
   internal class LipsumTool : ISynchronousTool
   {
      private const string ParagraphsKey = "paragraphs";
      private static readonly Random Random = new Random();
      private readonly string[] _paragraphs = new[] { "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas fringilla aliquet dolor, eget ullamcorper tellus. Aenean non lectus vel lectus euismod porta ut sit amet mauris. Proin massa tellus, bibendum nec aliquet at, commodo a libero. In ac ultricies dui, non rhoncus tellus. In volutpat accumsan velit, sed interdum erat rhoncus id. Mauris eleifend, lorem nec malesuada interdum, nisl nulla feugiat dolor, quis vulputate enim orci in ipsum. Fusce rutrum vulputate tristique. Suspendisse arcu eros, auctor quis arcu eu, mattis ultricies nulla. Proin volutpat augue eu enim pellentesque semper. Suspendisse placerat at leo ut rutrum. In consectetur varius enim. Curabitur a sapien commodo, ornare magna scelerisque, ultricies velit. Nulla et dolor ac leo mattis maximus vel at nunc. Nam ac mi mollis, porttitor metus nec, fermentum diam. Proin arcu nulla, rutrum ac enim at, imperdiet interdum nibh. Cras molestie vel dui ut gravida.", "In quis erat eu sem dapibus elementum. Integer et aliquet urna. Fusce pellentesque sit amet libero non hendrerit. Sed a rhoncus arcu. Curabitur venenatis ipsum at libero eleifend scelerisque. Cras vestibulum feugiat ante, vel tincidunt ipsum pharetra id. Aenean consectetur auctor ultricies. Donec eleifend nisl ut ligula varius, in molestie odio venenatis. Quisque quis augue non nisl iaculis porta. Pellentesque convallis leo tellus, ut porttitor nisi convallis eu. Integer at ultricies sapien, at pretium magna. Donec eu risus vitae dolor aliquet auctor. In convallis dictum orci. Nunc ultricies iaculis ex ut viverra. Vivamus ac tortor id metus tincidunt ultrices ut a dolor. Aliquam convallis purus sit amet nulla semper feugiat.", "Aenean in sollicitudin lorem. Sed id vehicula augue. Suspendisse dui orci, volutpat at nisl non, euismod laoreet nisi. Aliquam libero ex, tincidunt eu ultricies vitae, vehicula et justo. Aenean mattis porttitor accumsan. Proin aliquam nisi at volutpat facilisis. Phasellus ac quam aliquam, congue nunc at, facilisis eros. Mauris quis porta nibh. Pellentesque tincidunt dapibus vestibulum. Cras volutpat volutpat ipsum, in lobortis justo ullamcorper sit amet. Donec non ante vulputate, facilisis orci at, maximus velit. Ut iaculis nulla vel sem finibus ullamcorper. Donec luctus venenatis mollis. Quisque venenatis hendrerit augue, id facilisis diam venenatis ac. Etiam volutpat augue quis blandit convallis. In accumsan eu urna vel faucibus.", "Sed quis lorem quis nisi pellentesque fringilla. Curabitur et orci in nibh egestas rutrum. Proin dictum lorem ligula, tempus convallis ligula venenatis molestie. Maecenas quis lorem nec massa ultrices convallis. Quisque egestas elit mollis erat varius pulvinar. Sed id ex rhoncus, mollis nunc id, pellentesque mauris. Cras lacinia leo massa, sed tincidunt lectus dignissim quis. Ut magna erat, pretium a neque eget, bibendum facilisis ante. Donec et magna at orci lacinia tincidunt vehicula ut lacus. Praesent suscipit nisl in urna iaculis porta. Aenean ac commodo velit, ullamcorper tempor enim. Donec ut lacus non purus posuere suscipit a nec urna. Pellentesque varius consequat fermentum. Praesent bibendum dui sodales tincidunt aliquam.", "Etiam posuere augue non lorem tempor pellentesque. Morbi sit amet faucibus nunc, sit amet maximus quam. Duis interdum, orci id tristique gravida, enim nisl euismod neque, quis ullamcorper lectus lectus sit amet est. Aliquam a suscipit tellus, nec dapibus mauris. In a ipsum non sem gravida rutrum. Curabitur auctor sollicitudin fermentum. Cras fringilla sit amet nisl vel aliquet. Integer sit amet augue vitae enim lobortis viverra." };
      public string Name => "lipsum";

      public string Description => ToolResources.LipsumDescription;

      public string Category => CategoryResources.TextCategory;

      public string HelpText => ToolResources.LipsumHelp;

      public bool ShowToolInHelp => true;

      public string Execute(IDictionary<string, string> args)
      {
         int paragraphs = GetNumberOfParagraphs(args);
         var list = new List<string>
           {
              _paragraphs[0]
           };
         for (int i = 1; i < paragraphs; i++)
         {
            list.Add(_paragraphs[Random.Next(0, _paragraphs.Length)]);
         }

         return string.Join($"{Environment.NewLine}{Environment.NewLine}", list);
      }

      private static int GetNumberOfParagraphs(IDictionary<string, string> args)
      {
         int paragraphs = 5;
         if (args.TryGetValue(ParagraphsKey, out string paragraphsText))
         {
            int.TryParse(paragraphsText, out paragraphs);
         }

         return paragraphs;
      }
   }
}
