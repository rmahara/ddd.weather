﻿using DDD.Domain.Entities;

namespace DDD.Domain.Repositories
{
    public interface IAreasRepository
    {
        IReadOnlyList<AreaEntity> GetData();
    }
}
