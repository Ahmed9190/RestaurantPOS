using AutoMapper;
using RestaurantPOS.DTOs;
using RestaurantPOS.Entities;

namespace RestaurantPOS.Mappings;

public class AutoMapperProfile : Profile
{
  public AutoMapperProfile()
  {
    // === Customer ===
    CreateMap<CreateCustomerDto, Customer>();
    CreateMap<UpdateCustomerDto, Customer>();
    CreateMap<Customer, CustomerDto>();

    // === MenuItem ===
    CreateMap<CreateMenuItemDto, MenuItem>();
    CreateMap<UpdateMenuItemDto, MenuItem>();
    CreateMap<MenuItem, MenuItemDto>();

    // === Table ===
    CreateMap<CreateTableDto, Table>();
    CreateMap<UpdateTableDto, Table>();
    CreateMap<Table, TableDto>();

    // === Reservation ===
    CreateMap<CreateReservationDto, Reservation>();
    CreateMap<Reservation, ReservationDto>();

    // === Payment ===
    CreateMap<CreatePaymentDto, Payment>()
        .ForMember(dest => dest.Id, opt => opt.Ignore())
        .ForMember(dest => dest.PaidAt, opt => opt.Ignore())
        .ForMember(dest => dest.Order, opt => opt.Ignore());

    // Payment -> PaymentDto
    CreateMap<Payment, PaymentDto>();

    // === Order ===
    CreateMap<Order, OrderDto>()
      .ForMember(dest => dest.TableNumber, opt =>
        opt.MapFrom(src => src.Table != null ? src.Table.Number : (int?)null));

    // === Category ===
    CreateMap<CreateCategoryDto, Category>();
    CreateMap<UpdateCategoryDto, Category>();
    CreateMap<Category, CategoryDto>();
  }
}
