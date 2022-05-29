using HtmlAgilityPack;

namespace PROJECTV2.Model
{
    public static class Price
    {


        public static string GetPrice(string detailLink)
        {

            HtmlWeb htmlWeb = new HtmlWeb();
            HtmlDocument htmlDocument = htmlWeb.Load(detailLink);


            if (htmlDocument.DocumentNode.SelectSingleNode("//div[@class='classifiedInfo']//h3[@text]") != null)
            {

                return htmlDocument.DocumentNode.SelectSingleNode("//div[@class='classifiedInfo']//h3[@text]").InnerText;

            }


            return "0 TL";
        }




    }
}
