
using Application.DTOs;
using Domain.Entities;

namespace Application.Mapping
{
    public static class CustomerMapper
    {
        public static CustomerDto ToDto(this Customer customer)
        {
            if (customer == null) throw new ArgumentNullException(nameof(customer));

            return new CustomerDto
            {
                CustomerId = customer.CustomerId,
                CustomerCode = customer.CustomerCode,
                CustomerName = customer.CustomerName,
                CustomerType = customer.CustomerType,
                GSTIN = customer.GSTIN,
                PAN = customer.PAN,
                GSTRegistrationType = customer.GSTRegistrationType,
                IsGSTRegistered = customer.IsGSTRegistered,
                PlaceOfSupplyState = customer.PlaceOfSupplyState,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber
            };
        }

        public static Customer ToDomain(this CustomerDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            return new Customer
            {
                CustomerId = dto.CustomerId,
                CustomerCode = dto.CustomerCode?.Trim() ?? string.Empty,
                CustomerName = dto.CustomerName?.Trim() ?? string.Empty,
                CustomerType = dto.CustomerType?.Trim() ?? string.Empty,
                GSTIN = dto.GSTIN?.Trim(),
                PAN = dto.PAN?.Trim(),
                GSTRegistrationType = dto.GSTRegistrationType,
                IsGSTRegistered = dto.IsGSTRegistered,
                PlaceOfSupplyState = dto.PlaceOfSupplyState,
                Email = dto.Email?.Trim(),
                PhoneNumber = dto.PhoneNumber?.Trim(),
                CreatedBy = "System", 
                CreatedDate = DateTime.Now
            };
        }

        public static Customer ToDomain(this CustomerDto dto, int id)
        {
            var customer = dto.ToDomain();
            customer.CustomerId = id;
            customer.ModifiedBy = "System";
            customer.ModifiedDate = DateTime.Now;
            return customer;
        }
    }
}
