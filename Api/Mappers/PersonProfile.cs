using AutoMapper;
using Application.Commands.Persons;
using Application.Common.Commands.Documents;
using Api.Requests.Persons;
using AppResponse = Application.Common.Response.Persons.PersonResponse;
using ApiResponse = Api.Responses.Persons.PersonResponse;

namespace Api.Mappers
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<UpdatePersonRequest, UpdatePersonCommand>()
                .ConstructUsing(src => new UpdatePersonCommand(src.Id, src.Name));

            CreateMap<CreatePersonRequest, CreatePersonCommand>()
                .ConstructUsing(src => new CreatePersonCommand(
                    src.Name,
                    new CpfCommand(src.CpfNumber, src.BirthDate, src.CpfRegistrationDate),
                    new RgCommand(src.RgNumber, src.BirthDate, src.RgIssuingAuthority)
                ));
            
            CreateMap<AppResponse, ApiResponse>()
                .ConvertUsing(src => new ApiResponse
                (
                    src.Id,
                    src.Name,
                    src.Age,
                    src.Rg.BirthDate,
                    src.Cpf.Number,
                    src.Cpf.RegistrationDate,
                    src.Rg.Number,
                    src.Rg.IssuingAuthority
                ));
        }
    }
}
