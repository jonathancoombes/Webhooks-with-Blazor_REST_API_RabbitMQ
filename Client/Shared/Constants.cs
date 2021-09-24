using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceXAirlines.Client.Shared
{

   public static class ConfigurationSettings
   { 
       // Production
       // public static readonly string PostUrl = "http://coombes.eastus2.cloudapp.azure.com/api/WebhookSubscription";
       // public static readonly string PriceUpdateUrl = "http://coombes.eastus2.cloudapp.azure.com/api/Flight";
       
       //Dev
       public static readonly string PostUrl = "api/WebhookSubscription";
       public static readonly string PriceUpdateUrl = "api/Flight";

       public static readonly string cssShow = "display:block";
       public static readonly string cssHide = "display:none";
       public static readonly string PriceUpdates = "PriceUpdates";
       public static readonly string DelayUpdates = "DelayUpdates";

   }

}
