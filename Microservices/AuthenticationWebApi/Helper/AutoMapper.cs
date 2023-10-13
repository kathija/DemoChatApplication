namespace AuthenticationWebApi.Helper;

using AuthenticationWebApi.Model;
using AutoMapper;
using ChatApplication.Model;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {

        // RegisterRequest -> User
        CreateMap<RegisterRequest, User>();

    }
}