using AutoMapper;
using MediatR;
using MusicSite.API.Persistence;

namespace MusicSite.API.Features
{
    public abstract class BaseRequestHandler<Q, T> : IRequestHandler<Q, T> where Q : IRequest<T>
    {
        protected readonly ApplicationDbContext Context;
        protected readonly IMapper Mapper;

        protected BaseRequestHandler(ApplicationDbContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }

        public abstract Task<T> Handle(Q request, CancellationToken cancellationToken);
    }
}
