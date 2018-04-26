#define TEST

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;
using GetToTheShopper.WebApi.DTO;
using GetToTheShopper.WebApi.Controllers;
using GetToTheShopper.WebApi.Repositories.Interfaces;
using GetToTheShopper.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using System.CodeDom;
using Microsoft.AspNetCore.Hosting;
using GetToTheShopper.WebApi;
using System.Net.Http;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using WebApplication1.Models.AccountViewModels;
using System.Threading.Tasks;

namespace GetToTheShopperTests
{
    public static class HttpContentExtensions
    {
        public static async Task<T> ReadAsJsonAsync<T>(this HttpContent content)
        {
            string json = await content.ReadAsStringAsync();
            T value = JsonConvert.DeserializeObject<T>(json);
            return value;
        }
    }


    [TestClass]
    public class ModifyDBDataTests
    {
        IWebHostBuilder builder = new WebHostBuilder()
                      .UseStartup<TestStartup>();
        HttpRequestMessage request;
        HttpResponseMessage response;
        string json;
        TestServer _server;
        HttpClient _client;
        private void InitializeServerAndClient()
        {
            _server = new TestServer(builder);
            _client = _server.CreateClient();
        }
        private void standardPost(string url, object toSerialize)
        {
            request = new HttpRequestMessage(HttpMethod.Post, url);
            json = JsonConvert.SerializeObject(toSerialize);
            request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            _client.PostAsync(url, request.Content).Wait();
        }
        private void standardPut(string url, object toSerialize)
        {
            request = new HttpRequestMessage(HttpMethod.Put, url);
            json = JsonConvert.SerializeObject(toSerialize);
            request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            response = _client.PutAsync(url, request.Content).Result;
        }
        private T standardGet<T>(string url)
        {
            response = _client.GetAsync(url).Result;
            return response.Content.ReadAsJsonAsync<T>().Result;
        }
        private void standardDelete(string url)
        {
            _client.DeleteAsync(url).Wait();
        }
        [TestMethod]
        public void AddingAProductToDBIncrementsProductTableSize()
        {
            InitializeServerAndClient();

            List<ProductDTO> before = standardGet<List<ProductDTO>>("/api/Product");

            standardPost("/api/Product", new ProductDTO() { Name = "Chleb", Price = 6.6, Unit = new Unit(Unit.UnitName.sztuka) });

            List<ProductDTO> after = standardGet<List<ProductDTO>>("/api/Product");

            Assert.AreEqual(before.Count + 1, after.Count);
        }



        [TestMethod]
        public void AddingAShopProductAddsItToDB()
        {
            InitializeServerAndClient();


            //Adding product
            ProductDTO productDTO = new ProductDTO() { Name = "Ry¿ bia³y", Price = 2.5, Unit = new Unit(Unit.UnitName.kilogram) };

            standardPost("/api/Product", productDTO);

            List<ProductDTO> products = standardGet<List<ProductDTO>>("/api/Product");

            //Adding shop
            ShopDTO shopDTO = new ShopDTO() { Name = "Tesco" };

            standardPost("/api/Shop", shopDTO);

            List<ShopDTO> shops = standardGet<List<ShopDTO>>("/api/Shop");

            //Adding shopproduct
            ShopProductDTO shopProductDTO = new ShopProductDTO() { AvailableUnits = 30, Aisle = "4f", Product = products.Last(), ShopId = shops.Last().Id };

            standardPost("/api/ShopProduct", shopProductDTO);

            List<ShopProductDTO> shopProducts = standardGet<List<ShopProductDTO>>("/api/ShopProduct");

            Assert.AreEqual(shopProducts.Last().Product.Name, productDTO.Name);
            Assert.AreEqual(shopProducts.Last().AvailableUnits, shopProductDTO.AvailableUnits);

        }


        [TestMethod]
        public void EditingAProductNameChangesItsDBName()
        {
            InitializeServerAndClient();


            //Adding product
            ProductDTO productDTO = new ProductDTO() { Name = "Ry¿ bia³y", Price = 2.5, Unit = new Unit(Unit.UnitName.kilogram) };

            standardPost("/api/Product", productDTO);

            List<ProductDTO> products = standardGet<List<ProductDTO>>("/api/Product");

            ProductDTO productToChange = products.Last();
            string newName = "Makaron";
            productToChange.Name = newName;
            //Editing product
            standardPut("/api/Product/" + productToChange.Id, productToChange);

            products = standardGet<List<ProductDTO>>("/api/Product");

            ProductDTO productChanged = products.Last();
            Assert.AreEqual(newName, productChanged.Name);
        }

        [TestMethod]
        public void EditingAProductPriceChangesItsDBPrice()
        {
            InitializeServerAndClient();

            //Adding product
            ProductDTO productDTO = new ProductDTO() { Name = "Ry¿ bia³y", Price = 2.5, Unit = new Unit(Unit.UnitName.kilogram) };

            standardPost("/api/Product", productDTO);

            List<ProductDTO> products = standardGet<List<ProductDTO>>("/api/Product");

            //Editing product

            ProductDTO productToChange = products.Last();
            double newPrice = 5.6;
            productToChange.Price = newPrice;

            standardPut("/api/Product/" + productToChange.Id, productToChange);

            products = standardGet<List<ProductDTO>>("/api/Product");

            ProductDTO productChanged = products.Last();
            Assert.AreEqual(newPrice, productChanged.Price);
        }
        [TestMethod]
        public void EditingAShopProductQuantityChangesItsDBQuantity()
        {
            InitializeServerAndClient();

            //Adding product
            ProductDTO productDTO = new ProductDTO() { Name = "Ry¿ bia³y", Price = 2.5, Unit = new Unit(Unit.UnitName.kilogram) };

            standardPost("/api/Product", productDTO);

            List<ProductDTO> products = standardGet<List<ProductDTO>>("/api/Product");

            //Adding shop
            ShopDTO shopDTO = new ShopDTO() { Name = "Tesco" };

            standardPost("/api/Shop", shopDTO);

            List<ShopDTO> shops = standardGet<List<ShopDTO>>("/api/Shop");

            //Adding shopproduct
            ShopProductDTO shopProductDTO = new ShopProductDTO() { AvailableUnits = 30, Aisle = "4f", Product = products.Last(), ShopId = shops.Last().Id };

            standardPost("/api/ShopProduct", shopProductDTO);

            //Editing product
            List<ShopProductDTO> shopProducts = standardGet<List<ShopProductDTO>>("/api/ShopProduct");

            ShopProductDTO shopProductToChange = shopProducts.Last();
            double newQuantity = 5.6;
            shopProductToChange.AvailableUnits = newQuantity;

            standardPut("/api/ShopProduct/" + shopProductToChange.Id, shopProductToChange);

            shopProducts = standardGet<List<ShopProductDTO>>("/api/ShopProduct");

            ShopProductDTO shopProductChanged = shopProducts.Last();
            Assert.AreEqual(newQuantity, shopProductChanged.AvailableUnits);

        }
        [TestMethod]
        public void EditingAProductUnitChangesItsDBUnit()
        {
            InitializeServerAndClient();

            //Adding product
            ProductDTO productDTO = new ProductDTO() { Name = "Ry¿ bia³y", Price = 2.5, Unit = new Unit(Unit.UnitName.kilogram) };

            standardPost("/api/Product", productDTO);

            List<ProductDTO> products = standardGet<List<ProductDTO>>("/api/Product");
            ProductDTO productToChange = products.Last();
            Unit newUnit = new Unit(Unit.UnitName.sztuka);
            productToChange.Unit = newUnit;
            //Editing product
            standardPut("/api/Product/" + productToChange.Id, productToChange);

            products = standardGet<List<ProductDTO>>("/api/Product");

            ProductDTO productChanged = products.Last();
            Assert.AreEqual(newUnit.Name, productChanged.Unit.Name);
        }
        [TestMethod]
        public void DeletingAProductRemovesItFromDB()
        {
            InitializeServerAndClient();


            //Adding product
            ProductDTO productDTO = new ProductDTO() { Name = "Ry¿ bia³y", Price = 2.5, Unit = new Unit(Unit.UnitName.kilogram) };

            standardPost("/api/Product", productDTO);
            List<ProductDTO> products = standardGet<List<ProductDTO>>("/api/Product");
            int before = products.Count;

            //Deleting product

            standardDelete("/api/Product/" + products.Last().Id);

            products = standardGet<List<ProductDTO>>("/api/Product");

            int after = products.Count;
            Assert.AreEqual(before - 1, after);

        }
        [TestMethod]
        public void DeletingAReceiptRemovesItFromDB()
        {
            InitializeServerAndClient();


            //Adding receipt
            ReceiptDTO receiptDTO = new ReceiptDTO() { Name = "Niedzielny wieczór", Done = false, AuthorId = "0" };

            standardPost("/api/Receipt", receiptDTO);

            List<ReceiptDTO> receipts = standardGet<List<ReceiptDTO>>("/api/Receipt");

            int before = receipts.Count;

            //Deleting receipt
            standardDelete("/api/Receipt/" + receipts.Last().Id);

            receipts = standardGet<List<ReceiptDTO>>("/api/Receipt");


            int after = receipts.Count;
            Assert.AreEqual(before - 1, after);
        }
        [TestMethod]
        public void DeletingAShopRemovesItFromDB()
        {
            InitializeServerAndClient();


            //Adding shop
            ShopDTO shopDTO = new ShopDTO() { Name = "Tesco", Address = "Mickiewicza 5" };

            standardPost("/api/Shop", shopDTO);

            List<ShopDTO> shops = standardGet<List<ShopDTO>>("/api/Shop");
            
            int before = shops.Count;

            //Deleting shop
            standardDelete("/api/Shop/" + shops.Last().Id);

            shops = standardGet<List<ShopDTO>>("/api/Shop");

            int after = shops.Count;
            Assert.AreEqual(before - 1, after);
        }
        [TestMethod]
        public void EditingAReceiptNameChangesItInDB()
        {
            InitializeServerAndClient();


            //Adding receipt
            ReceiptDTO receiptDTO = new ReceiptDTO() { Name = "Niedzielny wieczór", Done = false, AuthorId = "0" };

            standardPost("/api/Receipt", receiptDTO);

            List<ReceiptDTO> receipts = standardGet<List<ReceiptDTO>>("/api/Receipt");

            int before = receipts.Count;

            //Deleting receipt

            standardDelete("/api/Receipt/" + receipts.Last().Id);

            receipts = standardGet<List<ReceiptDTO>>("/api/Receipt");




            int after = receipts.Count;
            Assert.AreEqual(before - 1, after);
        }
        [TestMethod]
        public void AddingAReceiptAddsItToDB()
        {
            InitializeServerAndClient();


            //Adding receipt
            ReceiptDTO receiptDTO = new ReceiptDTO() { Name = "Niedzielny wieczór", Done = false, AuthorId = "0" };

            List<ReceiptDTO> receipts = standardGet<List<ReceiptDTO>>("/api/Receipt");

            int before = receipts.Count;

            standardPost("/api/Receipt", receiptDTO);

            receipts = standardGet<List<ReceiptDTO>>("/api/Receipt");

            int after = receipts.Count;
            Assert.AreEqual(before + 1, after);
        }
        [TestMethod]
        public void AddingAShopAddsItToDB()
        {
            InitializeServerAndClient();


            //Adding shop
            ShopDTO shopDTO = new ShopDTO() { Name = "Tesco", Address = "Mickiewicza 5" };

            List<ShopDTO> shops = standardGet<List<ShopDTO>>("/api/Shop");

            int before = shops.Count;

            standardPost("/api/Shop", shopDTO);

            shops = standardGet<List<ShopDTO>>("/api/Shop");

            int after = shops.Count;
            Assert.AreEqual(before + 1, after);
        }
    }
    [TestClass]
    public class IntegrationTest
    {
        IWebHostBuilder builder = new WebHostBuilder()
              .UseStartup<TestStartup>();
        HttpRequestMessage request;
        HttpResponseMessage response;
        string json;
        TestServer _server;
        HttpClient _client;
        private void InitializeServerAndClient()
        {
            _server = new TestServer(builder);
            _client = _server.CreateClient();
        }
        private T standardGet<T>(string url)
        {
            response = _client.GetAsync(url).Result;
            return response.Content.ReadAsJsonAsync<T>().Result;
        }
        private void standardPost(string url, object toSerialize)
        {
            request = new HttpRequestMessage(HttpMethod.Post, url);
            json = JsonConvert.SerializeObject(toSerialize);
            request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            _client.PostAsync(url, request.Content).Wait();
        }
        [TestMethod]
        public void CheckAvailability()
        {
            InitializeServerAndClient();

            //Adding 2 products
            ProductDTO productDTO1 = new ProductDTO() { Name = "Ry¿ bia³y", Price = 2.5, Unit = new Unit(Unit.UnitName.kilogram) };
            ProductDTO productDTO2 = new ProductDTO() { Name = "Chleb", Price = 2.5, Unit = new Unit(Unit.UnitName.sztuka) };

            standardPost("/api/Product", productDTO1);
            standardPost("/api/Product", productDTO2);

            List<ProductDTO> products = standardGet<List<ProductDTO>>("/api/Product");

            //Adding shop
            ShopDTO shopDTO = new ShopDTO() { Name = "Tesco", Address = "Mickiewicza 5" };

            standardPost("/api/Shop", shopDTO);

            List<ShopDTO> shops = standardGet<List<ShopDTO>>("/api/Shop");

            //Adding shopproduct
            ShopProductDTO shopProductDTO = new ShopProductDTO() { AvailableUnits = 30, Aisle = "4f", Product = products.Last(), ShopId = shops.Last().Id };

            standardPost("/api/ShopProduct", shopProductDTO);

            //Adding receipt
            ReceiptDTO receiptDTO = new ReceiptDTO() { Name = "Niedzielny wieczór", Done = false, AuthorId = "0" };

            standardPost("/api/Receipt", receiptDTO);

            List<ReceiptDTO> receipts = standardGet<List<ReceiptDTO>>("/api/Receipt");


            //Adding 2 receipt products
            ReceiptProductDTO receiptProductDTO1 = new ReceiptProductDTO() { ProductId = products.Last().Id, Quantity = 10, ReceiptId = receipts.Last().Id };
            ReceiptProductDTO receiptProductDTO2 = new ReceiptProductDTO() { ProductId = products.First().Id, Quantity = 10, ReceiptId = receipts.Last().Id };

            standardPost("/api/ReceiptProduct", receiptProductDTO1);
            standardPost("/api/ReceiptProduct", receiptProductDTO2);

            //Returning availability
            List<ReceiptInShopDTO> receiptInShop = standardGet<List<ReceiptInShopDTO>>("/api/Receipt/ReceiptInShops/" + receipts.Last().Id);

            Assert.AreEqual(0.5, receiptInShop.First().Availability);
        }
    }
}

