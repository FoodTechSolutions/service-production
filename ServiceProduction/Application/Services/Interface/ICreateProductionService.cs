using Application.BackgroundServices.Models;
using Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interface
{
    public interface ICreateProductionService
    {
        Task ProcessEventAsync(CreateProductionModel model);
    }
}
