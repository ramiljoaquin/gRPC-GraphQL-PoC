using AccountService.Data;
using AccountService.Queries;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountService.Services
{
	public class ProfileQueryService : ProfileQuery.ProfileQueryBase
	{
		private readonly ProfileDbContext _dbContext;
		public ProfileQueryService(ProfileDbContext profileDbContext)
		{
			_dbContext = profileDbContext;
		}

		public override async Task<ProfileResponse> GetProfile(ProfileRequest request, ServerCallContext context)
		{
			var profile = await _dbContext.Profiles.SingleOrDefaultAsync(q => q.ProfileId == Guid.Parse(request.ProfileId));

			if (profile == null)
			{
				// Return empty profile instead
				return new ProfileResponse();
			}

			return new ProfileResponse
			{
				ProfileId = profile.ProfileId.ToString(),
				FirstName = profile.FirstName,
				LastName = profile.LastName,
				Email = profile.Email,
				CompanyName = profile.CompanyName,
				BirthDate = profile.BirthDate?.ToString() ?? "",
				Website = profile.Website ?? "",
				PhotoThumbUrl = profile.PhotoThumbUrl ?? "",
				Phone = profile.Phone ?? "",
			};
		}

		public override async Task<ProfilePageResponse> GetProfilePage(ProfilePageRequest request, ServerCallContext context)
		{
			var query = _dbContext.Profiles.Where(q => q.Deleted == false);

			string keywords = request.Keywords ?? "";

			if (keywords.Length > 1)
			{
				query = query.Where(q => q.FirstName.Contains(keywords, StringComparison.OrdinalIgnoreCase) || q.LastName.Contains(keywords, StringComparison.OrdinalIgnoreCase));
			}

			string orderBy = request.OrderBy ?? "createdwhen_desc";

			switch (orderBy)
			{
				case "firstname_asc":
					{
						query = query.OrderBy(q => q.FirstName);
						break;
					}
				case "firstname_desc":
					{
						query = query.OrderByDescending(q => q.FirstName);
						break;
					}
				case "lastname_asc":
					{
						query = query.OrderBy(q => q.LastName);
						break;
					}
				case "lastname_desc":
					{
						query = query.OrderByDescending(q => q.LastName);
						break;
					}
				case "lasteditedwhen_asc":
					{
						query = query.OrderBy(q => q.LastEditedWhen);
						break;
					}
				case "lasteditedwhen_desc":
					{
						query = query.OrderByDescending(q => q.LastEditedWhen);
						break;
					}
				case "createdwhen_asc":
					{
						query = query.OrderBy(q => q.CreatedWhen);
						break;
					}
				default: 
					{
						query = query.OrderByDescending(q => q.CreatedWhen);
						break;
					}
			}

			int count = await query.CountAsync();
			int page = Math.Min(0, request.Page - 1);
			int pageSize = Math.Max(5, request.PageSize);

			var records = await query.Skip(page * pageSize)
				.Take(pageSize)
				.Select(q => new ProfileResponse
			{
				ProfileId = q.ProfileId.ToString(),
				FirstName = q.FirstName,
				LastName = q.LastName,
				Email = q.Email,
				BirthDate = q.BirthDate.HasValue ? q.BirthDate.Value.ToString() : "",
				CompanyName = q.CompanyName ?? "",
				PhotoThumbUrl = q.PhotoThumbUrl ?? "",
				Phone = q.Phone ?? "",
				CreatedWhen = q.CreatedWhen.ToString()
			}).ToListAsync();

			var response = new ProfilePageResponse
			{
				Page = request.Page,
				PageSize = request.PageSize,
				RecordCount = count
			};

			response.Records.Add(records);

			return response;
		}
	}
}
