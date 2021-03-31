using System;
using System.Collections.Generic;
using CSModel.ViewModels.Shared;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CSService.Controllers
{
    public class DropdownValueController : ApiController
    {
        public List<DropdownValue> Get(string KindCode)
        {
            var lstDropdownValue = new List<DropdownValue>();
            switch (KindCode)
            {
                case "news_kind":
                    lstDropdownValue = new List<DropdownValue>()
                    {
                        new DropdownValue(){
                            value = "1",
                            text = "即時新聞"
                        },
                        new DropdownValue(){
                            value = "2",
                            text = "重大政策"
                        },
                        new DropdownValue(){
                            value = "3",
                            text = "業務新訊"
                        },
                        new DropdownValue(){
                            value = "4",
                            text = "活動快訊"
                        },
                        new DropdownValue(){
                            value = "5",
                            text = "就業資訊"
                        },
                    };
                    break;
                default:
                    break;
            }

            return lstDropdownValue;
        }

    }
}
