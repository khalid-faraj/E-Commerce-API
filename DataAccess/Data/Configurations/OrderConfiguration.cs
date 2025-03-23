using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models.Order_Aggregate;
using Order = Core.Models.Order_Aggregate.Order;

namespace DataAccess.Data.Configurationsig
{
	public class OrderConfiguration : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.OwnsOne(o => o.ShippedToAddress, a =>
			{
				a.WithOwner();
			});

			builder.Navigation(a => a.ShippedToAddress).IsRequired();

			builder.Property(a => a.Status)
				.HasConversion(
				 o => o.ToString(),
				 o => (OrderStatus)Enum.Parse(typeof(OrderStatus), o)
				 );

			builder.HasMany(o => o.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);

		}
	}
}
