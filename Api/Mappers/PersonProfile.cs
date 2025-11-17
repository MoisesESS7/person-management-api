using Api.Models;
using Api.Requests.Persons;
using Application.Commands.Persons;
using Application.Common.Commands.Documents;
using Application.Common.Models;
using AutoMapper;
using ApiPaginationMeta = Api.Models.PaginationMeta;
using ApiPersonResponse = Api.Responses.Persons.PersonResponse;
using AppPersonResponse = Application.Common.Response.Persons.PersonResponse;

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

            CreateMap<AppPersonResponse, ApiPersonResponse>()
                .ConstructUsing(src => new ApiPersonResponse(
                        src.Id,
                        src.Name,
                        src.Age,
                        src.Rg.BirthDate,
                        src.Cpf.Number,
                        src.Cpf.RegistrationDate,
                        src.Rg.Number,
                        src.Rg.IssuingAuthority)
                );

            CreateMap<PagedResult<AppPersonResponse>, PagedResponse<ApiPersonResponse>>()
                .ConvertUsing(src => new PagedResponse<ApiPersonResponse>
                    (
                        src.Data.Select(item => new ApiPersonResponse(
                            item.Id,
                            item.Name,
                            item.Age,
                            item.Rg.BirthDate,
                            item.Cpf.Number,
                            item.Cpf.RegistrationDate,
                            item.Rg.Number,
                            item.Rg.IssuingAuthority))
                        .ToList(),

                        new ApiPaginationMeta
                        (
                            src.Meta.PageMeta != null ? src.Meta.PageMeta.PageNumber : 0,
                            src.Meta.PageMeta != null ? src.Meta.PageMeta.PageSize : 0,
                            src.Meta.PageMeta != null ? src.Meta.PageMeta.TotalRecords : 0,
                            src.Meta.PageMeta != null ? src.Meta.PageMeta.TotalPages : 0
                        )
                    )
                );
        }
    }
}
