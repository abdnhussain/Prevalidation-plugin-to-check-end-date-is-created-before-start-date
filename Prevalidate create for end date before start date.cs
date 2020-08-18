using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;

namespace WalkersD365_Plugin___Prevalidate_create_for_end_date_before_start_date
{
    public class Prevalidate_create_for_end_date_before_start_date : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

            if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity)
            {
                var entity = (Entity)context.InputParameters["Target"];
                if (context.PrimaryEntityName == "ntt_subscription")
                {
                    var startdate = entity.GetAttributeValue<DateTime>("ntt_startdate");
                    var enddate = entity.GetAttributeValue<DateTime>("ntt_enddate");

                    if (enddate<startdate)
                    {
                        throw new InvalidPluginExecutionException("Please choose an end date after the start date!");
                    }
                }
            }
        }
    }
}
