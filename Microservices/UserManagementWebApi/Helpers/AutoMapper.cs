namespace UserManagementWebApi.Helpers;

using AutoMapper;
using ChatApplication.Model;
using UserManagementWebApi.Model;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {

        // RegisterRequest -> User
        CreateMap<UpdateRequest, User>();

    }
}