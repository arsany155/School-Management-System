using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.CQRS_DESIGN.ApplicationUser.Queries.DTO;
using SchoolProject.Core.CQRS_DESIGN.ApplicationUser.Queries.Models;
using SchoolProject.Core.Localization;
using SchoolProject.Core.Wrapper;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.CQRS_DESIGN.ApplicationUser.Queries.Handlers
{
    public class UserQueryHandler : ResponseHandler,
         IRequestHandler<GetUserPaginationQuery, PaginationResult<GetUserPaginationDTO>>,
         IRequestHandler<GetUserByIdQuery, Response<GetUserByIdDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _sharedResources;
        private readonly UserManager<User> _userManager;
        public UserQueryHandler(IStringLocalizer<SharedResources> stringLocalizer,
                                  IMapper mapper,
                                  UserManager<User> userManager) : base(stringLocalizer)
        {
            _mapper = mapper;
            _sharedResources = stringLocalizer;
            _userManager = userManager;
        }

        public async Task<PaginationResult<GetUserPaginationDTO>> Handle(GetUserPaginationQuery request, CancellationToken cancellationToken)
        {
            var users = _userManager.Users.AsQueryable();
            var paginatedList = await _mapper.ProjectTo<GetUserPaginationDTO>(users)
                                            .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }

        public async Task<Response<GetUserByIdDTO>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id.Equals(request.Id));
            //var user1 = _userManager.FindByIdAsync(request.Id.ToString());
            if (user == null)
            {
                return NotFound<GetUserByIdDTO>(_sharedResources[SharedResourcesKeys.NotFound]);
            }
            var userMapper = _mapper.Map<GetUserByIdDTO>(user);
            return Success(userMapper);
        }
    }
}
