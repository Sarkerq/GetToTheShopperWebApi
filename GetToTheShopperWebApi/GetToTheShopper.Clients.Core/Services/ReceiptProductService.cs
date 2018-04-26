using GetToTheShopper.Clients.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetToTheShopper.Clients.Core.Services
{
    public class ReceiptProductService
    {
        public bool AddProductToList(ReceiptProductDTO receiptsProduct)
        {
            return WebClient.PostAsync<ReceiptProductDTO>("ReceiptProduct", receiptsProduct).Result;
        }

        public bool UpdateProductOnList(ReceiptProductDTO receiptsProduct)
        {
            return WebClient.PutAsync<ReceiptProductDTO>("ReceiptProduct/" + receiptsProduct.Id, receiptsProduct).Result;
        }

        public bool DeleteProductFromList(ReceiptProductDTO receiptsProduct)
        {
            return WebClient.DeleteAsync<ReceiptProductDTO>("ReceiptProduct/" + receiptsProduct.Id).Result;
        }
        public double GetTotalReceiptPrice(int receiptId)
        {
            double totalPrice = 0;

            ProductService productService = new ProductService();
            List<ReceiptProductDTO> receiptProducts = GetReceiptProductsFromReceipt(receiptId);
            foreach (ReceiptProductDTO p in receiptProducts)
            {
                ProductDTO product = productService.GetProduct(p.ProductId);
                if (product != null)
                {
                    totalPrice += product.Price * p.Quantity;
                }
            }
            return totalPrice;
        }

        public List<ReceiptProductDTO> GetReceiptProductsFromReceipt(int receiptId)
        {
            return WebClient.ReadResponse<List<ReceiptProductDTO>>("ReceiptProduct/" + receiptId).Result;
        }
    }
}
