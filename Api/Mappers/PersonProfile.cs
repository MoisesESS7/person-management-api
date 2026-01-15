using Api.Contracts.Requests.Persons;
using Api.Models;
using Application.Common.Models;
using Application.Features.Persons.Commands.Create;
using Application.Features.Persons.Commands.Delete;
using Application.Features.Persons.Commands.Update;
using Application.Features.Persons.Queries.GetById;
using Application.Features.Persons.Queries.PagedSearch;
using AutoMapper;
using ApiPaginationMeta = Api.Models.PaginationMeta;
using ApiPersonResponse = Api.Contracts.Responses.Persons.PersonResponse;
using AppPersonResponse = Application.Features.Persons.Responses.PersonResponse;

namespace Api.Mappers
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<string, GetPersonByIdQuery>()
                .ConstructUsing(id => new GetPersonByIdQuery(id));

            CreateMap<string, DeletePersonCommand>()
                .ConstructUsing(id => new DeletePersonCommand(id));

            CreateMap<UpdatePersonRequest, UpdatePersonCommand>()
                .ConstructUsing(src => new UpdatePersonCommand(src.Id, src.Name));

            CreateMap<SearchParamsQuery, PagedSearchQuery>()
                .ConstructUsing(src =>
                    new PagedSearchQuery(new SearchParams
                    {
                        PageNumber = src.PageNumber,
                        PageSize = src.PageSize,
                        SearchTerm = src.SearchTerm,
                        SortBy = src.SortBy,
                        SortDescending = src.SortDescending
                    }));

            CreateMap<CreatePersonRequest, CreatePersonCommand>()
                .ConstructUsing(src => new CreatePersonCommand(
                    src.Name,
                    src.BirthDate,
                    src.CpfNumber,
                    src.CpfRegistrationDate,
                    src.RgNumber,
                    src.RgIssuingAuthority
                ));

            CreateMap<AppPersonResponse, ApiPersonResponse>()
                .ConstructUsing(src => new ApiPersonResponse(
                        src.Id,
                        src.Name,
                        src.CreatedAt,
                        src.UpdatedAt,
                        src.DeletedAt,
                        src.Age,
                        src.BirthDate,
                        src.CpfNumber,
                        src.CpfRegistrationDate,
                        src.RgNumber,
                        src.RgIssuingAuthority)
                );

            CreateMap<PagedResult<AppPersonResponse>, PagedResponse<ApiPersonResponse>>()
                .ConvertUsing(src => new PagedResponse<ApiPersonResponse>
                    (
                        src.Data.Select(item => new ApiPersonResponse(
                            item.Id,
                            item.Name,
                            item.CreatedAt,
                            item.UpdatedAt,
                            item.DeletedAt,
                            item.Age,
                            item.BirthDate,
                            item.CpfNumber,
                            item.CpfRegistrationDate,
                            item.RgNumber,
                            item.RgIssuingAuthority))
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
