using System;
using System.ComponentModel.DataAnnotations;

namespace NordicRealEstate.Api.Models
{
	public class BaseModel
	{
		[Key]
		public int Id { get; set; }

		public DateTime? DtCreated { get; set; }

		public int CreatedUserId { get; set; }

		public DateTime? DtUpdated { get; set; }

		public int UpdatedUserId { get; set; }
		
		public DateTime? DtDeleted { get; set; }

		public int DeletedUserId { get; set; }

		public bool IsActive { get; set; }
		
		public bool IsDeleted { get; set; }
	
		/// <summary>
		/// to check concurrency issue
		/// </summary>
		public int Stamp { get; set; }

	}
}
