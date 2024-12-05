using Microsoft.Xrm.Sdk;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VirtualEntityTest.Models;

namespace VirtualEntityTest
{
    public class GetData
    {
        private readonly string apiBaseUrl = "http://192.168.95.132:40100/api";

        public PriceListDto RetrievePriceList(Guid id)
        {
            string url = $"{apiBaseUrl}/PriceList/GetAllQ?$filter=Id eq {id}";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result;

                    var result = JsonConvert.DeserializeObject<BaseClass<PriceListDto>>(jsonString);

                    if (result.items.Count > 0)
                    {

                        return result.items.FirstOrDefault();
                    }
                    else
                    {
                        return new PriceListDto();
                    }
                }
                else
                {
                    throw new Exception("Error");
                }
            }
        }
    
        public List<PriceListDto> RetrieveMultiplePriceList()
        {
            string url = $"{apiBaseUrl}/PriceList/GetAllQ";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result;

                    var result = JsonConvert.DeserializeObject<BaseClass<PriceListDto>>(jsonString);

                    return result.items;
                }
                else
                {
                    throw new Exception("Error");
                }
            }
        }

        public ProductDto RetrieveProduct(Guid id)
        {
            string url = $"{apiBaseUrl}/Product/GetAllQ?$filter=Id eq {id}";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result;

                    var result = JsonConvert.DeserializeObject<BaseClass<ProductDto>>(jsonString);

                    if (result.items.Count > 0)
                    {

                        return result.items.FirstOrDefault();
                    }
                    else
                    {
                        return new ProductDto();
                    }
                }
                else
                {
                    throw new Exception("Error");
                }
            }
        }

        public List<ProductDto> RetrieveMultipleProduct(Guid priceListId)
        {
            string url = $"{apiBaseUrl}/Product/GetAllQ?$filter=priceListId eq {priceListId}";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result;

                    var result = JsonConvert.DeserializeObject<BaseClass<ProductDto>>(jsonString);

                    return result.items;
                }
                else
                {
                    throw new Exception("Error");
                }
            }
        }


    }
}

