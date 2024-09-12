using Application.BackgroundServices.Models;
using Application.Services.Interface;
using Domain.DTO;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CreateProductionService(
        ILogger<CreateProductionService> logger, 
        IProductionRepository productionRepository, 
        IProductRepository productRepository, 
        IProductionProductRepository productionProductRepository) : ICreateProductionService
    {

        public async Task ProcessEventAsync(CreateProductionModel model)
        {
            try
            {
                var products = productRepository.GetAll();

                var production = Production.CreateProduction();

                production
                    .SetCustomer(model.Customer)
                    .SetOrder(model.Order);

                productionRepository.Add(production);

                //foreach (var item in model.Items)
                //{
                //    var prod = products.FirstOrDefault(x => x.Name == item.Name);

                //    if (prod == null)
                //    {
                //        prod = Product.CreateProduct();
                //        prod.SetName(item.Name);
                //        productRepository.Add(prod);
                //    }

                //    var productionProduct = ProductionProduct.CreateProductionProduct();

                //    productionProduct
                //        .SetProduction(production.Id)
                //        .SetProduct(prod.Id);

                //    productionProductRepository.Add(productionProduct);
                //}
            }
            catch (Exception e)
            {
                logger.Log(LogLevel.Error, e.Message);
            }
        }
    }
}
