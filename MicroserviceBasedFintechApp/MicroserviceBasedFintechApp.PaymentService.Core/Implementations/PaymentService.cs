using MicroserviceBasedFintechApp.PaymentService.Core.Abstractions.Repository;
using MicroserviceBasedFintechApp.PaymentService.Core.Abstractions.Services;
using MicroserviceBasedFintechApp.PaymentService.Core.Contracts.Entities;
using MicroserviceBasedFintechApp.PaymentService.Core.Contracts.Enums;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceBasedFintechApp.PaymentService.Core.Implementations
{
    public class PaymentService : IPaymentService
    {
        private static readonly Random random = new Random();
        private readonly IGenericRepository<PaymentOrder> _paymentOrderRepo;
        private readonly IGenericRepository<AggregatedOrdersDaily> _aggregatedOrdersRepo;
        public PaymentService(
            IGenericRepository<PaymentOrder> paymentOrderRepo,
            IGenericRepository<AggregatedOrdersDaily> aggregatedOrdersRepo)
        {
            _paymentOrderRepo = paymentOrderRepo;
            _aggregatedOrdersRepo = aggregatedOrdersRepo;
        }
        public async Task AddOrder(PaymentOrder order)
        {
            PaymentOrder? orderInDb = await _paymentOrderRepo
                .GetQueryable()
                .SingleOrDefaultAsync(o => o.Id == order.Id);

            if (orderInDb != null) return;

            order.Status = CalculateTransactionStatus();
            
            await _paymentOrderRepo.InsertAsync(order);
            
            if(order.Status == Status.Rejected)
            {
                await _paymentOrderRepo.SaveChangesAsync();
                return;
            }

            AggregatedOrdersDaily? aggregatedDaily = await _aggregatedOrdersRepo
                .GetQueryable()
                .SingleOrDefaultAsync(a => a.ApiKey == order.ApiKey && a.DateAggregationUTC == order.CreationDateAtUtc.AddHours(4).Date);

            if (aggregatedDaily == null)
            {
                aggregatedDaily = new AggregatedOrdersDaily()
                {
                    CreationDateAtUtc = DateTime.UtcNow,
                    UpdateDateAtUtc = DateTime.UtcNow,
                    Amount = order.Amount,
                    ApiKey = order.ApiKey,
                    DateAggregationUTC = order.CreationDateAtUtc.AddHours(4).Date
                };
                await _aggregatedOrdersRepo.InsertAsync(aggregatedDaily);
            }
            else
            {
                aggregatedDaily.Amount += order.Amount;
            }
            if (aggregatedDaily.Amount > 10_000)
            {
                aggregatedDaily.Amount -= order.Amount;
                order.Status = Status.Rejected;
            }

            await _paymentOrderRepo.SaveChangesAsync();
        }


        private Status CalculateTransactionStatus()
        {
            return random.NextDouble() < 0.5 ? Status.Completed : Status.Rejected;
        }
    }
}
