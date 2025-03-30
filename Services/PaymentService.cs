using AutoMapper;
using RestaurantPOS.DTOs;
using RestaurantPOS.Entities;
using RestaurantPOS.Repositories;
using RestaurantPOS.Exceptions;

namespace RestaurantPOS.Services;

public class PaymentService
{
  private readonly IPaymentRepository _paymentRepo;
  private readonly IOrderRepository _orderRepo;
  private readonly IMapper _mapper;

  public PaymentService(IPaymentRepository paymentRepo, IOrderRepository orderRepo, IMapper mapper)
  {
    _paymentRepo = paymentRepo;
    _orderRepo = orderRepo;
    _mapper = mapper;
  }

  public async Task<PaymentDto> CreateAsync(CreatePaymentDto dto)
  {
    var order = await _orderRepo.GetByIdAsync(dto.OrderId);
    if (order == null)
      throw new ResourceNotFoundException($"Order #{dto.OrderId} not found");

    // Map DTO to Payment entity; override PaidAt to current time.
    var payment = _mapper.Map<Payment>(dto);
    payment.PaidAt = DateTime.UtcNow;

    var saved = await _paymentRepo.CreateAsync(payment);
    return _mapper.Map<PaymentDto>(saved);
  }

  public async Task<List<PaymentDto>> GetAllAsync()
  {
    var payments = await _paymentRepo.GetAllAsync();
    return _mapper.Map<List<PaymentDto>>(payments);
  }

  public async Task<PaymentDto?> GetByIdAsync(int id)
  {
    var p = await _paymentRepo.GetByIdAsync(id);
    return p == null ? null : _mapper.Map<PaymentDto>(p);
  }
}
