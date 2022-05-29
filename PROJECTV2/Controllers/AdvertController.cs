using HtmlAgilityPack;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PROJECTV2.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PROJECTV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertController : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetAdverts()
        {

            List<Advert> adverts = new List<Advert>(); // İlan listesi tanımlandı.

            var url = "https://www.sahibinden.com/"; // Site URL tanımlandı.

            HtmlWeb htmlWeb = new HtmlWeb();
            HtmlDocument htmlDocument = htmlWeb.Load(url);



            for (int i = 0; i < htmlDocument.DocumentNode.SelectNodes("//ul[@class='vitrin-list clearfix']//li//a//span").Count+3; i++) // Ana sayfa ilanlarını dolaşması için bir for döngüsü oluşturuldu.
            {


                Advert advert = new Advert(); // Bir ilan nesnesi oluşturuldu.

                advert.detailLink = htmlDocument.DocumentNode.SelectNodes("//ul[@class='vitrin-list clearfix']//li//a").ElementAt(i).GetAttributeValue("href", "test123test.com"); // Oluşturulan ilan nesnesinin detay linki özniteliğine sayfadaki ilgili değer atandı.



                if (advert.detailLink.Equals("/doping-tanitim/#doping-3") || advert.detailLink.Equals("/param-guvende/bireysel?widget_type=param-guvende-bireysel&widget_source=d-v-i"))
                {


                    continue;

                }

                advert.detailLink = "https://www.sahibinden.com" + advert.detailLink; // Oluşturulan ilan nesnesinin detay linki değeri doğru şekilde güncellendi. İlgili detay link değerinin kopyalandığı gibi tarayıcıda detay sayfasına gitmesi amaçlandı.

                advert.name = htmlDocument.DocumentNode.SelectNodes("//ul[@class='vitrin-list clearfix']//li//a").ElementAt(i).GetAttributeValue("title", "Başlık"); // Oluşturulan ilan nesnesinin ad özniteliğine sayfadaki ilgili değer atandı.

                advert.image = htmlDocument.DocumentNode.SelectNodes("//ul[@class='vitrin-list clearfix']//li//a//img").ElementAt(i).GetAttributeValue("src", "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fpbs.twimg.com%2Fprofile_images%2F716487122224439296%2FHWPluyjs.jpg&f=1&nofb=1"); // Oluşturulan ilan nesnesinin görsel özniteliğine sayfadaki ilgili değer atandı.

                advert.price = Price.GetPrice(advert.detailLink);  // Static bir sınıfın static bir metodundan fiyat bilgisi talep edilmesini amaçladım. Bu kısım şu an maalesef istediğim gibi çalışmıyor. Son teslim zamanını ezmemek adına bunu ileri bir tarihte sağlıklı bir şekilde çalışır hale getirmeyi istiyorum.

                adverts.Add(advert); // Oluşturulan ilan nesnesi ilan dizisine eklendi.

            } // For döngüsü bitti. İlan listesi dönmeye (return edilmeye) hazır hale geldi.

            return Ok(adverts); // İlan dizisi başarılı bir şekilde dönerse api tarafında 200 (başarılı) http koduyla birlikte ilan sınıfı değerlerini gözlemleyebileceğiz.

        }




    }
}
