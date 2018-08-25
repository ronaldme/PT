﻿using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using PT.DAL;
using PT.DAL.Entities;

namespace PT.Web.Features.Exercises
{
    public class UpsertExerciseCommandHandler : AsyncRequestHandler<UpsertExerciseCommand>
    {
        private readonly PtContext _db;
        private readonly IMapper _mapper;

        public UpsertExerciseCommandHandler(PtContext db,
            IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        protected override async Task Handle(UpsertExerciseCommand request, CancellationToken cancellationToken)
        {
            if (request.Id.HasValue)
            {
                var exercise = await _db.Exercises.FindAsync(request.Id) ??
                               throw new ArgumentException($"No exercise found with Id: {request.Id}");
                _mapper.Map(request, exercise);
            }
            else
            {
                _db.Exercises.Add(_mapper.Map<Exercise>(request));
            }

            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}