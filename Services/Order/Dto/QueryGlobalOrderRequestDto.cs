﻿namespace shopsport.Services.Order.Dto
{
	public class QueryGlobalOrderRequestDto
	{
		public Guid Order_id { get; init; }
		public Guid Id { get; init; }
		public string Email { get; init; }
		public string PhoneNumber { get; init; }
		public string Address { get; init; }
		public string Note { get; init; }
		public string FirstName { get; init; }
		public string LastName { get; init; }
	}
}
