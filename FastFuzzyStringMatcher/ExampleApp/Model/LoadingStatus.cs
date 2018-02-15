using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleApp.Model
{
    public class LoadingStatus
    {
        public int PercentComplete { get; set; }
        public int TranslationsLoaded { get; set; }
        public int TotalTranslationsToLoad { get; set; }
    }
}
