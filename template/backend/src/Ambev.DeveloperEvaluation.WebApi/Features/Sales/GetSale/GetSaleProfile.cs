﻿using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale
{
    /// <summary>
    /// Profile for mapping between Application and API GetSale responses
    /// </summary>
    public class GetSaleProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for GetSale feature
        /// </summary>
        public GetSaleProfile()
        {
            CreateMap<long, GetSaleCommand>()
               .ConstructUsing(id => new GetSaleCommand(id));
        }
    }
}