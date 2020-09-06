using AutoMapper;
using WebApi.Entities;
using WebApi.Models.Accounts;
using WebApi.Models.WhitelistedItems;

namespace WebApi.Helpers
{
    public class AutoMapperProfile : Profile
    {
        // mappings between model and entity objects
        public AutoMapperProfile()
        {
            CreateMap<Account, AccountResponse>();

            CreateMap<Account, AuthenticateResponse>();

            CreateMap<RegisterRequest, Account>();

            CreateMap<Models.Accounts.CreateRequest, Account>();

            CreateMap<Models.Accounts.UpdateRequest, Account>()
                .ForAllMembers(x => x.Condition(
                    (src, dest, prop) =>
                    {
                        // ignore null & empty string properties
                        if (prop == null) return false;
                        if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                        // ignore null role
                        if (x.DestinationMember.Name == "Role" && src.Role == null) return false;

                        return true;
                    }
                ));

            CreateMap<Models.WhitelistedItems.CreateRequest, WhitelistedItem>();
            CreateMap<Models.WhitelistedItems.UpdateRequest, WhitelistedItem>();
            CreateMap<WhitelistedItem, WhitelistedItemResponse>();
        }
    }
}
