using GetToTheShopper.WebApi.ModelsHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetToTheShopper.WebApi.DTO.Assemblers
{
    public class ProductFromListInShopAssembler
    {
        ShopProductAssembler shopProductAssembler = new ShopProductAssembler();
        ProductAssembler productAssembler = new ProductAssembler();
        ReceiptProductAssembler receiptProductAssembler = new ReceiptProductAssembler();
        public ProductFromListInShopDTO GetDTO(ProductFromListInShop product)
        {
            return new ProductFromListInShopDTO()
            {
                ShopProduct = shopProductAssembler.GetDTO(product.ShopProduct),
                Product = productAssembler.GetDTO(product.Product),
                WantedQuantity = product.WantedQuantity,
                EnoughUnits = product.EnoughUnits,
                ReceiptProduct = receiptProductAssembler.GetDTO(product.ReceiptProduct)
            };
        }

        public ProductFromListInShop GetModel(ProductFromListInShopDTO productDTO)
        {
            return new ProductFromListInShop()
            {
                ShopProduct = shopProductAssembler.GetModel(productDTO.ShopProduct),
                Product = productAssembler.GetModel(productDTO.Product),
                WantedQuantity = productDTO.WantedQuantity,
                EnoughUnits = productDTO.EnoughUnits,
                ReceiptProduct = receiptProductAssembler.GetModel(productDTO.ReceiptProduct)
            };
        }
    }
}
