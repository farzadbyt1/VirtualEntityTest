using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualEntityTest.Models;

namespace VirtualEntityTest
{
    public class PriceListVirtualEntity : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            var context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

            if (context.PrimaryEntityName == "bsve_pricelisttest")
            {
                // PriceList
                if (context.MessageName == "Retrieve")
                {
                    HandleRetrieve_PriceList(serviceProvider, context);
                }
                else if (context.MessageName == "RetrieveMultiple")
                {
                    HandleRetrieveMultiple_PriceList(serviceProvider, context);
                }

            }
            else if (context.PrimaryEntityName == "bsve_producttest")
            {
                // Product
                if (context.MessageName == "Retrieve")
                {
                    HandleRetrieve_Product(serviceProvider, context);
                }
                else if (context.MessageName == "RetrieveMultiple")
                {
                    HandleRetrieveMultiple_Product(serviceProvider, context);
                }

            }
        }

        private void HandleRetrieveMultiple_Product(IServiceProvider serviceProvider, IPluginExecutionContext context)
        {
            GetData getData = new GetData();

            var entityCollection = new EntityCollection();

            if (context.InputParameters.Contains("Query") &&
                context.InputParameters["Query"] is QueryExpression query)
            {
                try
                {
                    var condition = query.Criteria.Conditions.FirstOrDefault(r => r.AttributeName == "bsve_pricelistid");

                    var priceListId = condition.Values.FirstOrDefault();

                    var products = getData.RetrieveMultipleProduct(Guid.Parse(priceListId.ToString()));

                    entityCollection = ConvertProductDtoToEntityCollection(products);
                }
                catch (Exception ex)
                {

                }
            }

            context.OutputParameters["BusinessEntityCollection"] = entityCollection;
        }

        private EntityCollection ConvertProductDtoToEntityCollection(List<ProductDto> products)
        {
            var entities = new EntityCollection();

            foreach (var pl in products)
            {
                entities.Entities.Add(ConvertProductDtoToEntity(pl));
            }

            return entities;
        }

        private void HandleRetrieve_Product(IServiceProvider serviceProvider, IPluginExecutionContext context)
        {
            var target = (EntityReference)context.InputParameters["Target"];
            var entityName = target.LogicalName;
            var entityId = target.Id;
            GetData getData = new GetData();

            var product = getData.RetrieveProduct(entityId);

            var entity = ConvertProductDtoToEntity(product);

            context.OutputParameters["BusinessEntity"] = entity;
        }

        private Entity ConvertProductDtoToEntity(ProductDto product)
        {
            var entity = new Entity("bsve_producttest");
            entity.Id = product.id;
            entity["bsve_producttestid"] = product.id;
            entity["bsve_name"] = product.name;
            entity["bsve_image"] = product.image;
            entity["bsve_publish"] = product.publish;
            if (product.priceListId.HasValue)
                entity["bsve_pricelistid"] = new EntityReference("bsve_pricelisttest", product.priceListId.Value);

            return entity;
        }

        private void HandleRetrieveMultiple_PriceList(IServiceProvider serviceProvider, IPluginExecutionContext context)
        {
            GetData getData = new GetData();
            var priceList = getData.RetrieveMultiplePriceList();

            var entityCollection = ConvertPriceListDtoToEntityCollection(priceList);

            context.OutputParameters["BusinessEntityCollection"] = entityCollection;
        }

        private EntityCollection ConvertPriceListDtoToEntityCollection(List<PriceListDto> priceList)
        {
            var entities = new EntityCollection();

            foreach (var pl in priceList)
            {
                entities.Entities.Add(ConvertPriceListDtoToEntity(pl));
            }

            return entities;
        }

        private void HandleRetrieve_PriceList(IServiceProvider serviceProvider, IPluginExecutionContext context)
        {
            var target = (EntityReference)context.InputParameters["Target"];
            var entityName = target.LogicalName;
            var entityId = target.Id;
            GetData getData = new GetData();

            var priceList = getData.RetrievePriceList(entityId);

            var entity = ConvertPriceListDtoToEntity(priceList);

            context.OutputParameters["BusinessEntity"] = entity;
        }

        private Entity ConvertPriceListDtoToEntity(PriceListDto priceList)
        {
            var entity = new Entity("bsve_pricelisttest");
            entity.Id = priceList.id;
            entity["bsve_pricelisttestid"] = priceList.id;
            entity["bsve_name"] = priceList.name;
            entity["bsve_fromdate"] = priceList.fromDate;
            entity["bsve_todate"] = priceList.toDate;
            entity["bsve_createdon"] = priceList.createdOn;
            entity["bsve_isactive"] = priceList.isActive;

            return entity;
        }
    }
}
